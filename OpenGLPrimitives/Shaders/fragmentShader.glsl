#version 450 core

out vec4 color;

in vec2 vs_texCoord;  
in vec4 frag_Pos;  
in vec4 vs_normal;  

uniform float lightEnable;
uniform vec4 lightPos; 
uniform vec4 lightColor;

uniform sampler2D texture0;

void main(void)
{
    float ambientStrength = 0.1;
    vec4 ambient = ambientStrength * lightColor;
  	
  	vec4 result = ambient;  	
  	
    vec4 norm = normalize(vs_normal);
    vec4 lightDir = normalize(lightPos - frag_Pos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec4 diffuse = diff * lightColor * 0.5;
    result = result + diffuse;


    color = texture(texture0, vs_texCoord);
     //color = vec4(0.3f, 1, 0.7, 1);
 }