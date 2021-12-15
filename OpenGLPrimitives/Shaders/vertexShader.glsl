#version 450 core

layout (location = 0) in vec4 position;
layout (location = 1) in vec4 normal;
layout (location = 2) in vec2 texCoord;

out vec4 vs_normal;
out vec4 frag_Position;
out vec2 vs_texCoord;


layout (location = 20) uniform mat4 projection;
layout (location = 21) uniform mat4 model;
layout (location = 22) uniform mat4 view;

void main()
{
	gl_Position = projection * view * model * position;
	frag_Position = model * position;
	vs_normal = model * normal;
	vs_texCoord = texCoord;
}