Shader "Unlit/ObjSelected"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Transparency("Transparency", Range(0,1)) = 0.5
        _BlueTint("Blue Tint", Color) = (0.2, 0.4, 1.0, 1)
        _FresnelPower("Fresnel Power", Range(0.1, 10)) = 3.0
        _FresnelIntensity("Fresnel Intensity", Range(0, 2)) = 1.0
        _EdgeBrightness("Edge Brightness", Range(0, 5)) = 2.0
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
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Transparency;
            float4 _BlueTint;
            float _FresnelPower;
            float _FresnelIntensity;
            float _EdgeBrightness;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.viewDir = normalize(_WorldSpaceCameraPos.xyz - worldPos);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb = _BlueTint.rgb;
                float fresnel = saturate(1.0 - dot(normalize(i.normal), normalize(i.viewDir)));
                fresnel = pow(fresnel, _FresnelPower) * _FresnelIntensity;
                float edge = fresnel * _EdgeBrightness;
                col.rgb += edge * float3(0.1, 0.2, 0.5);
                float baseAlpha = col.a * _Transparency;
                col.a = baseAlpha + (1.0 - baseAlpha) * fresnel;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
}