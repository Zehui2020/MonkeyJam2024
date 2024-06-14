Shader "Custom Post-Processing/CRT Television"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}

        _ChromaticAberrationIntensity("Intensity", Float) = 0

        _Curve("Curvature", Float) = 0.2
        _ScanlineStrength("Scanline Strength", Float) = 0.1
        _GrayscaleStrength("Grayscale Strength", Float) = 0.1
    }

    SubShader
    {
        Cull off ZWrite Off ZTest Always
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"}

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #define E 2.71828f

        sampler2D _MainTex;
        float4 _MainTex_TexelSize;

        float _ChromaticAberrationIntensity;

        float _Curve;
        float _ScanlineStrength;
        float _GrayscaleStrength;

        struct appdata
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct v2f
        {
            float2 uv : TEXCOORD0;
            float4 vertex : SV_POSITION;
        };

        v2f vert(appdata v)
        {
            v2f o;
            o.vertex = TransformObjectToHClip(v.vertex.xyz);
            o.uv = v.uv;
            return o;
        }
        ENDHLSL

        // Chromatic Aberration pass
        pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag_chromatic

            float4 frag_chromatic(v2f i) : SV_Target
            {
                // Calculate chromatic aberration
                float2 redOffset = _ChromaticAberrationIntensity * float2(1, 0);
                float2 greenOffset = _ChromaticAberrationIntensity * float2(0, 0);
                float2 blueOffset = _ChromaticAberrationIntensity * float2(0, 1);

                // Sample the texture with color channel offsets
                float4 red = tex2D(_MainTex, i.uv + redOffset);
                float4 green = tex2D(_MainTex, i.uv + greenOffset);
                float4 blue = tex2D(_MainTex, i.uv + blueOffset);

                // Combine color channels
                return float4(red.r, green.g, blue.b, 1);
            }

            ENDHLSL
        }

        // CRT curvature
        pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag_crt

            float4 frag_crt(v2f i) : SV_Target
            {
                // Calculate displacement based on the distance from the center
                float2 displacement = i.uv - 0.5; 
                float distanceFromCenter = length(displacement);
                float displacementAmount = distanceFromCenter * distanceFromCenter * _Curve;

                // Apply curvature
                float2 displacedUV = i.uv + displacement * displacementAmount;

                // Sample main texture with curvature
                float4 col = tex2D(_MainTex, displacedUV);

                // Apply black to stretched edges
                if (displacedUV.x < 0 || displacedUV.y < 0 || displacedUV.x > 1 || displacedUV.y > 1)
                    return float4(0, 0, 0, 1);
                else
                    return col;
            }

            ENDHLSL
        }

        // CRT scan lines
        pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag_scanlines

            float4 frag_scanlines(v2f i) : SV_Target
            {
                // Sample main texture
                float4 col = tex2D(_MainTex, i.uv);

                // Apply scanlines
                float scanline = sin(i.uv.y * 500.0) * 0.5 + 0.5;
                col.rgb -= col.rgb * scanline * _ScanlineStrength;

                return col;
            }

            ENDHLSL
        }

        // CRT grayscale
        pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag_grayscale

            float4 frag_grayscale(v2f i) : SV_Target
            {
                // Sample main texture
                float4 col = tex2D(_MainTex, i.uv);

                if (_GrayscaleStrength > 0.f)
                {
                    // Convert to grayscale by averaging color channels
                    float grayscaleValue = (col.r + col.g + col.b) / _GrayscaleStrength;
                    // Set all color channels to the grayscale value
                    col.rgb = float3(grayscaleValue, grayscaleValue * 2, grayscaleValue);
                }

                return col;
            }

            ENDHLSL
        }
    }
}