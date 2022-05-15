#version 330 core

layout (location = 0) out vec4 color;

uniform vec4 light_color = vec4(1.0f, 1.0f, 1.0f, 1.0f);
uniform vec2 light_position;
uniform float light_saturation = 1.0f;
uniform float light_limit = 1.0f;
uniform float light_power = 0.5f;

uniform sampler2D texture1;
uniform sampler2D texture2;

in DATA
{
	vec4 position;
	vec3 color; 
	vec2 textCoord;
} fs_in;


void main() {
	float intensity = light_power / length(fs_in.position.xy - light_position);
	intensity = 1.0f;//( intensity < light_limit ? intensity : light_limit );
	color =  texture(texture1, fs_in.textCoord) * vec4( fs_in.color * light_power * intensity, 1.0f);
}