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

			struct Attributes
			{
				half4 positionOS : POSITION;
				half2 uv : TEXCOORD0;
				half4 color : COLOR0;
			};

			struct Varyings
			{
				half4 positionHCS : SV_POSITION;
				half3 positionWS : TEXCOORD2;
				half2 uv : TEXCOORD0;
				half4 color: TEXCOORD1;
			};

			half4 _Albedo;
			TEXTURE2D(_BaseMap);
			SAMPLER(sampler_BaseMap);

			CBUFFER_START(UnityPerMaterial)
				half4 _BaseMap_ST;
			CBUFFER_END

			Varyings vert(Attributes IN)
			{
				Varyings OUT;
				VertexPositionInputs positions = GetVertexPositionInputs(IN.positionOS);

				OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
				OUT.positionWS = positions.positionWS.xyz;
				OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
				OUT.color = IN.color;

				return OUT;
			}

			void applyFog(inout half4 color, half3 positionWS)
			{
				float4 inColor = color;

				#if defined(FOG_LINEAR) || defined(FOG_EXP) || defined(FOG_EXP2)
				half viewZ = -TransformWorldToView(positionWS).z;
				half nearZToFarZ = max(viewZ - _ProjectionParams.y, 0);
				half density = 1.0f - ComputeFogIntensity(ComputeFogFactorZ0ToFar(nearZToFarZ));
				
				color = lerp(color, unity_FogColor, density);
				#else
				color = color;
				#endif
			}

			half4 frag(Varyings IN) : SV_Target
			{
				half2 uv = IN.uv;
				half4 col = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, uv);
				col *= _Albedo * IN.color;
				applyFog(col, IN.positionWS);

				return col;
			}
			ENDHLSL
		}
	}
}