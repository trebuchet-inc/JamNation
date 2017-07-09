Shader "Custom/Drawing" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Offset ("Offset", Range(0, 1)) = 0.00
		_Silhouette ("Offset", Range(0, 1)) = 0.00
		_LignSensitivity ("LignSensitivity", Range(0, 3)) = 0.00
		_Palier1("Offset", Range(0, 3)) = 0.00
		_Palier2("Offset", Range(0, 3)) = 0.00
		_Palier3("Offset", Range(0, 3)) = 0.00
		_Palier4("Offset", Range(0, 3)) = 0.00
		_Mix("Mix", Range(0, 1)) = 1.00
		_Saturation("Saturation", float) = 1.00
		_Threshold("Threshold", Range(0, 1)) = 1.00
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
			float _Offset;
			float _Silhouette;
			float _LignSensitivity;
			float _Palier1;
			float _Palier2;
			float _Palier3;
			float _Palier4;
			float _Mix;
			float _Saturation;
			float _Threshold;

			fixed4 frag (v2f_img i) : SV_Target
			{
				float2 uv = i.uv;

				fixed4 c1 = tex2D(_MainTex, uv);
			    uv.x += _Offset;
			    fixed4 c2 = tex2D(_MainTex, uv);
			    uv.y += _Offset;
			    uv.x -= _Offset;
			    fixed4 c3 = tex2D(_MainTex, uv);
			    uv.y -= _Offset;
			    uv.x -= _Offset;
			    fixed4 c4 = tex2D(_MainTex, uv);
			    uv.y += _Offset;
			    uv.x += _Offset;
			    fixed4 c5 = tex2D(_MainTex, uv);

			    float fc1 = (c1.r * 0.3 + c1.g * 0.59 + c1.b * 0.11) * 3;
  	 			float fc2 = (c2.r * 0.3 + c2.g * 0.59 + c2.b * 0.11) * 3;
   				float fc3 = (c3.r * 0.3 + c3.g * 0.59 + c3.b * 0.11) * 3;
   				float fc4 = (c4.r * 0.3 + c4.g * 0.59 + c4.b * 0.11) * 3;
   				float fc5 = (c5.r * 0.3 + c5.g * 0.59 + c5.b * 0.11) * 3;

   				half4 c;

   				if(fc1 > _Palier1){
			        c = 1.0 * c1;
			    }
			    
			    else if(fc1 > _Palier2){
			     	c = 0.8 * c1;
			    } 
			    
			    else if(fc1 > _Palier3){
			     	c = 0.6 * c1;
			    }
			    
			    else if(fc1 > _Palier4){
			     	c = 0.4 * c1;
			    }
			    
			    else{
			      	c = 0.1 * c1;  
			  	}

				c = c * _Mix + _Color * (1 - _Mix);
						    
			    if(abs(fc1 - fc2) > _LignSensitivity){
			     	c -= half4(_Silhouette,_Silhouette,_Silhouette,1);   
			    }
			    if(abs(fc1 - fc3) > _LignSensitivity){
			    	c -= half4(_Silhouette,_Silhouette,_Silhouette,1);   
			    }
			    if(abs(fc1 - fc4) > _LignSensitivity){
			     	c -= half4(_Silhouette,_Silhouette,_Silhouette,1);   
			    }
			    if(abs(fc1 - fc5) > _LignSensitivity){
			    	c -= half4(_Silhouette,_Silhouette,_Silhouette,1);   
			    }

			    if(abs(c1.r - c1.g) > 0.01 && abs(c1.r - c1.b) > 0.01){
				    if(c1.r > _Threshold){c.r *= _Saturation;}
				    if(c1.g > _Threshold){c.g *= _Saturation;}
				    if(c1.b > _Threshold){c.b *= _Saturation;}
			    }

				return c;
			}

		ENDCG
		}
	}
}
