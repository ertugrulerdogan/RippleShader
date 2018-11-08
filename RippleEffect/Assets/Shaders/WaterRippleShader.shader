// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/WaterRippleShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

		_Scale("Scale",float) =1
		_Speed("Speed",float) =1
		_Frequency("Frequency",float) =1
    }
    SubShader
    {
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		float _Scale, _Speed, _Frequency;

        struct Input
        {
            float2 uv_MainTex;
			float3 customValue;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

		float _WaveAmplitude1, _WaveAmplitude2, _WaveAmplitude3, _WaveAmplitude4, _WaveAmplitude5, _WaveAmplitude6, _WaveAmplitude7, _WaveAmplitude8;
		float _OffsetX1, _OffsetZ1, _OffsetX2, _OffsetZ2, _OffsetX3, _OffsetZ3, _OffsetX4, _OffsetZ4, _OffsetX5, _OffsetZ5, _OffsetX6, _OffsetZ6, _OffsetX7, _OffsetZ7, _OffsetX8, _OffsetZ8;
		float _Distance1, _Distance2, _Distance3, _Distance4, _Distance5, _Distance6, _Distance7, _Distance8;
		float _xImpact1, _zImpact1, _xImpact2, _zImpact2, _xImpact3, _zImpact3, _xImpact4, _zImpact4, _xImpact5, _zImpact5, _xImpact6, _zImpact6,
			_xImpact7, _zImpact7, _xImpact8, _zImpact8;

        UNITY_INSTANCING_BUFFER_START(Props)

        UNITY_INSTANCING_BUFFER_END(Props)

		void vert(inout appdata_full v, out Input o) 
		{
			UNITY_INITIALIZE_OUTPUT(Input, o)
			half offsetvert = ((v.vertex.x * v.vertex.x) + (v.vertex.z  * v.vertex.z));

			half value1 = _Scale * sin(_Time.w * _Speed * _Frequency + offsetvert + (v.vertex.x * _OffsetX1) + (v.vertex.z * _OffsetZ1));
			half value2 = _Scale * sin(_Time.w * _Speed * _Frequency + offsetvert + (v.vertex.x * _OffsetX2) + (v.vertex.z * _OffsetZ2));
			half value3 = _Scale * sin(_Time.w * _Speed * _Frequency + offsetvert + (v.vertex.x * _OffsetX3) + (v.vertex.z * _OffsetZ3));
			half value4 = _Scale * sin(_Time.w * _Speed * _Frequency + offsetvert + (v.vertex.x * _OffsetX4) + (v.vertex.z * _OffsetZ4));
			half value5 = _Scale * sin(_Time.w * _Speed * _Frequency + offsetvert + (v.vertex.x * _OffsetX5) + (v.vertex.z * _OffsetZ5));
			half value6 = _Scale * sin(_Time.w * _Speed * _Frequency + offsetvert + (v.vertex.x * _OffsetX6) + (v.vertex.z * _OffsetZ6));
			half value7 = _Scale * sin(_Time.w * _Speed * _Frequency + offsetvert + (v.vertex.x * _OffsetX7) + (v.vertex.z * _OffsetZ7));
			half value8 = _Scale * sin(_Time.w * _Speed * _Frequency + offsetvert + (v.vertex.x * _OffsetX8) + (v.vertex.z * _OffsetZ8));
			
			float3 worldpos = mul(unity_ObjectToWorld, v.vertex).xyz;

			if (sqrt(pow(worldpos.x - _xImpact1, 2) + pow(worldpos.z - _zImpact1, 2)) < _Distance1) 
			{
				v.vertex.y += value1 * _WaveAmplitude1;
				o.customValue += value1 * _WaveAmplitude1;
			}
			if (sqrt(pow(worldpos.x - _xImpact2, 2) + pow(worldpos.z - _zImpact2, 2)) < _Distance2)
			{
				v.vertex.y += value2 * _WaveAmplitude2;
				o.customValue += value2 * _WaveAmplitude2;
			}
			if (sqrt(pow(worldpos.x - _xImpact3, 2) + pow(worldpos.z - _zImpact3, 2)) < _Distance3)
			{
				v.vertex.y += value3 * _WaveAmplitude3;
				o.customValue += value3 * _WaveAmplitude3;
			}
			if (sqrt(pow(worldpos.x - _xImpact4, 2) + pow(worldpos.z - _zImpact4, 2)) < _Distance4)
			{
				v.vertex.y += value4 * _WaveAmplitude4;
				o.customValue += value4 * _WaveAmplitude4;
			}
			if (sqrt(pow(worldpos.x - _xImpact5, 2) + pow(worldpos.z - _zImpact5, 2)) < _Distance5)
			{
				v.vertex.y += value5 * _WaveAmplitude5;
				o.customValue += value5 * _WaveAmplitude5;
			}
			if (sqrt(pow(worldpos.x - _xImpact6, 2) + pow(worldpos.z - _zImpact6, 2)) < _Distance6)
			{
				v.vertex.y += value6 * _WaveAmplitude6;
				o.customValue += value6 * _WaveAmplitude6;
			}
			if (sqrt(pow(worldpos.x - _xImpact7, 2) + pow(worldpos.z - _zImpact7, 2)) < _Distance7)
			{
				v.vertex.y += value7 * _WaveAmplitude7;
				o.customValue += value7 * _WaveAmplitude7;
			}
			if (sqrt(pow(worldpos.x - _xImpact8, 2) + pow(worldpos.z - _zImpact8, 2)) < _Distance8)
			{
				v.vertex.y += value8 * _WaveAmplitude8;
				o.customValue += value8 * _WaveAmplitude8;
			}
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color ;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
			o.Normal.y = IN.customValue;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
