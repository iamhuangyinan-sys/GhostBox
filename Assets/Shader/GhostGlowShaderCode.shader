// Unity Shader for a Ghostly Glow Effect
// Target: Universal Render Pipeline (URP)
Shader "Unlit/GhostGlowURP"
{
    // 1. 属性 (Properties)
    // 这些变量会显示在材质的 Inspector 面板中
    Properties
    {
        _BaseColor("Base Color", Color) = (0.8, 0.9, 1, 1)
        _GlowColor("Glow Color (Emission)", Color) = (0.3, 0.7, 1, 1)
        [HDR] _GlowPower("Glow Power", Range(0.1, 8.0)) = 2.5
        _Alpha("Transparency", Range(0.0, 1.0)) = 0.6
    }

    // 2. 子着色器 (SubShader)
    // 包含实际的渲染逻辑
    SubShader
    {
        // 标签 (Tags) - 告诉 Unity 如何以及何时渲染这个着色器
        // 必须为 URP 设置正确的 RenderPipeline 标签
        Tags { "RenderPipeline" = "UniversalPipeline" "RenderType"="Transparent" "Queue"="Transparent" }

        // 渲染通道 (Pass)
        Pass
        {
            // 混合模式 (Blending) - 实现半透明效果
            Blend SrcAlpha OneMinusSrcAlpha
            // 关闭深度写入 - 透明物体通常不写入深度缓冲
            ZWrite Off
            // 关闭剔除 - 让物体的背面也可见，幽灵效果更好
            Cull Off

            // HLSL 代码块开始
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // 包含 URP 的核心库文件，里面有许多必需的函数和结构体
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // 定义 CBUFFER 来匹配 Properties 中的变量
            // 这是 HLSL 访问材质属性的方式
            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                half4 _GlowColor;
                half _GlowPower;
                half _Alpha;
            CBUFFER_END

            // 顶点着色器的输入结构 (appdata)
            struct Attributes
            {
                float4 positionOS   : POSITION; // 物体空间顶点位置
                float3 normalOS     : NORMAL;   // 物体空间法线
            };

            // 顶点着色器到片元着色器的输出结构 (v2f)
            struct Varyings
            {
                float4 positionCS   : SV_POSITION; // 裁剪空间顶点位置
                float3 normalWS     : TEXCOORD0;   // 世界空间法线
                float3 viewDirWS    : TEXCOORD1;   // 世界空间视角方向
            };

            // 3. 顶点着色器 (Vertex Shader)
            // 处理每个顶点，计算其最终位置和需要传递给片元着色器的数据
            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                // 将顶点位置从物体空间转换到世界空间，再到裁剪空间
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);

                // 获取世界空间中的顶点位置和法线
                float3 positionWS = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                
                // 计算从顶点指向摄像机的“视角方向”向量
                OUT.viewDirWS = GetCameraPositionWS() - positionWS;

                return OUT;
            }

            // 4. 片元着色器 (Fragment Shader)
            // 处理每个像素，计算其最终颜色
            half4 frag(Varyings IN) : SV_Target
            {
                // 归一化（Normalize）向量，确保它们是单位长度，这对于点积计算至关重要
                IN.normalWS = normalize(IN.normalWS);
                IN.viewDirWS = normalize(IN.viewDirWS);

                // --- 菲涅尔效应计算 ---
                // 计算法线和视角方向的点积。saturate 将结果限制在 [0, 1] 范围
                half dotProduct = dot(IN.normalWS, IN.viewDirWS);
                // 边缘处点积接近0，中心处接近1。我们用 1-dot 来获取边缘
                half fresnel = 1.0 - saturate(dotProduct);
                // 使用 pow 函数和 _GlowPower 来控制菲涅尔辉光的宽度和强度
                fresnel = pow(fresnel, _GlowPower);

                // 最终的自发光颜色 = 辉光颜色 * 菲涅尔效应强度
                half3 emission = _GlowColor.rgb * fresnel;

                // 最终颜色 = 物体基础颜色 + 自发光颜色
                // 这是一个加法混合，使辉光“叠加”在基础颜色之上
                half3 finalColor = _BaseColor.rgb + emission;

                // 返回最终的 RGBA 颜色
                // RGB 是我们计算出的颜色，Alpha 来自属性
                return half4(finalColor, _Alpha);
            }

            ENDHLSL
        }
    }
}