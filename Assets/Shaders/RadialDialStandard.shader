Shader "Custom/RadialDialStandard"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _RimColor ("Rim Color", Color) = (1,1,1,1)
        _RimFill ("Fill Amount", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
        };

        sampler2D _MainTex;
        fixed4 _Color;
        fixed4 _RimColor;
        float _RimFill;

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float adjustedFill = pow(1 - _RimFill, 0.25);
            if (dot(normalize(IN.viewDir), o.Normal) > adjustedFill) o.Albedo = _Color.rgb;
            else o.Albedo = _RimColor.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}