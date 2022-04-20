#version 330 core

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 color;
layout (location = 2) in vec2 textCoord;

uniform mat4 matrix_projection; // = mat4(1.0f);
uniform mat4 matrix_view; // = mat4(1.0f);
uniform mat4 matrix_model; // = mat4(1.0f);

out DATA
{
	vec4 position;
	vec3 color; 
	vec2 textCoord;
} vs_out;

void main() {
	//vec4 _position = normalize(vec4(position, 1.0f));
	vec4 _position = vec4(position, 1.0f);
	gl_Position =  _position;// * matrix_model * matrix_view * matrix_projection;
	vs_out.position = _position; // * matrix_model;
	vs_out.color = color;
	vs_out.textCoord = textCoord;
}

