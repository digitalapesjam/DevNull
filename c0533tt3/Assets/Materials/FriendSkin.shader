Shader "Custom/FriendSkin" {
	Properties {
		_Face ("Face", 2D) = "white" { }
		_RandomSeed ("RandomSeed",float) = 0 
	}
SubShader { // Unity chooses the subshader that fits the GPU best
      Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
      ZWrite Off
      Blend SrcAlpha OneMinusSrcAlpha
      Pass { // some shaders require multiple passes
      
         CGPROGRAM // here begins the part in Unity's Cg
 
         #pragma vertex vert 
            // this specifies the vert function as the vertex shader 
         #pragma fragment frag
            // this specifies the frag function as the fragment shader
 
 		
 		uniform sampler2D _Face;
 		uniform float _RandomSeed;
 
 		struct vertexInput {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
         };
 
 		struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 tex : TEXCOORD0;
         };
  		
 		float circle(float2 center,  float radius, float thickness, float2 pos){
 			float dist = length(pos-center);
 			float difference = dist-radius;
 		
	 		if (difference > thickness/2)
	 			return 1;
 			else if (difference < -thickness/2)
 				return -1;
 			else 
 				return 2*difference/thickness;
 		}
 
		vertexOutput vert(vertexInput input) 
         {
            vertexOutput output; 
            output.pos =  mul(UNITY_MATRIX_MVP, input.vertex);
            output.tex = input.texcoord;
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR // fragment shader
         {
         	float2 texco = input.tex;
         	texco.y += sin(_RandomSeed+_Time*100)*0.1;
         
         	float cValue = circle(float2(0.5,0.5),0.4,0.05,texco);
         	float4 col = float4 (0,0,0,1-cValue);
			
			if (cValue < 0)
				col = tex2D(_Face,float2(texco)) - float4(1+cValue,1+cValue,1+cValue,0);

			return col;
         }
 
         ENDCG // here ends the part in Cg 
      }
   }
}
