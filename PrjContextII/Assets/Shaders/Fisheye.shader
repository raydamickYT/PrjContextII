Shader "Custom/FisheyeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BulgeAmount ("Bulge Amount", Float) = 0.5
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

            sampler2D _MainTex;
            float _BulgeAmount;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Fisheye effect logic
                float2 uv = i.uv * 2.0 - 1.0; // Normalize UV to [-1, 1]
                float theta = atan2(uv.y, uv.x);
                float radius = length(uv);

                radius = pow(radius, _BulgeAmount);

                uv.x = radius * cos(theta);
                uv.y = radius * sin(theta);
                uv = uv * 0.5 + 0.5; // Revert UV to [0, 1]

                fixed4 col = tex2D(_MainTex, uv);
                return col;
            }
            ENDCG
        }
    }
}
