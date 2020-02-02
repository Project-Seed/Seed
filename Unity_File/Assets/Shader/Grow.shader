Shader "Custom/Grow"
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_MaskTex("MaskTex",2D) = "white"{}
		_Thickness("Thickness",float) = 0 
		_Grow("Grow",Range(-0.5,0.5)) = 0

		_Cutoff("Cutout",Range(0,1)) = 0.5
	}
		SubShader
		{
			Tags { "RenderType" = "AlphatestCutout" "Queue" = "Alphatest" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows alphatest:_Cutoff vertex:vert 	
			#pragma target 3.0


			sampler2D _MainTex, _MaskTex;
			float _Thickness, _Grow;

			struct Input {
				float2 uv_MainTex, uv_MaskTex;
			};

			void vert(inout appdata_full v) {
				float m = tex2Dlod(_MaskTex, float4(v.texcoord.x + _Grow, v.texcoord.y , 0, 0)).r;
				v.vertex.xyz += v.normal * _Thickness * (1 - m);
			}

			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
				float m = tex2D(_MaskTex, float2(IN.uv_MaskTex.x + _Grow,IN.uv_MaskTex.y )).r;
				o.Emission = c.rgb;
				o.Alpha = m;
			}
			ENDCG
		}
	FallBack "Diffuse"
}
