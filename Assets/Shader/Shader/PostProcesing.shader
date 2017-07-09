Shader "Hidden/PostProcessing"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Param("Param", float) = 1
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			float _Param;

			fixed4 frag (v2f_img i) : SV_Target
			{
				float2 uv = i.uv;

				float size = 8.0 + 64.0 * _Param;

				uv = floor(uv * size) / size;

				fixed4 col = tex2D(_MainTex, uv);

				return col;
			}
			ENDCG
		}
	}
}