Shader "Custom/Vignette"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _LuminosityAmount("_LuminosityAmount", Range(0,1)) = 1.0
        _VignetteAmount("_VignetteAmount", Range(0,1)) = 1.0
    }
        SubShader
        {
            // No culling or depth
            Cull Off ZWrite Off ZTest Always

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

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                sampler2D _MainTex;

                fixed _LuminosityAmount;
                fixed _VignetteAmount;

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv);

                    float luminosity = 0.299 * col.r + 0.567 * col.g + 0.114 * col.b;
                    col = lerp(col, luminosity, _LuminosityAmount);

                    i.uv *= 1.0f - i.uv.xy;

                    float vignette = ((i.uv.y * i.uv.x) * col.r + (i.uv.y * i.uv.x) * col.g + (i.uv.y * i.uv.x) * col.b) * 5.0f;
                    col = lerp(col, vignette, _VignetteAmount);
                    return col;
                }
                ENDCG
            }
        }
}
