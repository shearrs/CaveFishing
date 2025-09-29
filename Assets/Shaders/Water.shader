Shader "Custom/Water"
{
	Properties 
	{
		_Albedo("Albedo", Color) = (1, 1, 1, 1)
		_BaseMap("Base Map", 2D) = "white"
		_WaveSpeed("Wave Speed", float) = 1
		_WaveHeight("Wave Height", float) = 1
		_XScrollingSpeed("X Scrolling Speed", float) = 0.1
		_YScrollingSpeed("Y Scrolling Speed", float) = 0.1
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

			struct Attributes
			{
				float4 positionOS : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct Varyings
			{
				float4 positionHCS : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			half4 _Albedo;
			half _XScrollingSpeed;
			half _YScrollingSpeed;
			half _WaveSpeed;
			half _WaveHeight;
			TEXTURE2D(_BaseMap);
			SAMPLER(sampler_BaseMap);

			CBUFFER_START(UnityPerMaterial)
				float4 _BaseMap_ST;
			CBUFFER_END

			Varyings vert(Attributes IN)
			{
				Varyings OUT;

				half3 objectSpace = IN.positionOS.xyz;
				half offset = length(objectSpace.xz);
				objectSpace.y += _WaveHeight * cos(offset * _WaveSpeed * _Time.y);

				OUT.positionHCS = TransformObjectToHClip(objectSpace);

				OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);

				return OUT;
			}

			half4 frag(Varyings IN) : SV_Target
			{
				half2 uv = IN.uv;
				uv.x += _Time.y * _XScrollingSpeed;
				uv.y += _Time.y * _YScrollingSpeed;

				half4 col = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, uv);
				col *= _Albedo;

				return col;
			}
			ENDHLSL
		}
	}
}