Shader "Unlit/RayMarch"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #define MAX_STEPS 100
            #define MAX_DIST 100
            #define SURF_DIST 0.01
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 ro : TEXCOORD1;
                float3 hitPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                // 模型空间下的PDF
                o.ro = mul(unity_WorldToObject, float4(_WorldSpaceCameraPos,1));
                o.hitPos = v.vertex;
                // 世界空间下的PDF
                // o.ro = _WorldSpaceCameraPos;
                // o.hitPos = mul(unity_ObjectToWorld, v.vertex);
                // 也可以直接求出摄像机到顶点方向
                // rd = -WorldSpaceViewDir(v.vertex);

                //想要找到该像素点的世界坐标
                // float3 vP = UnityObjectToViewPos(v.vertex);
                // vP.z = -1;
                // o.ro = mul(unity_CameraToWorld,vP).xyz;

                return o;
            }

            float GetDist(float3 p)
            {
                float d = length(p) - 0.1;
                return d;
            }

            float RayMarch(float3 ro, float3 rd)
            {
                float dO = 0;
                float dS;
                for(int i = 0; i < MAX_STEPS; i++)
                {
                    float3 p = ro + dO * rd;
                    dS = GetDist(p);
                    dO += dS;
                    if(dS < SURF_DIST || dO > MAX_DIST)break;
                }
                return dO;
            }

            float3 GetNormal(float3 p)
            {
                float2 e = float2(0.01, 0);
                float3 n = GetDist(p) - float3(
                    GetDist(p-e.xyy),
                    GetDist(p-e.yxy),
                    GetDist(p-e.yyx)
                );
                return normalize(n);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                //摄像机原点坐标
                float3 ro = i.ro;
                //射线反向
                float3 rd = normalize(i.hitPos - ro);
                float d = RayMarch(ro, rd);
                if(d >= MAX_DIST)
                    discard;
                else{
                    float3 p = ro + rd * d;
                    float3 n = GetNormal(p);
                    col.rgb = n;
                }
                return col;
            }
            ENDCG
        }
    }
}
