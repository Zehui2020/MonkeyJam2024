using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

class CRTTelevisionPass : ScriptableRenderPass
{
    private Material _mat;
    private CRTTelevisionPostProcess crtTelevisionPostProcess;
    private RenderTargetIdentifier src;
    private RenderTargetHandle dest;
    private int texID;

    public CRTTelevisionPass()
    {
        if (!_mat)
            _mat = CoreUtils.CreateEngineMaterial("Custom Post-Processing/CRT Television");

        renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        crtTelevisionPostProcess = VolumeManager.instance.stack.GetComponent<CRTTelevisionPostProcess>();
        RenderTextureDescriptor desc = renderingData.cameraData.cameraTargetDescriptor;
        src = renderingData.cameraData.renderer.cameraColorTarget;
    }

    public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
    {
        if (crtTelevisionPostProcess == null || !crtTelevisionPostProcess.IsActive())
            return;

        texID = Shader.PropertyToID("_MainTex");
        dest = new RenderTargetHandle();
        dest.id = texID;

        cmd.GetTemporaryRT(texID, cameraTextureDescriptor);
        base.Configure(cmd, cameraTextureDescriptor);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (crtTelevisionPostProcess == null || !crtTelevisionPostProcess.IsActive())
            return;

        CommandBuffer cmd = CommandBufferPool.Get("Custom/CRT Television");
        _mat.SetFloat("_ChromaticAberrationIntensity", crtTelevisionPostProcess.ChromaticAberrationIntensity.value);

        _mat.SetFloat("_Curve", crtTelevisionPostProcess.CRTCurvature.value);
        float randLineStrength = UnityEngine.Random.Range(0, crtTelevisionPostProcess.CRTScanLineStrength.value);
        _mat.SetFloat("_ScanlineStrength", randLineStrength);
        _mat.SetFloat("_GrayscaleStrength", crtTelevisionPostProcess.CRTGrayscaleStrength.value);

        cmd.Blit(src, texID, _mat, 0);
        cmd.Blit(texID, src, _mat, 1);
        cmd.Blit(src, texID, _mat, 2);
        cmd.Blit(texID, src, _mat, 3);

        context.ExecuteCommandBuffer(cmd);
        cmd.Clear();
        CommandBufferPool.Release(cmd);
    }
}