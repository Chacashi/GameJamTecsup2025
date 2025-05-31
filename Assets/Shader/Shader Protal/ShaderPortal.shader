Shader "Custom/MovingPortal" 
{
    Properties
    {
        _Speed ("Motion Speed", Float) = 1.0
        _Distortion ("Distortion Strength", Float) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _Speed;
            float _Distortion;
            float _TimeY;

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv - 0.5;
                float radius = length(uv);

                float distortion = sin(_TimeY * _Speed + radius * 10.0) * _Distortion;
                uv *= (1.0 + distortion);

                float alpha = 1.0 - smoothstep(0.4, 0.5, radius);
                return fixed4(uv.x, uv.y, 1.0, alpha);
            }
            ENDCG
        }
    }
}