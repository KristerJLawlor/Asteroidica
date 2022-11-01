Shader "Stargate - Portal/Gate Glow"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.BlendMode)] _BlendSrc("BlendMode Src", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendDst("BlendMode Dst", Float) = 10
        _Progress("Progress", float) = 1
        _Color ("Color", Color) = (1, 1, 1, 1)
        _MainTex("Noise Tex", 2D) = "black"{}
        [Header(Mult l Pow l EMPTY l Progress Pow)]
        _Settings("", Vector) = (1,1,1,1)
        [Header(Speed A l Speed B l Edge Glow l Center Glow)]
        _Settings2("", Vector) = (1,1,1,1)
        [Header(Channel Mult)]
        _Settings3("", Vector) = (1,1,1,1)
        [IntRange] _StencilRef ("Stencil Reference Value", Range(0,255)) = 0
        [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("Stencil Comp", Int) = 8
    }
    SubShader
    {
        Blend [_BlendSrc] [_BlendDst]
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        ZWrite Off
        LOD 100

        Stencil{
                Ref [_StencilRef]
                Comp [_StencilComp]
            }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "StargateCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 worldNormal : TEXCOORD2; //.w contains Y offset per segment
                float3 viewDir : TEXCOORD3;
            };
            
            float4 _Color, _Settings, _Settings2, _MainTex_ST, _Settings3;
            sampler2D _MainTex;
            float _Progress;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal.xyz = UnityObjectToWorldNormal(v.normal).xyz;
                o.worldNormal.w = v.uv2.y;
                o.viewDir = WorldSpaceViewDir (v.vertex);
                o.uv.xy = v.uv;
                o.uv.zw = TRANSFORM_TEX(v.uv1, _MainTex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half4 mainTex = tex2D(_MainTex, i.uv.zw + _Time[0] * _Settings2.x);
                half4 mainTex2 = tex2D(_MainTex, i.uv.zw + _Time[0] * _Settings2.y);

                float fresnel = pow(saturate(dot(i.worldNormal.xyz, normalize(i.viewDir))) * _Settings.x, _Settings.y);

                float4 finalColor = fresnel;

                finalColor *= Overlay(mainTex.x * _Settings3.x, mainTex2.z * _Settings3.y) * pow((i.uv.z), 3) * _Settings2.z;
                finalColor += Overlay(Overlay(mainTex.y * _Settings3.z, mainTex2.w * _Settings3.w), (1 - i.uv.z)) * _Settings2.w;
                finalColor *= _Color;

                finalColor *= pow(abs(i.uv.x - 0.5) *_Progress, _Settings.w);

                return max(0, finalColor);
            }
            ENDCG
        }
    }
}
