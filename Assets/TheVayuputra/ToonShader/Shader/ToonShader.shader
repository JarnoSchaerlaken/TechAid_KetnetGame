Shader "TheVayuputra/ToonShader"
{
    Properties
    {
        [MainTexture] _BaseMap ("Texture", 2D) = "white" {}
        [MainColor] _BaseColor ("Color", Color) = (0.5,0.5,0.5,1)

        _ShadeThreshold ("Shade Threshold", Range(0, 1)) = 0.5
        _ShadeSoftness ("Shade Softness", Range(0, 1)) = 0.04

        _GlossThreshold ("Gloss Threshold", Range(0, 1)) = 0.6
        _GlossSoftness ("Gloss Softness", Range(0, 1)) = 0.05
        [HDR]_GlossTint ("Gloss Tint", Color) = (1,1,1,1)

        _EdgeThreshold ("Edge Threshold", Range(0, 1)) = 0.65
        _EdgeSoftness ("Edge Softness", Range(0,1)) = 0.4
        _EdgeColor ("Edge Color", Color) = (1,1,1,1)

        _StrokeWidth ("Stroke Width", Range(0.0, 1.0)) = 0.15
        _StrokeColor ("Stroke Color", Color) = (0,0,0,1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }

        Pass
        {
            Name "ForwardPass"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile_fog
            #pragma multi_compile_instancing

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"

            TEXTURE2D(_BaseMap); SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;
                float _ShadeThreshold;
                float _ShadeSoftness;
                float _GlossThreshold;
                float _GlossSoftness;
                float4 _GlossTint;
                float _EdgeThreshold;
                float _EdgeSoftness;
                float4 _EdgeColor;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
                float4 tangentOS  : TANGENT;
                float2 uv         : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float2 uv : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float3 viewDirWS : TEXCOORD2;
                float4 shadowCoord : TEXCOORD3;
                half fogCoord : TEXCOORD4;
                float3 positionWS : TEXCOORD5;
                float4 positionCS : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            Varyings vert(Attributes input)
            {
                Varyings output = (Varyings)0;

                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);

                VertexPositionInputs posInput = GetVertexPositionInputs(input.positionOS.xyz);
                VertexNormalInputs normInput = GetVertexNormalInputs(input.normalOS, input.tangentOS);

                output.positionCS = posInput.positionCS;
                output.positionWS = posInput.positionWS;
                output.normalWS = normInput.normalWS;
                output.viewDirWS = GetCameraPositionWS() - posInput.positionWS;
                output.uv = input.uv;

                output.shadowCoord = TransformWorldToShadowCoord(posInput.positionWS);
                output.fogCoord = ComputeFogFactor(posInput.positionCS.z);

                return output;
            }

            float4 frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(input);

                float3 N = normalize(input.normalWS);
                float3 V = normalize(input.viewDirWS);
                float3 L = normalize(_MainLightPosition.xyz);
                float3 H = normalize(V + L);

                float NL = dot(N, L) * 0.5 + 0.5;
                float NV = dot(N, V);
                float NH = dot(N, H);

                float3 baseMap = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv).rgb;

                float shadeFactor = smoothstep(_ShadeThreshold - _ShadeSoftness,
                                               _ShadeThreshold + _ShadeSoftness, NL);

                float glossFactor = smoothstep(
                    (1 - _GlossThreshold * 0.05) - _GlossSoftness * 0.05,
                    (1 - _GlossThreshold * 0.05) + _GlossSoftness * 0.05,
                    NH
                );

                float shadow = MainLightRealtimeShadow(input.shadowCoord);

                float edge = smoothstep(
                    (1 - _EdgeThreshold) - _EdgeSoftness * 0.5,
                    (1 - _EdgeThreshold) + _EdgeSoftness * 0.5,
                    0.5 - NV
                );

                float3 diffuse = _MainLightColor.rgb * baseMap * _BaseColor.rgb * shadeFactor * shadow;
                float3 specular = _GlossTint.rgb * shadow * shadeFactor * glossFactor;
                float3 ambient = edge * _EdgeColor.rgb + SampleSH(N) * _BaseColor.rgb * baseMap;

                float3 finalColor = diffuse + ambient + specular;
                finalColor = MixFog(finalColor, input.fogCoord);

                return float4(finalColor, 1.0);
            }

            ENDHLSL
        }

        // Outline Pass
        Pass
        {
            Name "OutlinePass"
            Cull Front
            Tags { "LightMode"="SRPDefaultUnlit" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                half fogCoord : TEXCOORD0;
            };

            float _StrokeWidth;
            float4 _StrokeColor;

            v2f vert(appdata v)
            {
                v2f o;

                float3 pos = v.vertex.xyz + v.normal * _StrokeWidth * 0.1;
                o.pos = TransformObjectToHClip(pos);

                VertexPositionInputs posInput = GetVertexPositionInputs(v.vertex.xyz);
                o.fogCoord = ComputeFogFactor(posInput.positionCS.z);

                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float3 col = MixFog(_StrokeColor.rgb, i.fogCoord);
                return float4(col, 1.0);
            }

            ENDHLSL
        }

        UsePass "Universal Render Pipeline/Lit/ShadowCaster"
    }
}