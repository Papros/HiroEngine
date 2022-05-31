#version 330 core

layout (location = 0) out vec4 color;

uniform sampler2D texture1;
uniform vec4 cheatedColor;

in DATA
{
	vec4 position;
	vec3 color; 
	vec2 textCoord;
} fs_in;


void main() {
	color = cheatedColor;
}