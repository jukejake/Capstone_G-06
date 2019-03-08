Shader "Custom/SnowShaderTransparent"
{
	Properties
	{
		[HideInInspector]_DrawingTex("Drawing texture", 2D) = "" {}
		[HideInInspector]_EnableSnow("Float ", Range(0,1)) = 0.0
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_SnowTex("Snow Texture", 2D) = "white" {}
		_SnowColor("Snow Color", Color) = (1.0,1.0,1.0,1.0)
		_SnowRange("Snow Range", Range(-1,1)) = 0.0
		_Glossiness("Smoothness", Range(0,1)) = 0.5
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"  }
		LOD 300

		ZWrite Off
		Cull Back
		Lighting On
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf BlinnPhong decal:add nolightmap
		#pragma vertex vert
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _DrawingTex;
		sampler2D _MainTex;
		sampler2D _SnowTex;
		sampler2D _NormalTex;
		
		struct Input
		 {
			float2 uv_DrawingTex;
			float2 uv_MainTex;
			float2 uv_SnowTex;
		};

		float _SnowRange;
		float4 _SnowColor;

		float _EnableSnow;

		half _Glossiness;
		fixed4 _Color;

		void vert(inout appdata_full v)
		{
			//Convert the normal to world coortinates
			float3 snormal = normalize(float3(-2, 1, 0).xyz);
			float3 sn = mul((float3x3)unity_WorldToObject, snormal).xyz;
			float4 tex = tex2Dlod(_DrawingTex, float4(v.texcoord.xy, 0, 0));

		}

		void surf(Input IN, inout SurfaceOutput o)
		{
			float4 drawData = tex2D(_DrawingTex, IN.uv_DrawingTex);
			
			float4 mainData = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			float4 snowData = tex2D(_SnowTex, IN.uv_SnowTex);
			fixed4 c = lerp(mainData, drawData, drawData.a);

			o.Albedo = c.rgb;

			c.a = drawData.a + mainData.a + snowData.a;

			// Metallic and smoothness come from slider variables
			//o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			//o.Normal = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex));
		}
		ENDCG
	}
	FallBack "Diffuse"
}
