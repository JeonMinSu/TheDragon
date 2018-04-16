Shader "Custom/flow" {
	Properties {
	    _Color ("color", Color)= (1,1,1,1)
		_MainTex ("texture", 2D) = "white" {}
		_MainTex02 ("noise", 2D) = "white" {}
		_NBar("noise bar", Range(0,1)) = 0
	}
	SubShader {
		Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "ForceNoShadowCasting" ="true" "RenderType"="TransparentCutout" }
	
		CGPROGRAM
		
		#pragma surface surf Standard alpha:fade
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MainTex02;
		float _NBar;
		float4 _Color;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MainTex02;
			float4 color:Color;
			
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 d = tex2D (_MainTex02, float2(IN.uv_MainTex.x ,(IN.uv_MainTex.y - _Time.y)));
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex+d.xy*_NBar*2)* _Color;
			o.Emission= IN.color.rgb*c.rgb*2;
			o.Alpha =  IN.color.a*c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
