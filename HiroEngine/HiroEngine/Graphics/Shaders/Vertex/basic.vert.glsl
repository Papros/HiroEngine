#version 330 core

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 color;
layout (location = 2) in vec2 textCoord;

uniform mat4 pr_matrix = mat4(1.0f);
uniform mat4 vm_matrix = mat4(1.0f);
uniform mat4 ml_matrix = mat4(1.0f);

out DATA
{
	vec4 position;
	vec3 color; 
	vec2 textCoord;
} vs_out;

void main() {
	vec4 _position = vec4(position, 1.0f);
	//gl_Position = pr_matrix * vm_matrix * ml_matrix * _position;
	gl_Position =  _position;
	vs_out.position = ml_matrix * _position;
	vs_out.color = color;
	vs_out.textCoord = textCoord;
}

