Shader "Custom/BGShader" {
	Properties {
		_Parallax0 ("Parallax 0", 2D) = "white" { }
		_Parallax1 ("Parallax 1", 2D) = "white" { }
		_Parallax2 ("Parallax 2", 2D) = "white" { }
		_ParallaxSpeed ("Parallax Speed", float) = 10
		_HyperSpeed ("Hyper Speed", float) = 20
	}
SubShader { // Unity chooses the subshader that fits the GPU best
      Pass { // some shaders require multiple passes
         CGPROGRAM // here begins the part in Unity's Cg
 
 		 #pragma target 3.0
 
         #pragma vertex vert 
            // this specifies the vert function as the vertex shader 
         #pragma fragment frag
            // this specifies the frag function as the fragment shader
 
 		
 		uniform sampler2D _Parallax0,_Parallax1,_Parallax2;
 		uniform float _ParallaxSpeed;
 		uniform float _HyperSpeed;
 
		float rand(float2 co){
		    return frac(sin(dot(co.xy ,float2(12.9898,78.233))) * 43758.5453);
		}
 
 		struct vertexInput {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
         };
 
 		struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 tex : TEXCOORD0;
            float4 objPos : TEXCOORD1;
         };
  		
 		float line(float2 center,  float len, float thickness, float4 pixelPos){
	 		if (abs(center.y-pixelPos.y) < thickness/2 && abs(center.x-pixelPos.x) < len/2)
	 			return 1;
 			else
 				return 0;
 		}
 
		vertexOutput vert(vertexInput input) 
         {
            vertexOutput output; 
            output.pos =  mul(UNITY_MATRIX_MVP, input.vertex);
            output.objPos = input.vertex;
            output.tex = input.texcoord;
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR // fragment shader
         {
         	float4 rvalue = float4(0,0,0,0);
			 if (_ParallaxSpeed < _HyperSpeed){
         	 	rvalue = tex2D(_Parallax0,float2(input.tex.x-_Time.x*_ParallaxSpeed,input.tex.y+-_Time.x*10)) + 
         	 			tex2D(_Parallax1,float2(input.tex.x-_Time.x*_ParallaxSpeed/1.5,input.tex.y+-_Time.x*10))+
         	 			tex2D(_Parallax2,float2(input.tex.x-_Time.x*_ParallaxSpeed/2,input.tex.y+-_Time.x*10));	
         	 			
	           	 if (input.tex.y-0.5 > sin(input.tex.x+_Time.x*_ParallaxSpeed)/10 + sin(input.tex.x*10+_Time.x*_ParallaxSpeed)/10)
	         	 	rvalue += float4(0.1,0.1,0.1,1);
         	 }else {
         	 	float timeOffset0 = input.tex.x-_Time.x*_ParallaxSpeed*3;
         	 	float timeOffset1 = input.tex.x-_Time.x*_ParallaxSpeed*2;
         	 	float timeOffset2 = input.tex.x-_Time.x*_ParallaxSpeed;
         	 	for (int i=0;i<10;i++)
         	 		rvalue +=  tex2D(_Parallax0,float2(timeOffset0+i/256.0,input.tex.y)) + 
         	 			tex2D(_Parallax1,float2(timeOffset1+i/256.0,input.tex.y))+
         	 			tex2D(_Parallax2,float2(timeOffset2+i/256.0,input.tex.y));	
         	 }
         	 

         	 
         	 return rvalue;
         }
 
         ENDCG // here ends the part in Cg 
      }
   }
}
