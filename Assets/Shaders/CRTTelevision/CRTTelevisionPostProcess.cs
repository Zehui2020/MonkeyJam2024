using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using System;

[Serializable, VolumeComponentMenuForRenderPipeline("Custom/CRT Television", typeof(UniversalRenderPipeline))]
public class CRTTelevisionPostProcess : VolumeComponent, IPostProcessComponent
{
    [Tooltip("Intensity of chromatic aberration")]
    public FloatParameter ChromaticAberrationIntensity = new ClampedFloatParameter(1.0f, 0f, 0.01f);

    [Tooltip("Curvature of CRT effect")]
    public FloatParameter CRTCurvature = new ClampedFloatParameter(0.3f, 0f, 1f);

    [Tooltip("Strength of CRT line effect")]
    public FloatParameter CRTScanLineStrength = new ClampedFloatParameter(0.7f, 0f, 1f);

    [Tooltip("Strength of CRT grayscale effect")]
    public FloatParameter CRTGrayscaleStrength = new ClampedFloatParameter(0f, 0f, 20f);

    public bool IsActive()
    {
        return (ChromaticAberrationIntensity.value > 0.0f) && active;
    }

    public bool IsTileCompatible()
    {
        return true;
    }
}