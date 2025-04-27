Shader "Custom/TMP-URP-SDF-Lit"
{
    Properties
    {
        _MainTex ("Font Atlas (Alpha as Distance Field)", 2D) = "white" {}
        _FaceColor ("Face Color", Color) = (1,0,0,1)
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width", Range(0,0.1)) = 0.05
        _Smoothness ("Smoothing", Range(0.001, 0.1)) = 0.01
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "RenderPipeline"="UniversalPipeline" }
        LOD 300
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            float4 _MainTex_ST;
            float4 _FaceColor;
            float4 _OutlineColor;
            float _OutlineWidth;
            float _Smoothness;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float distance = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv).a;

                float edge = smoothstep(0.5 - _Smoothness, 0.5 + _Smoothness, distance);
                float outline = smoothstep(0.5 - _OutlineWidth - _Smoothness, 0.5 - _OutlineWidth + _Smoothness, distance);

                float3 baseColor = lerp(_OutlineColor.rgb, _FaceColor.rgb, outline);

                // Correct lighting using URP Main Light
                Light mainLight = GetMainLight(); // 👈 This gives you light color + direction
                float3 normal = normalize(IN.normalWS);
                float NdotL = saturate(dot(normal, -mainLight.direction)); // 👈 negative light direction
                float3 litColor = baseColor * (mainLight.color.rgb * NdotL);

                return float4(litColor, edge * _FaceColor.a);
            }

            ENDHLSL
        }
    }
}
