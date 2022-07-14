#version 330 core

layout (location = 0) in vec3 vertex_coords;
layout (location = 1) in vec3 vertex_color;
layout (location = 2) in vec2 texture_coords;

uniform mat4 matrix_projection;// = mat4(1.0f);
uniform mat4 matrix_view;// = mat4(1.0f);
uniform mat4 matrix_model;// = mat4(1.0f);

out DATA
{
	vec4 position;
	vec3 color; 
	vec2 textCoord;
} vs_out;

void main() {
	gl_Position =  vec4(vertex_coords, 1.0f) * matrix_model * matrix_view * matrix_projection;
	vs_out.position = vec4(vertex_coords, 1.0f) * matrix_model;
	vs_out.color = vertex_color;
	vs_out.textCoord = texture_coords;
}

