Shader "Custom/Outline"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (0,1,0,1)
        _OutlineWidth ("Outline Width", Range (0, 10)) = 0.5
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
        }

        ZWrite Off
        Cull Off
        ZTest LEqual
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "OUTLINE"

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            fixed4 _OutlineColor;
            float _OutlineWidth;

            v2f vert(appdata v)
            {
                v2f o;

                // Posição normal do vértice em clip space
                float4 clipPos = UnityObjectToClipPos(v.vertex);

                // Normal transformada para clip space
                float3 normalHCS =
                    mul((float3x3)UNITY_MATRIX_VP,
                        mul((float3x3)UNITY_MATRIX_M,
                            v.normal));

                // Expansão em relação ao espaço da tela
                float2 offset =
                    normalize(normalHCS.xy)
                    / _ScreenParams.xy
                    * clipPos.w
                    * _OutlineWidth
                    * 2.0;

                clipPos.xy += offset;

                o.vertex = clipPos;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _OutlineColor;
            }

            ENDCG
        }
    }
}