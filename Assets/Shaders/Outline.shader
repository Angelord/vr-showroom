Shader "Custom/Outline"
{
    Properties
    {
        _CurrentAlbedo ("Albedo (RGB)", 2D) = "white" {}
        _TargetAlbedo ("Target Albedo (RGB)", 2D) = "white" {}
        _AlbedoBlend("Albedo Blenb", Range(0, 1)) = 0.0
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        [MaterialToggle][PerRendererData] _OutlineEnabled("Outline Enabled", Float) = 0
    }
    SubShader
    {
        LOD 200

        Pass
        {

            Name "Outline"
            Cull Front
            ZTest [_ZTest]
            
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"
 
                static const float OUTLINE_WIDTH = 0.005;
                const fixed4 OUTLINE_COLOR = fixed4(1, 1, 1, 1);
                
                half _OutlineEnabled;
 
                struct v2f {
                    float4 pos          : POSITION;
                };
 
                v2f vert (appdata_full v)
                {
                    v2f output;
                    float3 viewPosition = UnityObjectToViewPos(v.vertex);
                    float3 viewNormal = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
                    output.pos = UnityViewToClipPos(viewPosition + viewNormal * OUTLINE_WIDTH * _OutlineEnabled);
                    return output;
                }
 
                half4 frag( v2f i ) : COLOR
                {
                    return OUTLINE_COLOR;
                }
            ENDCG          
        }
        
        
        
        Tags { "RenderType"="Opaque" }
        Cull Off
        CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma surface surf Standard fullforwardshadows
    
            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0
    
            sampler2D _CurrentAlbedo;
            sampler2D _TargetAlbedo;
    
            struct Input
            {
                float2 uv_MainTex;
            };
    
            half _AlbedoBlend;
            half _Glossiness;
            half _Metallic;
    
            // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
            // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
            // #pragma instancing_options assumeuniformscaling
            UNITY_INSTANCING_BUFFER_START(Props)
                // put more per-instance properties here
            UNITY_INSTANCING_BUFFER_END(Props)
    
            void surf (Input IN, inout SurfaceOutputStandard o)
            {
                // Albedo comes from a texture tinted by color
                fixed4 cCurAl = tex2D (_CurrentAlbedo, IN.uv_MainTex);
                fixed4 cTarAl = tex2D (_TargetAlbedo, IN.uv_MainTex);
                
                fixed4 c = cTarAl * _AlbedoBlend + cCurAl * (1.0 - _AlbedoBlend); 
                o.Albedo = c.rgb;
                // Metallic and smoothness come from slider variables
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Alpha = c.a;
            }
        ENDCG
    }
    FallBack "Diffuse"
}
