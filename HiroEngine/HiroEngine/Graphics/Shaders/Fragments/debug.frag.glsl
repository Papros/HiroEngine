#version 330 core

layout (location = 0) out vec4 color;

in DATA
{
	vec4 position;
	vec3 color; 
	vec2 textCoord;
} fs_in;


void main() {
	color =  vec4( fs_in.color, 0.8f);
}