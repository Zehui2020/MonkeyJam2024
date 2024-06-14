using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using System;

public class CRTTelevisionRenderFeature : ScriptableRendererFeature
{
    private CRTTelevisionPass crtTelevisionPass;

    public override void Create()
    {
        crtTelevisionPass = new CRTTelevisionPass();
        name = "CRT Television";
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(crtTelevisionPass);
    }
}