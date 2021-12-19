#version 450 core

out vec4 color;

in vec4 vs_color;  
in vec4 frag_Pos;  
in vec4 vs_normal;  

uniform float lightEnable;
uniform vec4 lightPos; 
uniform vec4 lightColor;

void main(void)
{
     color = vec4(0.3f, 1, 0.7, 1);
 }