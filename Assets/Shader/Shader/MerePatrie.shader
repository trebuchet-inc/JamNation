Shader "Custom/MerePatrie" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color)  = (1,0,0,1)
		_Min ("Min", Range(0, 1)) = 0.00
		_Moy ("Moy", Range(0, 1)) = 0.00
		_Max ("Max", Range(0, 1)) = 0.00
		_Offset ("Offset", Range(0, 1)) = 0.1
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
			half4 _Color;
			float _Min;
			float _Max;
			float _Moy;
			float _Offset;

			fixed4 frag (v2f_img i) : SV_Target
			{
				float2 uv;
				float2 BaseUv = i.uv;
				float xCoord;
				float yCoord;
				int MinCount = 0;
				int MoyBisCount = 0;
				int MoyCount = 0;
				int MaxCount = 0;

				fixed4 c;
				float intensity;
				fixed4 col;

				for(int i = 0; i < 25; i++){
					xCoord = ((i%5) * _Offset) - (2*_Offset);
					yCoord = ((i/5) * _Offset) - (2*_Offset);
					uv = BaseUv;
					uv.x += xCoord;
					uv.y += yCoord;
					c = tex2D(_MainTex, uv);
					intensity = (c.r * 0.3 + c.g * 0.59 + c.b * 0.11) ;

					if(intensity < _Min){
						MinCount ++;
					}
					else if(intensity < _Moy){
						MoyCount ++;
					}
					else if(intensity < _Max){
						MoyBisCount ++;
					}
					else{
						MaxCount ++;
					}
				}

				if(MinCount > 12){
					col = fixed4(0,0,0,1);
				}
				else if(MoyCount > 12){
					col = _Color * 0.5f;
				}
				else if(MoyBisCount > 12){
					col = _Color;
				}
				else {
					col = fixed4(1,1,1,1);
				}

				return col;
			}

		ENDCG
		}
	}
}
