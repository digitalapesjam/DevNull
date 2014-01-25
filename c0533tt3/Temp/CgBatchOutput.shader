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
         Program "vp" {
// Vertex combos: 1
//   opengl - ALU: 6 to 6
//   d3d9 - ALU: 6 to 6
SubProgram "opengl " {
Keywords { }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
"3.0-!!ARBvp1.0
# 6 ALU
PARAM c[5] = { program.local[0],
		state.matrix.mvp };
MOV result.texcoord[0], vertex.texcoord[0];
MOV result.texcoord[1], vertex.position;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 6 instructions, 0 R-regs
"
}

SubProgram "d3d9 " {
Keywords { }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
"vs_3_0
; 6 ALU
dcl_position o0
dcl_texcoord0 o1
dcl_texcoord1 o2
dcl_position0 v0
dcl_texcoord0 v1
mov o1, v1
mov o2, v0
dp4 o0.w, v0, c3
dp4 o0.z, v0, c2
dp4 o0.y, v0, c1
dp4 o0.x, v0, c0
"
}

SubProgram "gles " {
Keywords { }
"!!GLES


#ifdef VERTEX

varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp mat4 glstate_matrix_mvp;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = _glesMultiTexCoord0;
  xlv_TEXCOORD1 = _glesVertex;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD0;
uniform highp float _HyperSpeed;
uniform highp float _ParallaxSpeed;
uniform sampler2D _Parallax2;
uniform sampler2D _Parallax1;
uniform sampler2D _Parallax0;
uniform highp vec4 _Time;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0;
  highp vec4 rvalue_2;
  rvalue_2 = vec4(0.0, 0.0, 0.0, 0.0);
  if ((_ParallaxSpeed < _HyperSpeed)) {
    highp vec2 tmpvar_3;
    tmpvar_3.x = (xlv_TEXCOORD0.x - (_Time.x * _ParallaxSpeed));
    tmpvar_3.y = (xlv_TEXCOORD0.y + (-(_Time.x) * 10.0));
    highp vec2 tmpvar_4;
    tmpvar_4.x = (xlv_TEXCOORD0.x - ((_Time.x * _ParallaxSpeed) / 1.5));
    tmpvar_4.y = (xlv_TEXCOORD0.y + (-(_Time.x) * 10.0));
    highp vec2 tmpvar_5;
    tmpvar_5.x = (xlv_TEXCOORD0.x - ((_Time.x * _ParallaxSpeed) / 2.0));
    tmpvar_5.y = (xlv_TEXCOORD0.y + (-(_Time.x) * 10.0));
    lowp vec4 tmpvar_6;
    tmpvar_6 = ((texture2D (_Parallax0, tmpvar_3) + texture2D (_Parallax1, tmpvar_4)) + texture2D (_Parallax2, tmpvar_5));
    rvalue_2 = tmpvar_6;
    highp float tmpvar_7;
    tmpvar_7 = sin((xlv_TEXCOORD0.x + (_Time.x * _ParallaxSpeed)));
    highp float tmpvar_8;
    tmpvar_8 = sin(((xlv_TEXCOORD0.x * 10.0) + (_Time.x * _ParallaxSpeed)));
    if (((xlv_TEXCOORD0.y - 0.5) > ((tmpvar_7 / 10.0) + (tmpvar_8 / 10.0)))) {
      rvalue_2 = (rvalue_2 + vec4(0.1, 0.1, 0.1, 1.0));
    };
  } else {
    int i_9;
    highp float timeOffset2_10;
    highp float timeOffset1_11;
    highp float timeOffset0_12;
    timeOffset0_12 = (xlv_TEXCOORD0.x - ((_Time.x * _ParallaxSpeed) * 3.0));
    timeOffset1_11 = (xlv_TEXCOORD0.x - ((_Time.x * _ParallaxSpeed) * 2.0));
    timeOffset2_10 = (xlv_TEXCOORD0.x - (_Time.x * _ParallaxSpeed));
    i_9 = 0;
    for (int i_9 = 0; i_9 < 10; ) {
      highp vec2 tmpvar_13;
      tmpvar_13.x = (timeOffset0_12 + (float(i_9) / 256.0));
      tmpvar_13.y = tmpvar_1.y;
      lowp vec4 tmpvar_14;
      tmpvar_14 = texture2D (_Parallax0, tmpvar_13);
      highp vec2 tmpvar_15;
      tmpvar_15.x = (timeOffset1_11 + (float(i_9) / 256.0));
      tmpvar_15.y = tmpvar_1.y;
      lowp vec4 tmpvar_16;
      tmpvar_16 = texture2D (_Parallax1, tmpvar_15);
      highp vec2 tmpvar_17;
      tmpvar_17.x = (timeOffset2_10 + (float(i_9) / 256.0));
      tmpvar_17.y = tmpvar_1.y;
      lowp vec4 tmpvar_18;
      tmpvar_18 = texture2D (_Parallax2, tmpvar_17);
      rvalue_2 = (rvalue_2 + ((tmpvar_14 + tmpvar_16) + tmpvar_18));
      i_9 = (i_9 + 1);
    };
  };
  gl_FragData[0] = rvalue_2;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { }
"!!GLES


#ifdef VERTEX

varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp mat4 glstate_matrix_mvp;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = _glesMultiTexCoord0;
  xlv_TEXCOORD1 = _glesVertex;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD0;
uniform highp float _HyperSpeed;
uniform highp float _ParallaxSpeed;
uniform sampler2D _Parallax2;
uniform sampler2D _Parallax1;
uniform sampler2D _Parallax0;
uniform highp vec4 _Time;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0;
  highp vec4 rvalue_2;
  rvalue_2 = vec4(0.0, 0.0, 0.0, 0.0);
  if ((_ParallaxSpeed < _HyperSpeed)) {
    highp vec2 tmpvar_3;
    tmpvar_3.x = (xlv_TEXCOORD0.x - (_Time.x * _ParallaxSpeed));
    tmpvar_3.y = (xlv_TEXCOORD0.y + (-(_Time.x) * 10.0));
    highp vec2 tmpvar_4;
    tmpvar_4.x = (xlv_TEXCOORD0.x - ((_Time.x * _ParallaxSpeed) / 1.5));
    tmpvar_4.y = (xlv_TEXCOORD0.y + (-(_Time.x) * 10.0));
    highp vec2 tmpvar_5;
    tmpvar_5.x = (xlv_TEXCOORD0.x - ((_Time.x * _ParallaxSpeed) / 2.0));
    tmpvar_5.y = (xlv_TEXCOORD0.y + (-(_Time.x) * 10.0));
    lowp vec4 tmpvar_6;
    tmpvar_6 = ((texture2D (_Parallax0, tmpvar_3) + texture2D (_Parallax1, tmpvar_4)) + texture2D (_Parallax2, tmpvar_5));
    rvalue_2 = tmpvar_6;
    highp float tmpvar_7;
    tmpvar_7 = sin((xlv_TEXCOORD0.x + (_Time.x * _ParallaxSpeed)));
    highp float tmpvar_8;
    tmpvar_8 = sin(((xlv_TEXCOORD0.x * 10.0) + (_Time.x * _ParallaxSpeed)));
    if (((xlv_TEXCOORD0.y - 0.5) > ((tmpvar_7 / 10.0) + (tmpvar_8 / 10.0)))) {
      rvalue_2 = (rvalue_2 + vec4(0.1, 0.1, 0.1, 1.0));
    };
  } else {
    int i_9;
    highp float timeOffset2_10;
    highp float timeOffset1_11;
    highp float timeOffset0_12;
    timeOffset0_12 = (xlv_TEXCOORD0.x - ((_Time.x * _ParallaxSpeed) * 3.0));
    timeOffset1_11 = (xlv_TEXCOORD0.x - ((_Time.x * _ParallaxSpeed) * 2.0));
    timeOffset2_10 = (xlv_TEXCOORD0.x - (_Time.x * _ParallaxSpeed));
    i_9 = 0;
    for (int i_9 = 0; i_9 < 10; ) {
      highp vec2 tmpvar_13;
      tmpvar_13.x = (timeOffset0_12 + (float(i_9) / 256.0));
      tmpvar_13.y = tmpvar_1.y;
      lowp vec4 tmpvar_14;
      tmpvar_14 = texture2D (_Parallax0, tmpvar_13);
      highp vec2 tmpvar_15;
      tmpvar_15.x = (timeOffset1_11 + (float(i_9) / 256.0));
      tmpvar_15.y = tmpvar_1.y;
      lowp vec4 tmpvar_16;
      tmpvar_16 = texture2D (_Parallax1, tmpvar_15);
      highp vec2 tmpvar_17;
      tmpvar_17.x = (timeOffset2_10 + (float(i_9) / 256.0));
      tmpvar_17.y = tmpvar_1.y;
      lowp vec4 tmpvar_18;
      tmpvar_18 = texture2D (_Parallax2, tmpvar_17);
      rvalue_2 = (rvalue_2 + ((tmpvar_14 + tmpvar_16) + tmpvar_18));
      i_9 = (i_9 + 1);
    };
  };
  gl_FragData[0] = rvalue_2;
}



#endif"
}

SubProgram "gles3 " {
Keywords { }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;

#line 64
struct vertexOutput {
    highp vec4 pos;
    highp vec4 tex;
    highp vec4 objPos;
};
#line 58
struct vertexInput {
    highp vec4 vertex;
    highp vec4 texcoord;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[8];
uniform highp vec4 unity_LightPosition[8];
uniform highp vec4 unity_LightAtten[8];
#line 19
uniform highp vec4 unity_SpotDirection[8];
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
#line 23
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
#line 27
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
#line 31
uniform highp vec4 _LightSplitsNear;
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
#line 35
uniform highp vec4 unity_ShadowFadeCenterAndType;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
#line 39
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
#line 43
uniform highp mat4 glstate_matrix_texture0;
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
#line 47
uniform highp mat4 glstate_matrix_projection;
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
#line 51
uniform sampler2D _Parallax0;
uniform sampler2D _Parallax1;
uniform sampler2D _Parallax2;
uniform highp float _ParallaxSpeed;
uniform highp float _HyperSpeed;
#line 71
#line 76
#line 84
#line 76
vertexOutput vert( in vertexInput xlat_varinput ) {
    vertexOutput xlat_varoutput;
    xlat_varoutput.pos = (glstate_matrix_mvp * xlat_varinput.vertex);
    #line 80
    xlat_varoutput.objPos = xlat_varinput.vertex;
    xlat_varoutput.tex = xlat_varinput.texcoord;
    return xlat_varoutput;
}
out highp vec4 xlv_TEXCOORD0;
out highp vec4 xlv_TEXCOORD1;
void main() {
    vertexOutput xl_retval;
    vertexInput xlt_xlat_varinput;
    xlt_xlat_varinput.vertex = vec4(gl_Vertex);
    xlt_xlat_varinput.texcoord = vec4(gl_MultiTexCoord0);
    xl_retval = vert( xlt_xlat_varinput);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.tex);
    xlv_TEXCOORD1 = vec4(xl_retval.objPos);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];

#line 64
struct vertexOutput {
    highp vec4 pos;
    highp vec4 tex;
    highp vec4 objPos;
};
#line 58
struct vertexInput {
    highp vec4 vertex;
    highp vec4 texcoord;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[8];
uniform highp vec4 unity_LightPosition[8];
uniform highp vec4 unity_LightAtten[8];
#line 19
uniform highp vec4 unity_SpotDirection[8];
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
#line 23
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
#line 27
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
#line 31
uniform highp vec4 _LightSplitsNear;
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
#line 35
uniform highp vec4 unity_ShadowFadeCenterAndType;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
#line 39
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
#line 43
uniform highp mat4 glstate_matrix_texture0;
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
#line 47
uniform highp mat4 glstate_matrix_projection;
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
#line 51
uniform sampler2D _Parallax0;
uniform sampler2D _Parallax1;
uniform sampler2D _Parallax2;
uniform highp float _ParallaxSpeed;
uniform highp float _HyperSpeed;
#line 71
#line 76
#line 84
#line 84
highp vec4 frag( in vertexOutput xlat_varinput ) {
    highp vec4 rvalue = vec4( 0.0, 0.0, 0.0, 0.0);
    if ((_ParallaxSpeed < _HyperSpeed)){
        #line 89
        rvalue = ((texture( _Parallax0, vec2( (xlat_varinput.tex.x - (_Time.x * _ParallaxSpeed)), (xlat_varinput.tex.y + ((-_Time.x) * 10.0)))) + texture( _Parallax1, vec2( (xlat_varinput.tex.x - ((_Time.x * _ParallaxSpeed) / 1.5)), (xlat_varinput.tex.y + ((-_Time.x) * 10.0))))) + texture( _Parallax2, vec2( (xlat_varinput.tex.x - ((_Time.x * _ParallaxSpeed) / 2.0)), (xlat_varinput.tex.y + ((-_Time.x) * 10.0)))));
        if (((xlat_varinput.tex.y - 0.5) > ((sin((xlat_varinput.tex.x + (_Time.x * _ParallaxSpeed))) / 10.0) + (sin(((xlat_varinput.tex.x * 10.0) + (_Time.x * _ParallaxSpeed))) / 10.0)))){
            rvalue += vec4( 0.1, 0.1, 0.1, 1.0);
        }
    }
    else{
        #line 94
        highp float timeOffset0 = (xlat_varinput.tex.x - ((_Time.x * _ParallaxSpeed) * 3.0));
        highp float timeOffset1 = (xlat_varinput.tex.x - ((_Time.x * _ParallaxSpeed) * 2.0));
        highp float timeOffset2 = (xlat_varinput.tex.x - (_Time.x * _ParallaxSpeed));
        highp int i = 0;
        for ( ; (i < 10); (i++)) {
            rvalue += ((texture( _Parallax0, vec2( (timeOffset0 + (float(i) / 256.0)), xlat_varinput.tex.y)) + texture( _Parallax1, vec2( (timeOffset1 + (float(i) / 256.0)), xlat_varinput.tex.y))) + texture( _Parallax2, vec2( (timeOffset2 + (float(i) / 256.0)), xlat_varinput.tex.y)));
        }
    }
    #line 101
    return rvalue;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec4 xlv_TEXCOORD1;
void main() {
    highp vec4 xl_retval;
    vertexOutput xlt_xlat_varinput;
    xlt_xlat_varinput.pos = vec4(0.0);
    xlt_xlat_varinput.tex = vec4(xlv_TEXCOORD0);
    xlt_xlat_varinput.objPos = vec4(xlv_TEXCOORD1);
    xl_retval = frag( xlt_xlat_varinput);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}

}
Program "fp" {
// Fragment combos: 1
//   opengl - ALU: 158 to 158, TEX: 33 to 33
//   d3d9 - ALU: 62 to 62, TEX: 6 to 6, FLOW: 7 to 7
SubProgram "opengl " {
Keywords { }
Vector 0 [_Time]
Float 1 [_ParallaxSpeed]
Float 2 [_HyperSpeed]
SetTexture 0 [_Parallax0] 2D
SetTexture 1 [_Parallax1] 2D
SetTexture 2 [_Parallax2] 2D
"3.0-!!ARBfp1.0
# 158 ALU, 33 TEX
PARAM c[8] = { program.local[0..2],
		{ 0, 1, 3, 2 },
		{ 10, 0.1, 0.5, 0.66666669 },
		{ 0.1, 1, 0.00390625, 0.0078125 },
		{ 0.01171875, 0.015625, 0.01953125, 0.0234375 },
		{ 0.02734375, 0.03125, 0.03515625 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEMP R5;
TEMP R6;
MOV R3.x, c[1];
MUL R4.y, R3.x, c[0].x;
MOV R0.x, c[4];
MAD R2.y, R0.x, -c[0].x, fragment.texcoord[0];
MAD R2.x, -R4.y, c[4].z, fragment.texcoord[0];
MAD R6.x, R3, -c[0], fragment.texcoord[0];
MAD R5.x, -R4.y, c[3].w, fragment.texcoord[0];
MAD R5.z, -R4.y, c[3], fragment.texcoord[0].x;
TEX R1, R2, texture[2], 2D;
MOV R0.y, R2;
MAD R0.x, -R4.y, c[4].w, fragment.texcoord[0];
MOV R6.y, R2;
TEX R2, R6, texture[0], 2D;
TEX R0, R0, texture[1], 2D;
ADD R0, R2, R0;
ADD R0, R0, R1;
MAD R1.y, fragment.texcoord[0].x, c[4].x, R4;
MAD R1.x, R3, c[0], fragment.texcoord[0];
SIN R1.y, R1.y;
SIN R1.x, R1.x;
ADD R2.x, R1, R1.y;
MUL R2.y, R2.x, c[4];
ADD R2.z, fragment.texcoord[0].y, -c[4];
MOV R2.x, c[2];
ADD R1, R0, c[5].xxxy;
SLT R4.x, c[1], R2;
SLT R2.y, R2, R2.z;
MUL R2.x, R4, R2.y;
CMP R1, -R2.x, R1, R0;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R6, c[5].z;
TEX R3, R0, texture[2], 2D;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R5, c[5].z;
TEX R2, R0, texture[1], 2D;
MOV R5.y, fragment.texcoord[0];
ABS R4.x, R4;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R5.z, c[5].z;
TEX R0, R0, texture[0], 2D;
ADD R0, R0, R2;
TEX R2, R5, texture[1], 2D;
CMP R5.y, -R4.x, c[3].x, c[3];
ADD R0, R0, R3;
MOV R5.w, fragment.texcoord[0].y;
TEX R3, R5.zwzw, texture[0], 2D;
ADD R3, R3, R2;
MOV R2.y, fragment.texcoord[0];
MOV R2.x, R6;
TEX R2, R2, texture[2], 2D;
ADD R2, R3, R2;
CMP R3, -R5.y, R2, R1;
ADD R4, R3, R0;
CMP R3, -R5.y, R4, R3;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R6, c[5].w;
TEX R2, R0, texture[2], 2D;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R5, c[5].w;
TEX R1, R0, texture[1], 2D;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R5.z, c[5].w;
TEX R0, R0, texture[0], 2D;
ADD R0, R0, R1;
ADD R0, R0, R2;
ADD R4, R3, R0;
CMP R3, -R5.y, R4, R3;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R6, c[6];
TEX R2, R0, texture[2], 2D;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R5, c[6];
TEX R1, R0, texture[1], 2D;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R5.z, c[6];
TEX R0, R0, texture[0], 2D;
ADD R0, R0, R1;
ADD R0, R0, R2;
ADD R4, R3, R0;
CMP R3, -R5.y, R4, R3;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R6, c[6].y;
TEX R2, R0, texture[2], 2D;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R5, c[6].y;
TEX R1, R0, texture[1], 2D;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R5.z, c[6].y;
TEX R0, R0, texture[0], 2D;
ADD R0, R0, R1;
ADD R0, R0, R2;
ADD R4, R3, R0;
CMP R3, -R5.y, R4, R3;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R6, c[6].z;
TEX R2, R0, texture[2], 2D;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R5, c[6].z;
TEX R1, R0, texture[1], 2D;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R5.z, c[6].z;
TEX R0, R0, texture[0], 2D;
ADD R0, R0, R1;
ADD R0, R0, R2;
ADD R4, R3, R0;
CMP R3, -R5.y, R4, R3;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R6, c[6].w;
TEX R2, R0, texture[2], 2D;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R5, c[6].w;
TEX R1, R0, texture[1], 2D;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R5.z, c[6].w;
TEX R0, R0, texture[0], 2D;
ADD R0, R0, R1;
ADD R0, R0, R2;
ADD R4, R3, R0;
CMP R3, -R5.y, R4, R3;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R6, c[7];
TEX R2, R0, texture[2], 2D;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R5, c[7];
TEX R1, R0, texture[1], 2D;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R5.z, c[7];
TEX R0, R0, texture[0], 2D;
ADD R0, R0, R1;
ADD R0, R0, R2;
ADD R4, R3, R0;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R6, c[7].y;
TEX R2, R0, texture[2], 2D;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R5, c[7].y;
TEX R1, R0, texture[1], 2D;
MOV R0.y, fragment.texcoord[0];
ADD R0.x, R5.z, c[7].y;
TEX R0, R0, texture[0], 2D;
ADD R0, R0, R1;
ADD R1, R0, R2;
CMP R0, -R5.y, R4, R3;
ADD R1, R0, R1;
CMP R0, -R5.y, R1, R0;
MOV R2.y, fragment.texcoord[0];
ADD R2.x, R6, c[7].z;
TEX R2, R2, texture[2], 2D;
MOV R3.y, fragment.texcoord[0];
ADD R3.x, R5, c[7].z;
TEX R3, R3, texture[1], 2D;
MOV R4.y, fragment.texcoord[0];
ADD R4.x, R5.z, c[7].z;
TEX R4, R4, texture[0], 2D;
ADD R3, R4, R3;
ADD R2, R3, R2;
ADD R1, R0, R2;
CMP result.color, -R5.y, R1, R0;
END
# 158 instructions, 7 R-regs
"
}

SubProgram "d3d9 " {
Keywords { }
Vector 0 [_Time]
Float 1 [_ParallaxSpeed]
Float 2 [_HyperSpeed]
SetTexture 0 [_Parallax0] 2D
SetTexture 1 [_Parallax1] 2D
SetTexture 2 [_Parallax2] 2D
"ps_3_0
; 62 ALU, 6 TEX, 7 FLOW
dcl_2d s0
dcl_2d s1
dcl_2d s2
def c3, 0.00000000, 0.15915491, 0.50000000, 0.10000000
def c4, 6.28318501, -3.14159298, -0.50000000, 10.00000000
def c5, 0.66666669, 0.10000000, 1.00000000, 3.00000000
def c6, 2.00000000, 0.00390625, 0, 0
defi i0, 10, 0, 1, 0
dcl_texcoord0 v0.xy
mov r1.x, c2
mov r0, c3.x
if_lt c1.x, r1.x
mov r0.x, c0
mad r3.y, c4.w, -r0.x, v0
mov r1.x, c0
mul r3.z, c1.x, r1.x
mov r0.x, c0
mad r3.x, -r3.z, c3.z, v0
mov r0.y, r3
mad r0.x, c1, -r0, v0
texld r0, r0, s0
mov r1.y, r3
mad r1.x, -r3.z, c5, v0
texld r1, r1, s1
add r2, r0, r1
mad r0.y, v0.x, c4.w, r3.z
mov r0.x, c0
mad r0.x, c1, r0, v0
mad r0.y, r0, c3, c3.z
mad r0.x, r0, c3.y, c3.z
frc r0.x, r0
texld r1, r3, s2
frc r0.y, r0
mad r3.x, r0.y, c4, c4.y
mad r4.x, r0, c4, c4.y
sincos r0.xy, r3.x
sincos r3.xy, r4.x
add r3.x, r3.y, r0.y
add r0, r2, r1
mad r2.x, -r3, c3.w, v0.y
add r1, r0, c5.yyyz
add r2.x, r2, c4.z
cmp r0, -r2.x, r0, r1
else
mov r1.x, c0
mul r1.y, c1.x, r1.x
mov r1.x, c0
mad r4.x, -r1.y, c5.w, v0
mad r4.y, -r1, c6.x, v0.x
mad r4.z, c1.x, -r1.x, v0.x
mov r4.w, c3.x
loop aL, i0
mad r1.x, r4.w, c6.y, r4.z
mov r1.y, v0
texld r3, r1, s2
mad r1.x, r4.w, c6.y, r4.y
mov r1.y, v0
texld r2, r1, s1
mad r1.x, r4.w, c6.y, r4
mov r1.y, v0
texld r1, r1, s0
add r1, r1, r2
add r1, r1, r3
add r0, r0, r1
add r4.w, r4, c5.z
endloop
endif
mov oC0, r0
"
}

SubProgram "gles " {
Keywords { }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { }
"!!GLES"
}

SubProgram "gles3 " {
Keywords { }
"!!GLES3"
}

}

#LINE 81
 // here ends the part in Cg 
      }
   }
}
