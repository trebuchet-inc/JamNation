Shader "Custom/WeirdBlack" {

	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Offset ("Offset", Range(0, 1)) = 0.01
		_StrenghR ("StrenghR", float) = 0.01
		_StrenghG ("StrenghG", float) = 0.01
		_StrenghB ("StrenghB", float) = 0.01
	}

	SubShader {
		Cull Off ZWrite Off ZTest Always
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			float _Offset;
			float _StrenghR;
			float _StrenghG;
			float _StrenghB;

			fixed4 frag (v2f_img i) : SV_Target
			{
				float2 uv = i.uv;

				fixed4 c1 = tex2D(_MainTex, uv);
			    uv.x += _Offset;
			    fixed4 c2 = tex2D(_MainTex, uv);
			    uv.y += _Offset;
			    fixed4 c3 = tex2D(_MainTex, uv);
			    uv.x -= _Offset;
			    fixed4 c4 = tex2D(_MainTex, uv);
			    
			    float r = (sin((c1.r + c1.g + c1.b) * _StrenghR) + 1) * 0.5 ;
			    float g = (sin((c2.r + c2.g + c2.b) * _StrenghG) + 1) * 0.5 ;
			    float b = (sin((c4.r + c4.g + c4.b) * _StrenghB) + 1) * 0.5 ;

				fixed4 col = fixed4(r, g, b, 1);

				return col;
			}

		ENDCG
		}
	}
}
