Shader "Custom/LightShaft"
{
	Properties
	{
		_Albedo("Albedo", Color) = (1, 1, 1, 1)
		_AlphaHeight("Alpha Height", float) = 0.2
		_AlphaBlending("Alpha Blending", float) = 0.5
	}

	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "RenderPipeline" = "UniversalRenderPipeline" }

		ZWrite Off
		Blend One One
		Cull Off

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

			struct Attributes
			{
				float2 uv : TEXCOORD0;
				float4 positionOS : POSITION;
			};

			struct Varyings
			{
				float2 uv : TEXCOORD0;
				float4 positionHCS : SV_POSITION;
				float3 positionOS : TEXCOORD1;
			};

			half4 _Albedo;
			half _AlphaHeight;
			half _AlphaBlending;

			Varyings vert(Attributes IN)
			{
				Varyings OUT;

				half3 objectPosition = IN.positionOS.xyz;
				OUT.positionHCS = TransformObjectToHClip(objectPosition);

				OUT.positionOS = IN.positionOS.xyz;

				return OUT;
			}

			half4 frag(Varyings IN) : SV_TARGET
			{
				half2 uv = IN.uv;
				float alpha = smoothstep(_AlphaHeight, _AlphaBlending, IN.positionOS.y);

				half4 col = _Albedo * alpha;

				return col;
			}
			ENDHLSL
		}
	}
}