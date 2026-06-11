Shader "PF-1900/Simple Shader V0"
{
    SubShader{
        Pass{

            CGPROGRAM
            #pragma vertex vertices
            #pragma fragment fragments

            struct appdata{
                float4 vertex : POSITION; 
            };
            
            struct FromVertToFrag{
                float4 vertex: SV_POSITION;
            };

            FromVertToFrag vertices(appdata v){
                FromVertToFrag o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float4 fragments (FromVertToFrag i) : SV_TARGET{
                return float4(0,1,0,1);
            }
            
            ENDCG
        }
    }
}