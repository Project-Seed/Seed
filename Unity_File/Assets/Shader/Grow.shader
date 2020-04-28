Shader "Custom/Grow"
{
	Properties
	{
		_TileOff("Tiling & Offset",vector) = (1,1,0,0)//
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_MaskTex("MaskTex",2D) = "white"{}
		_Thickness("Thickness",float) = 0 
		_Grow("Grow",Range(-0.5,0.5)) = 0
		_Cutoff("Cutout",Range(0,1)) = 0.5

		_Displacement("DisplacementMap",2D) = "black"{} //
		_Dis("Displacement amount", float) = 0 //
	}
		SubShader
		{
			Tags { "RenderType" = "AlphatestCutout" "Queue" = "Alphatest" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows alphatest:_Cutoff vertex:vert 	
			#pragma target 3.0


			sampler2D _MainTex, _MaskTex;
			sampler2D _Displacement; //
			float _Thickness, _Grow;
			float _Dis;
			float4 _TileOff; //

			struct Input {
				float2 uv_MainTex, uv_MaskTex;
			};

			void vert(inout appdata_full v) {
				float m = tex2Dlod(_MaskTex, float4(v.texcoord.x + _Grow, v.texcoord.y , 0, 0)).r;
				float disp = tex2Dlod(_Displacement, float4(v.texcoord.x * _TileOff.x + _TileOff.z + _Grow, v.texcoord.y * _TileOff.y + _TileOff.w , 0, 0)).r * _Dis; //
				v.vertex.xyz += v.normal * _Thickness * (1 - m) + (disp * v.normal);
			}

			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				fixed4 c = tex2D(_MainTex, float2(IN.uv_MaskTex.x, IN.uv_MaskTex.y));
				//fixed4 c = tex2D(_MainTex, float2(IN.uv_MaskTex.x + _Grow, IN.uv_MaskTex.y));
				float m = tex2D(_MaskTex, float2(IN.uv_MaskTex.x + _Grow,IN.uv_MaskTex.y )).r;
				o.Emission = c.rgb;
				o.Alpha = m;
			}
			ENDCG
		}
	FallBack "Diffuse"
}
