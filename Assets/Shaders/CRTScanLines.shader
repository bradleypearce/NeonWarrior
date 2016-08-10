Shader "CRTScanLines"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_MaskTex ("Mask texture", 2D) = "white" {}
		_maskBlend ("Mask blending", Float) = 0.5
		_maskSize ("Mask Size", Float) = 1

	}
     
	SubShader
	{
		Pass
		{
		    ZTest Always 
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			uniform sampler2D _MainTex; 
			uniform sampler2D _MaskTex;
			
			fixed _maskBlend; 
			fixed _maskSize;
			
			v2f_img vert (appdata_base v) 
			{
   			 	v2f_img o;
    			o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
    			o.uv = v.texcoord;
   				return o;
			}
		
			fixed4 frag (v2f_img i) : COLOR
			{
				fixed4 mask = tex2D(_MaskTex, i.uv * _maskSize);
				fixed4 base = tex2D(_MainTex, i.uv);
				return lerp(base, mask, _maskBlend);
			}
			ENDCG
		}
	}
	Fallback off 
}
