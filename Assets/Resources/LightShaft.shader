Shader "Custom/LightShaft"
{
	Properties
	{
		_Albedo("Albedo", Color) = (1, 1, 1, 1)
		_AlphaTop("Alpha Top", Range(0.01, 1)) = 0.8
		_AlphaBottom("Alpha Bottom", Range(0.01, 1)) = 0.5
		_AlphaOffset("Alpha Offset", float) = 3.14159
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
				float3 normalOS : NORMAL;
			};

			struct Varyings
			{
				float2 uv : TEXCOORD0;
				float4 positionHCS : SV_POSITION;
				float3 normalOS : NORMAL;
			};

			half4 _Albedo;
			half _AlphaTop;
			half _AlphaBottom;
			half _AlphaOffset;

			Varyings vert(Attributes IN)
			{
				Varyings OUT;

				half3 objectPosition = IN.positionOS.xyz;
				OUT.positionHCS = TransformObjectToHClip(objectPosition);

				OUT.uv = IN.uv;
				OUT.normalOS = IN.normalOS;

				return OUT;
			}

			half4 frag(Varyings IN) : SV_TARGET
			{
				half2 uv = IN.uv;

				half normalMag = abs(IN.normalOS.y);

				clip(0 - normalMag);

				half yPos = cos(3.14159 * uv.y - _AlphaOffset/2);
				half alpha1 = smoothstep(_AlphaBottom, _AlphaTop, yPos);

				half4 col = _Albedo * alpha1;

				return col;
			}
			ENDHLSL
		}
	}
}