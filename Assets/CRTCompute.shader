Shader "Unlit/CRTCompute"
{
    Properties
    {
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag

            #include "UnityCustomRenderTexture.cginc"

            half4 frag(v2f_customrendertexture In) : SV_Target
            {
                half4 selfRgba = tex2D(_SelfTexture2D, In.globalTexcoord.xy);
                if(selfRgba.r > 0.5 && selfRgba.g < 0.5)
                {
                    selfRgba.r = 0;
                    selfRgba.g = 1;
                }
                return selfRgba;
            }
            ENDCG
        }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            
            #include "UnityCustomRenderTexture.cginc"

            half4 frag(v2f_customrendertexture In) : SV_Target
            {
                return half4(1, 0, 0, 0);
            }
            ENDCG
        }
        
    }
}