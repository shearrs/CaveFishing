Shader "Custom/Vertex Unlit"
{
	Properties 
	{
		_Albedo("Albedo", Color) = (1, 1, 1, 1)
		_BaseMap("Base Map", 2D) = "white"
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderVariablesFunctions.hlsl"

			struct Attributes
			{
				float4 positionOS : POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR0;
			};

			struct Varyings
			{
				float4 positionHCS : SV_POSITION;
				float3 positionWS : TEXCOORD2;
				float2 uv : TEXCOORD0;
				float4 color: TEXCOORD1;
			};

			float4 _Albedo;
			TEXTURE2D(_BaseMap);
			SAMPLER(sampler_BaseMap);

			CBUFFER_START(UnityPerMaterial)
				float4 _BaseMap_ST;
			CBUFFER_END

			Varyings vert(Attributes IN)
			{
				Varyings OUT;
				VertexPositionInputs positions = GetVertexPositionInputs(IN.positionOS.xyz);

				OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
				OUT.positionWS = positions.positionWS.xyz;
				OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
				OUT.color = IN.color;

				return OUT;
			}

			half4 frag(Varyings IN) : SV_Target
			{
				float2 uv = IN.uv;
				float4 col = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, uv);
				col *= _Albedo * IN.color;

				return col;
			}
			ENDHLSL
		}
	}
}