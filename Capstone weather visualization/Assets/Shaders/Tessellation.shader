Shader "Tessellation" {
	Properties{
		[HideInInspector]_DrawingTex("Drawing texture", 2D) = "" {}
		_Tess("Tessellation", Range(1,32)) = 4
		_MainTex("Base (RGB)", 2D) = "white" {}
		_DispTex("Disp Texture", 2D) = "gray" {}
		_Displacement("Displacement", Range(0, 1.0)) = 0.3
		_Color("Color", color) = (1,1,1,0)
		_SpecColor("Spec color", color) = (0.5,0.5,0.5,0.5)
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 300

			CGPROGRAM
			#pragma enable_d3d11_debug_symbols
			#pragma surface surf BlinnPhong addshadow fullforwardshadows vertex:disp tessellate:tessFixed nolightmap
			#pragma target 4.6

		struct appdata
		{
			float4 vertex : POSITION;
			float4 tangent : TANGENT;
			float3 normal : NORMAL;
			float2 texcoord : TEXCOORD0;
		};

		float _Tess;

		float4 tessFixed()
		{
			return _Tess;
		}

		sampler2D _DispTex;
		sampler2D _DrawingTex;
		float _Displacement;

		float _X;
		float _Y;
		float _Z;

		void disp(inout appdata v)
		{
			float d = tex2Dlod(_DispTex, float4(v.texcoord.xy,0,0)).r * _Displacement;
			v.vertex.xyz +=  v.normal * d;
		}

		struct Input
		{
			float2 uv_MainTex;
			float2 uv_DrawingTex;
		};

		sampler2D _MainTex;
		fixed4 _Color;

		void surf(Input IN, inout SurfaceOutput o)
		{
			float4 drawData = tex2D(_DrawingTex, IN.uv_DrawingTex);
			float4 mainData = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed4 c = lerp(mainData, drawData, drawData.a);

			o.Albedo = c.rgb;

			c.a = mainData.a;

			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}