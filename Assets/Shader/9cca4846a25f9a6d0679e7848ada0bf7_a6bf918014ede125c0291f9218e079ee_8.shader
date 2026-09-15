Shader "Unlit/PlayerLight"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _FresnelColor ("Fresnel Color", Color) = (1,1,1,1)
        _FresnelPower ("Fresnel Power", Range(0.1, 10)) = 5.0
        _FresnelBias ("Fresnel Bias", Range(0, 1)) = 0.1
        _Transparency ("Center Transparency", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags 
        { 
            "Queue" = "Transparent" 
            "RenderType" = "Transparent"
            "IgnoreProjector" = "True"
        }
        
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Back

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 normal : TEXCOORD0;
                float3 viewDir : TEXCOORD1;
            };

            fixed4 _MainColor;
            fixed4 _FresnelColor;
            float _FresnelPower;
            float _FresnelBias;
            float _Transparency;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float fresnel = saturate(_FresnelBias + (1 - _FresnelBias) * 
                                pow(1 - dot(i.normal, i.viewDir), _FresnelPower));
                fixed4 col = lerp(_MainColor, _FresnelColor, fresnel);
                float alpha = lerp(_Transparency, 1, fresnel);
                return fixed4(col.rgb, alpha);
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
}