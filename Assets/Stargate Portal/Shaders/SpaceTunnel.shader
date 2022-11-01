Shader "Stargate - Portal/Space Tunnel"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.BlendMode)] _BlendSrc("BlendMode Src", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendDst("BlendMode Dst", Float) = 10

        [HDR]_ColorA ("Cloud Color A", Color) = (1,1,1,1)
        [HDR]_ColorB ("Cloud Color B", Color) = (1,1,1,1)
        [HDR]_EmissionColor ("Emission Color", Color) = (1,1,1,1)
        _SkyTex ("Texture", Cube) = "white" {}
        _MainTex ("Texture", 2D) = "white" {}
        _FlowTex ("Flow Displace", 2D) = "white" {}
        [Header(ScrollSpeed1 XY l ScrollSpeed2 XY)]
        _Settings("", Vector) = (1,1,1,1)
        [Header(Fresnel Mult Pow l Flow Speed l Influence)]
        _Settings3("", Vector) = (1,1,1,1)
        [Header(UV Distort XY l Flow Distort XY)]
        _Settings4("", Vector) = (1,1,1,1)
        [Header(Distort Opacity l  Emission Opacity l EMPTY l Global Opacity)]
        _Settings5("", Vector) = (1,1,1,1)
        [Header(Emission Mult Pow l Dissolve Progress l Dissolve Softness)]
        _Settings6("", Vector) = (1,1,1,1)
        [Header(Event Horizon Mult Pow l EMPTY l EMPTY)]
        _Settings8("", Vector) = (1,1,1,1)
        [Header(Phase Stabilization Pattern Pow Mult l Progress l Alpha)]
        _PhaseStabilization("", Vector) = (1,1,1,1)
        [IntRange] _StencilRef ("Stencil Reference Value", Range(0,255)) = 0
    }
    SubShader
    {
        Blend [_BlendSrc] [_BlendDst]
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        ZWrite Off
        //ZTest Always
        Cull Back
        LOD 100

        Stencil{
            Ref [_StencilRef]
            Comp Equal
        }

        GrabPass
        {
            "_DistortTexture"
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
                float4 color : COLOR;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 uv : TEXCOORD0;
                float4 uv1 : TEXCOORD1;
                float4 uv2 : TEXCOORD2;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
                float3 worldNormal : TEXCOORD3;
                float3 viewDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                float4 grabPos : TEXCOORD6;
                half3 worldRefl : TEXCOORD7;
            };

            sampler2D _MainTex, _FlowTex, _DistortTexture;
            float4 _MainTex_ST, _FlowTex_ST, _Settings, _Settings3, _Settings4, _Settings5, _Settings6, _Settings7, _Settings8, _PhaseStabilization;
            float4 _ColorA, _ColorB, _EmissionColor;
            uniform samplerCUBE _SkyTex;

            float3 RotateAroundYInDegrees (float3 vertex, float degrees)
             {
                float alpha = degrees * UNITY_PI / 180.0;
                float sina, cosa;
                sincos(alpha / 2, sina, cosa);
                float3x3 m = float3x3(
                    cosa,    -sina,   0,
                    sina,    cosa,   0,
                    0,        0,       1);
                float3 r = float3(mul(m, vertex.xyz) ).rgb;
                return r;
            }

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_INITIALIZE_OUTPUT(v2f, o);

                float dissolveMask = saturate(((1 - v.uv.y) - _Settings6.z - .1) / _Settings6.w);

                float dissolveMask2 = saturate(((v.uv.y) - _Settings6.z - .1) / _Settings6.w);

                v.vertex.xz *= lerp(dissolveMask2, 1, v.color.b);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv.xy = v.uv.xy * _MainTex_ST.xy;
                o.uv.zw = v.uv.xy * _MainTex_ST.zw;

                o.uv1.xy = v.uv.xy * _FlowTex_ST.xy;
                o.uv1.zw = v.uv.xy * _FlowTex_ST.zw;

                o.uv2.xy = v.uv.xy;

                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = WorldSpaceViewDir (v.vertex);
                o.color = v.color;
                o.screenPos = ComputeScreenPos(o.vertex);
                o.grabPos = ComputeGrabScreenPos(UnityObjectToClipPos(v.vertex));
                
                //
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                // compute world space view direction
                float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
                // world space normal
                // world space reflection vector
                o.worldRefl = RotateAroundYInDegrees(-worldViewDir + o.worldNormal * (v.uv.y), _Time[0] * 1500); // + o.worldNormal * .25; //reflect(-worldViewDir, o.worldNormal);

                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float2 sinPattern = 1 - abs(sin(float2(i.uv2.x - _Time[1] * .5, i.uv2.y + i.uv2.y * .33) * 15 + _PhaseStabilization.z));
                float ripple = max(0, (pow((sinPattern.x + sinPattern.y) * sin(saturate(i.uv2.y * 1 + _PhaseStabilization.z) * UNITY_PI) * _PhaseStabilization.x, _PhaseStabilization.y)));

                half4 flowTex = tex2D(_FlowTex, i.uv1.xy + _Time[0] * _Settings3.z * float2(-1, 1) + ripple * 3);
                
                half4 mainTex = tex2D(_MainTex, i.uv.xy + flowTex.xy * _Settings3.w + _Time[0] * _Settings.xy);
                half4 mainTex2 = tex2D(_MainTex, i.uv.zw + flowTex.zw * _Settings3.w + _Time[0] * _Settings.zw);
                half4 mainTex3 = tex2D(_FlowTex, i.uv.zw + flowTex.zw * _Settings3.w + _Time[0] * _Settings.zw * 1.3);


                float4 dustCloud = Overlay(mainTex, mainTex2);

                float fresnel = saturate(pow(saturate(dot(normalize(i.worldNormal + (mainTex.xyz + mainTex2.xyz - 1)), normalize(i.viewDir))) * _Settings3.x, _Settings3.y));

                i.grabPos.xy += ((i.worldNormal.xy * _Settings4.xy + dustCloud.xy * _Settings4.zw) * i.uv.y * i.color.g)  * fresnel;

                half3 bgColor = DecodeHDR(tex2Dproj(_DistortTexture, i.grabPos), unity_SpecCube0_HDR);
                
                //sky reflection
                // sample the default reflection cubemap, using the reflection vector
                i.worldRefl *= lerp(flowTex.xyz, 1, 1 - i.uv.y);
                half4 skyData = texCUBE(_SkyTex, i.worldRefl + ripple * 5) * 1.5;


                float4 finalColor = float4(bgColor, _Settings5.x);

                finalColor = lerp(finalColor, lerp(_ColorA, _ColorB, dustCloud.r), fresnel);

                finalColor.rgba *= saturate(pow(dot(normalize(i.worldNormal), normalize(i.viewDir)), 3)) * i.uv.y;

                float emissionFactor = saturate(pow(Overlay(Overlay(sin(mainTex.r + mainTex2.b + i.uv.y),  mainTex3.r), i.uv.y) * _Settings6.x, _Settings6.y));

                finalColor += lerp(dustCloud.b, dustCloud.a, sin(mainTex.r + mainTex.b) ) * emissionFactor * _EmissionColor * i.color.r * _Settings5.y;

                finalColor.rgb = lerp(finalColor, skyData.rgb, 1 - emissionFactor * .5);

                float eventHorizon = (pow((1 - i.uv2.y)* _Settings8.x, _Settings8.y));

                finalColor.rgb = lerp(finalColor.rgb, Overlay(eventHorizon.rrr, finalColor.rgb), saturate(eventHorizon));

                finalColor.a = Overlay(finalColor.a, i.color.r);

                finalColor.a *= _Settings5.w;

                float dissolveMask = saturate(((1 - Overlay(i.uv2.y, Luminance(finalColor))) - _Settings6.z) / _Settings6.w);
                
                finalColor.rgb += lerp(Overlay(finalColor, _EmissionColor), Overlay(finalColor, _ColorA), ripple) * ripple * _PhaseStabilization.w * _EmissionColor;
                
                finalColor.rgb = lerp(bgColor, finalColor.rgb, saturate(dissolveMask * 2 - 1));

                finalColor.rgba *= saturate(dissolveMask * 4);

                return clamp(finalColor, 0, 10);
            }
            ENDCG
        }
    }
}
