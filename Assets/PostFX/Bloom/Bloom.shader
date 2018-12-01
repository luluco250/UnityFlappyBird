Shader "Hidden/Bloom" {
	Properties {
		_MainTex("", 2D) = "black" {}
		_Intensity("", Float) = 1.0
		_Contrast("", Float) = 1.0
		_Scale("", Float) = 1.0
	}

	SubShader { Cull Off ZWrite Off ZTest Always
		CGINCLUDE

		sampler2D _MainTex;
		float _Intensity, _Contrast, _Scale;

		void VS_PostProcess(
			float3 vertex : POSITION,
			out float4 position : SV_POSITION,
			inout float2 texcoord : TEXCOORD
		) {
			position = UnityObjectToClipPos(vertex);
		}

		float3 blend_screen(float3 a, float3 b, float w) {
			return lerp(a, 1.0 - (1.0 - a) * (1.0 - b), w);
		}

		float3 rgb2hsv(float3 c) {
			float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
			float4 p = c.g < c.b ? float4(c.bg, K.wz) : float4(c.gb, K.xy);
    		float4 q = c.r < p.x ? float4(p.xyw, c.r) : float4(c.r, p.yzx);

			float d = q.x - min(q.w, q.y);
			float e = 1.0e-10;
			return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
		}

		float3 hsv2rgb(float3 c) {
			float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
			float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
			return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
		}

		float4 PS_Bloom(
			float4 p : SV_POSITION,
			float2 uv : TEXCOORD
		) : SV_TARGET {
			float3 color = tex2D(_MainTex, uv).rgb;
			
			float3 hsv = hsv2rgb(color);

			hsv.x = sin(_Time.y * 10.0) * 0.5 + 0.5;
			hsv.z *= 2.5;

			color = rgb2hsv(hsv);

			color = dot(color, 0.333);


			return float4(color, 1.0);

			/*

			static const float3x3 cKernel = float3x3(
				1, 2, 1,
				2, 4, 2,
				1, 2, 1
			);
			static const float cSum = 16;
			static const float cInvSum = 1.0 / cSum;
			static const float2 cPS = 1.0 / _ScreenParams.xy * _Scale;

			float3 color = tex2D(_MainTex, uv).rgb;

			float3 blurred = color * cKernel[1][1];

			// Top
			blurred += tex2D(_MainTex, uv + float2(-cPS.x, cPS.y)).rgb * cKernel[0][0];
			blurred += tex2D(_MainTex, uv + float2(0, cPS.y)).rgb * cKernel[1][0];
			blurred += tex2D(_MainTex, uv + float2(cPS.x, cPS.y)).rgb * cKernel[2][0];

			// Middle
			blurred += tex2D(_MainTex, uv + float2(-cPS.x, 0)).rgb * cKernel[0][1];
			blurred += tex2D(_MainTex, uv + float2(cPS.x, 0)).rgb * cKernel[2][1];

			// Bottom
			blurred += tex2D(_MainTex, uv + float2(-cPS.x, -cPS.y)).rgb * cKernel[0][2];
			blurred += tex2D(_MainTex, uv + float2(0, -cPS.y)).rgb * cKernel[1][2];
			blurred += tex2D(_MainTex, uv + float2(cPS.x, -cPS.y)).rgb * cKernel[2][2];

			blurred *= cInvSum;

			//color += pow(blurred, _Contrast) * _Intensity;
			color = blend_screen(color, pow(blurred, _Contrast), _Intensity);

			return float4(color, 1.0);

			*/
		}

		ENDCG

		Pass { // Bloom
			CGPROGRAM
			#pragma vertex VS_PostProcess
			#pragma fragment PS_Bloom
			ENDCG
		}
	}
}