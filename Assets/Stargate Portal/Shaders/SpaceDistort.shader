Shader "Stargate - Portal/SpaceDistort"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("NormalGlow", 2D) = "white" {}
        [HDR]_CrackColor("Crack Color", Color) = (1,1,1,1)
        _GlobalCrack ("Global Crack", 2D) = "black" {}
        _Normal("Normal", Vector) = (0,0,1,0)
        _Move("Move", Vector) = (0,0,1,0)
        _Animation("Animation Mult Pow, Noise, Center Distort", Vector) = (0,0,1,0)
        _ColorControl1("Glow Mult, Pow, Crack Progress, Softness", Vector) = (1,1,1,1)
        [HDR]_GlowColor("Glow Color", Color) = (1,1,1,1)
        _FogColor("Fog Color", Color) = (1,1,1,1)

        [IntRange] _StencilRef ("Stencil Reference Value", Range(0,255)) = 0
    }


SubShader
    {
        // Draw ourselves after all opaque geometry
        Tags {"RenderType"="Transparent" "Queue"="Transparent" "DisableBatching"="True" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite On
        ZTest LEqual
        Cull Off
        // Grab the screen behind the object into _BackgroundTexture
        GrabPass
        {
            "_BackgroundTexture"
        }

        Stencil{
            Ref [_StencilRef]
            Comp NotEqual
            //Pass Replace
        }

        // Render the object with the texture generated above, and invert the colors
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "StargateCG.cginc"
            // make fog work
            #pragma multi_compile_fog
            
            float4 _Normal, _Move, _Animation, _ColorControl1;
            float4 _GlowColor, _FogColor, _CrackColor;

            float3x3 XRotationMatrix(float sina, float cosa)
            {
                return float3x3(
                    1,   0,       0,
                    0,   cosa,   -sina,
                    0,   sina,   cosa);
            }
       
            float3x3 YRotationMatrix(float sina, float cosa)
            {
                return float3x3(
                    cosa,    0,   -sina,
                    0,        1,   0,
                    sina,    0,   cosa);
            }
       
            float3x3 ZRotationMatrix(float sina, float cosa)
            {
                return float3x3(
                    cosa,    -sina,   0,
                    sina,    cosa,   0,
                    0,        0,       1);
            }

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float3 normal : NORMAL;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 grabPos : TEXCOORD0;
                float4 pos : SV_POSITION;
                float4 color : COLOR;
                float3 normal : NORMAL;
                float4 uv : TEXCOORD1;
                float4 uv2 : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };

            v2f vert(appdata v) {
                v2f o;

                UNITY_INITIALIZE_OUTPUT(v2f, o);

                o.uv.xy = v.uv0.xy;
                o.uv2.xy = v.uv2.xy;
                o.uv2.zw = v.vertex.xy;

                //v.vertex.y = 0;

                o.normal = v.normal;
                
                //v.normal = _Normal;
                float centerGradient = distance(float2(0,0), v.uv1.xy);
                float fromCenter = pow(centerGradient * _Animation.x, _Animation.y) + _Move.w;
                
                float anticipatePhase = fromCenter + 1;

                v.vertex.y = lerp(0, v.vertex.y * 4 - ((sin(_Time[1] * 50 -.5 + v.uv1.y * v.uv1.x - v.uv1.y) * .5 + .5) * v.vertex.y), saturate(anticipatePhase));
                
                float4 originalVert = v.vertex;

                //vertex animation over time

                float rotAngle = v.normal.x * 1000 + _Animation.z * _Time[0];
                //Convert to radian
                rotAngle *= 0.01745329251994329576923690768489;//rotAngle * UNITY_PI / 180.0
                //rotAngle += _Move.w;
                //Calculate sin cos
                float sina, cosa;
                sincos(rotAngle, sina, cosa);

                float randRot = frac((v.normal.y + v.normal.z));
                float3 pivot = float3(-v.uv1.x, 0, v.uv1.y);

                //Move to root
                v.vertex.xyz -= pivot;

                v.vertex.xyz += _Move.xyz * saturate(fromCenter);

                //Rotate
                v.vertex.xyz = lerp(mul(XRotationMatrix(sina, cosa), v.vertex.xyz), mul(ZRotationMatrix(sina, cosa), v.vertex.xyz), randRot) * (1-v.color.r);
        
                v.vertex.xyz = mul(YRotationMatrix(sina, cosa), v.vertex.xyz);


                //Move it back
                v.vertex.xyz += pivot;


                v.vertex = lerp(originalVert, v.vertex, saturate(fromCenter * 10));
                v.vertex.xyz = lerp(v.vertex.xyz, _Move.xyz, saturate(fromCenter));

                o.color.r = v.color.r;
                o.color.g = centerGradient;
                o.color.b = anticipatePhase;
                o.color.a = fromCenter;
                o.grabPos = ComputeGrabScreenPos(UnityObjectToClipPos(float3(originalVert.x, 0, originalVert.z)));
                
                o.pos = UnityObjectToClipPos(v.vertex);

                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }

            sampler2D _BackgroundTexture, _MainTex, _GlobalCrack;

            half4 frag(v2f i) : SV_Target
            {
                half4 globalCrack = tex2D(_GlobalCrack, i.uv.xy);

                float crackProgress = 1 - saturate((globalCrack.b - _ColorControl1.z) / _ColorControl1.w);

                half4 normalGlow = tex2D(_MainTex, i.uv.xy);

                half4 normalGlowZ = tex2D(_MainTex, i.uv2.zw * .25 - float2(0, _Time[0] * 15));

                i.grabPos.xy += (globalCrack.xy * 2 - 1) * .5 * crackProgress * -(i.uv.xz * 2 - 1);

                half4 bgColor = tex2Dproj(_BackgroundTexture, i.grabPos) + saturate((i.color.aaaa * 5 + .75) - (i.uv.xyxy - 0.5) * i.color.b);
                bgColor.a = 1;
                
                half4 finalColor = bgColor + normalGlow.a * saturate(pow(saturate(i.color.b) * _ColorControl1.x, _ColorControl1.y)) * _GlowColor;
                finalColor += ((1 - saturate(finalColor)) * 2 - 1 ) * saturate(i.color.b);
                
                finalColor.rgb = lerp(finalColor.rgb, Overlay(normalGlowZ.xyz, finalColor.rgb), saturate(i.color.r) + saturate(i.color.b));

                finalColor.rgb += _CrackColor * (Overlay(finalColor.rgb, globalCrack.rga)) * globalCrack.a * crackProgress + sin(crackProgress * UNITY_PI) * globalCrack.a;

                finalColor.a *= lerp(1, i.uv2.x, i.color.r * (1 - i.uv2.x));

                //UNITY_APPLY_FOG_COLOR(i.fogCoord, finalColor, _FogColor);

                return finalColor;
            }
            ENDCG
        }

    }
}