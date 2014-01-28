Shader "Custom/BGShader" {
	Properties {
		_Parallax0 ("Parallax 0", 2D) = "white" { }
		_Parallax1 ("Parallax 1", 2D) = "white" { }
		_Parallax2 ("Parallax 2", 2D) = "white" { }
		_City ("City Layer", 2D) = "white" { }
		_CityBrightness ("City Brightness",Range(0,1)) = 1
		_Speed ("Speed", float) = 10
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
 
 		
 		uniform sampler2D _Parallax0,_Parallax1,_Parallax2,_City;
 		
 		uniform float _Speed;
 		uniform float _HyperSpeed;
 		uniform float _CityBrightness;
 
		float rand(float2 co){
		    return frac(sin(dot(co.xy ,float2(12.9898,78.233))) * 43758.5453);
		}
 
 		struct vertexInput {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
         };
 
 		struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 worldPos : TEXCOORD2;
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
            output.worldPos = mul(_Object2World,input.vertex);
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR // fragment shader
         {
         	float texture_size = 0.38;
	        float texture_position = 0.56;
         
         	float4 rvalue = float4(0,0,0,0);
         	
         	float t = 0.995;
         	if (_SinTime.z > t)
         		rvalue += float4(1,1,1,1) * (_SinTime.z-t)/(1-t);
         	
			 if (_Speed < _HyperSpeed){
         	 	
				
	           	if (input.tex.y-0.4 > sin(input.tex.x*20-input.worldPos.x/10)/10 + sin(input.tex.x*10-input.worldPos.x/20)/10 + sin(5+input.tex.x*5-input.worldPos.x/40	)/10)
	         	 	rvalue = float4(0.1,0.1,0.1,1);
	         	 	
	         	
	         	float4 v = tex2D(_City,float2(0.3+input.tex.x*0.8-input.worldPos.x/85,0.99-(input.tex.y-texture_position)/texture_size));
         	 	if (input.tex.y >  texture_position && v.w > 0 && input.tex.y < 0.99*(texture_position+texture_size))
	         	 		rvalue = v*_CityBrightness*0.7;
	         	
	         	v = tex2D(_City,float2(input.tex.x*0.8-input.worldPos.x/75,0.99-(input.tex.y-texture_position)/texture_size));
         	 	if (input.tex.y >  texture_position && v.w > 0 && input.tex.y < 0.99*(texture_position+texture_size))
	         	 		rvalue = v*_CityBrightness;
	         	
	         	 	
	         	 rvalue += tex2D(_Parallax0,float2(input.tex.x-_Time.x*_Speed,input.tex.y+-_Time.x*10)) + 
         	 			tex2D(_Parallax1,float2(input.tex.x-_Time.x*_Speed/1.5,input.tex.y+-_Time.x*10))+
         	 			tex2D(_Parallax2,float2(input.tex.x-_Time.x*_Speed/2,input.tex.y+-_Time.x*10));
	         	 	
         	 }else {
         	 	float timeOffset0 = input.tex.x-_Time.x*_Speed*3;
         	 	float timeOffset1 = input.tex.x-_Time.x*_Speed*2;
         	 	float timeOffset2 = input.tex.x-_Time.x*_Speed;
         	 	for (int i=0;i<10;i++)
         	 		rvalue +=  tex2D(_Parallax0,float2(timeOffset0+i/256.0,input.tex.y)) + 
         	 			tex2D(_Parallax1,float2(timeOffset1+i/256.0,input.tex.y))+
         	 			tex2D(_Parallax2,float2(timeOffset2+i/256.0,input.tex.y));	
         	 }
         	 

			if (input.tex.y >= (texture_position+texture_size)*0.99)
	         		rvalue = float4(0,0,0,0);
         	 
         	 return rvalue;
         }
 
         ENDCG // here ends the part in Cg 
      }
   }
}
