ÐShader "Hidden/Twirt Effect Shader" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  Fog { Mode Off }
Program "vp" {
SubProgram "gles " {
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _CenterRadius;
varying highp vec2 xlv_TEXCOORD0;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = (_glesMultiTexCoord0.xy - _CenterRadius.xy);
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform highp vec4 _CenterRadius;
uniform highp mat4 _RotationMatrix;
varying highp vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  tmpvar_2.zw = vec2(0.0, 0.0);
  tmpvar_2.x = xlv_TEXCOORD0.x;
  tmpvar_2.y = xlv_TEXCOORD0.y;
  highp vec2 tmpvar_3;
  tmpvar_3 = (xlv_TEXCOORD0 / _CenterRadius.zw);
  highp vec2 tmpvar_4;
  tmpvar_4 = (mix ((_RotationMatrix * tmpvar_2).xy, xlv_TEXCOORD0, vec2(min (1.0, sqrt(dot (tmpvar_3, tmpvar_3))))) + _CenterRadius.xy);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_4);
  tmpvar_1 = tmpvar_5;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX

in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _CenterRadius;
out highp vec2 xlv_TEXCOORD0;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = (_glesMultiTexCoord0.xy - _CenterRadius.xy);
}



#endif
#ifdef FRAGMENT

out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform highp vec4 _CenterRadius;
uniform highp mat4 _RotationMatrix;
in highp vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  tmpvar_2.zw = vec2(0.0, 0.0);
  tmpvar_2.x = xlv_TEXCOORD0.x;
  tmpvar_2.y = xlv_TEXCOORD0.y;
  highp vec2 tmpvar_3;
  tmpvar_3 = (xlv_TEXCOORD0 / _CenterRadius.zw);
  highp vec2 tmpvar_4;
  tmpvar_4 = (mix ((_RotationMatrix * tmpvar_2).xy, xlv_TEXCOORD0, vec2(min (1.0, sqrt(dot (tmpvar_3, tmpvar_3))))) + _CenterRadius.xy);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture (_MainTex, tmpvar_4);
  tmpvar_1 = tmpvar_5;
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
"!!GLES"
}
SubProgram "gles3 " {
"!!GLES3"
}
}
 }
}
Fallback Off
}