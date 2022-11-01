Shader "Hidden/Internal-Flare"
{
    SubShader {

        Tags {"Queue"="Overlay" "RenderType"="Overlay"}
        ZWrite Off ZTest Always
        Cull Off
        Blend One One
        //ColorMask RGB

        Stencil{
            Ref 15
            Comp Greater
        }

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"

            sampler2D _FlareTexture;

            struct appdata_t {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            float4 _FlareTexture_ST;

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                o.texcoord = TRANSFORM_TEX(v.texcoord, _FlareTexture);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 color = tex2D(_FlareTexture, i.texcoord) * i.color;
                color = lerp(color, color + color * abs(sin(_Time[3])), abs(sin(_Time[2] * 2.3))) * 5;
                return color;
            }
            ENDCG
        }
    }
}
