çShader "Hidden/FisheyeShader" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "" {}
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
varying highp vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec2 tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = _glesMultiTexCoord0.xy;
  tmpvar_1 = tmpvar_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform highp vec2 intensity;
varying highp vec2 xlv_TEXCOORD0;
void main ()
{
  mediump vec4 color_1;
  mediump vec2 realCoordOffs_2;
  mediump vec2 coords_3;
  coords_3 = xlv_TEXCOORD0;
  mediump vec2 tmpvar_4;
  tmpvar_4 = ((coords_3 - 0.5) * 2.0);
  coords_3 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = (((1.0 - (tmpvar_4.y * tmpvar_4.y)) * intensity.y) * tmpvar_4.x);
  realCoordOffs_2.x = tmpvar_5;
  highp float tmpvar_6;
  tmpvar_6 = (((1.0 - (tmpvar_4.x * tmpvar_4.x)) * intensity.x) * tmpvar_4.y);
  realCoordOffs_2.y = tmpvar_6;
  lowp vec4 tmpvar_7;
  highp vec2 P_8;
  P_8 = (xlv_TEXCOORD0 - realCoordOffs_2);
  tmpvar_7 = texture2D (_MainTex, P_8);
  color_1 = tmpvar_7;
  gl_FragData[0] = color_1;
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX

in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
out highp vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec2 tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = _glesMultiTexCoord0.xy;
  tmpvar_1 = tmpvar_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
}



#endif
#ifdef FRAGMENT

out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform highp vec2 intensity;
in highp vec2 xlv_TEXCOORD0;
void main ()
{
  mediump vec4 color_1;
  mediump vec2 realCoordOffs_2;
  mediump vec2 coords_3;
  coords_3 = xlv_TEXCOORD0;
  mediump vec2 tmpvar_4;
  tmpvar_4 = ((coords_3 - 0.5) * 2.0);
  coords_3 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = (((1.0 - (tmpvar_4.y * tmpvar_4.y)) * intensity.y) * tmpvar_4.x);
  realCoordOffs_2.x = tmpvar_5;
  highp float tmpvar_6;
  tmpvar_6 = (((1.0 - (tmpvar_4.x * tmpvar_4.x)) * intensity.x) * tmpvar_4.y);
  realCoordOffs_2.y = tmpvar_6;
  lowp vec4 tmpvar_7;
  highp vec2 P_8;
  P_8 = (xlv_TEXCOORD0 - realCoordOffs_2);
  tmpvar_7 = texture (_MainTex, P_8);
  color_1 = tmpvar_7;
  _glesFragData[0] = color_1;
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