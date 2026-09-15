Shader "Unlit/GlassPlus"
{
    Properties{
        _Color("Main Tint",Color) = (1,1,1,1)
        _MainTex("Main Tex",2D) = "white"{}
        _VerticalScale("Vertical Scale",Range(0,1)) = 0.5
        _OutlineColor("OutlineColor",Color) =(1,1,1,1)
        _OutlinePower("OutlinePower",Range(0,1)) = 0.0
    }
    SubShader{
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        pass{
             Tags { "LightMode"="ForwardBase"}
             ZWrite Off
             Blend SrcAlpha OneMinusSrcAlpha
        CGPROGRAM
             #include "Lighting.cginc" 
             #pragma vertex vert
             #pragma fragment frag

            float4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed _VerticalScale;
            float4 _OutlineColor;
            float _OutlinePower;

             struct a2v
            {
                float4 vertex : POSITION;    
                float3 normal : NORMAL;        
                float4 texcoord : TEXCOORD0;   
            };

            struct v2f
            {
                float4 uv : TEXCOORD0;
                float4 pos : SV_POSITION; 
                float3 worldNormal : NORMAL;  
                float3 worldPos : TEXCOORD1;
            };

            v2f vert(a2v v){
                v2f o;

                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld,v.vertex).xyz;
                o.uv.xy = TRANSFORM_TEX(v.texcoord,_MainTex);
                return o;
            }

            fixed4 frag(v2f i):SV_TARGET{

                fixed3 worldNormal = normalize(i.worldNormal);
                fixed3 viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
                fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
                fixed4 texColor = tex2D(_MainTex,i.uv.xy);

                fixed3 albedo = texColor.rgb * _Color.rgb;
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo * _OutlineColor.rgb;
                fixed3 diffuse = _LightColor0.rgb * albedo * max(0,dot(worldNormal,worldLightDir));
                fixed A = _VerticalScale + (1-_VerticalScale)*pow((1-max(dot(worldNormal,viewDir),0)),5);
                return fixed4(ambient+diffuse,texColor.a * A);
            }
            ENDCG 
        }
    }
    Fallback "Transparewnt/VertexLit"
}