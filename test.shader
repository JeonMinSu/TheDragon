Shader "Custom/test" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		[HDR]
		_Emissioncolor("Emi",Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows


		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};


		float4 _Color;
		float4 _Emissioncolor;



		void surf (Input IN, inout SurfaceOutputStandard o) {

			float4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

			o.Emission = c.rgb * _Emissioncolor;

			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
