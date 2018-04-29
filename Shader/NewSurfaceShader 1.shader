Shader "Custom/NewSurfaceShader 1" {
	Properties {
		[HDR]_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Blink ( " 반짝", Range(0,5)) = 0

	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		LOD 200

		blend SrcAlpha One
		zwrite off

		CGPROGRAM
		#pragma surface surf Lambert keepalpha 

		#pragma target 3.0



		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
			float3 worldNormal;
		};


		fixed4 _Color;
		float _Blink;



		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			//fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			
			float rim = dot(IN.viewDir, IN.worldNormal);
			rim = abs(rim);
			rim = pow(rim, 4);
			rim = saturate(rim) * cos(_Time.y * _Blink);
			o.Emission = _Color * rim;

			o.Alpha = rim;
		}
		ENDCG
	}
	FallBack "Transparent"
}
