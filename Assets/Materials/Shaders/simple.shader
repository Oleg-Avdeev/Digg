Shader "Unlit/simple"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1) 
        _Light ("Light Vector", Vector) = (1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            // indicate that our pass is the "base" pass in forward
            // rendering pipeline. It gets ambient and main directional
            // light data set up; light direction in _WorldSpaceLightPos0
            // and color in _LightColor0
            Tags {"LightMode"="ForwardBase"}
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc" // for UnityObjectToWorldNormal
            #include "UnityLightingCommon.cginc" // for _LightColor0

            struct v2f
            {
                float2 uv : TEXCOORD0;
                fixed4 diff : COLOR0; // diffuse lighting color
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            fixed4 _Light;
            fixed4 _Color;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                half nl = max(0, dot(worldNormal, _Light.xyz));
                o.diff = nl;
                return o;
            }
            

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _Color;

                fixed f = i.diff * 3 - frac(i.diff * 3 + 0.1) - 0.1;
                f = max(f, 0.4);

                col = saturate(col * f);
                
                return col;
            }
            ENDCG
        }
    }
}
