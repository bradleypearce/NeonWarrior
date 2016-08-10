Shader "ScreenDistortion"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DisplacementTex("Displacement Texture", 2D) = "white" {}
		_Strength ("Strength", float) = 1
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

			
			uniform sampler2D _MainTex; 
			uniform sampler2D _DisplacementTex;
			
			fixed _Strength;

			v2f_img vert (appdata_base v) 
			{
   			 	v2f_img o;
    			o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
    			o.uv = v.texcoord;
   				return o;
			}

			float4 frag (v2f_img i) : COLOR
			{
				half2 n = tex2D(_DisplacementTex, i.uv);
				half2 d = n * 2 - 1;
				i.uv += d * _Strength;
				i.uv = saturate(i.uv);

				float4 c = tex2D(_MainTex, i.uv);
				return c;
			}
			ENDCG
		}
	}
}
