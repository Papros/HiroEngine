#version 330 core

layout (location = 0) out vec4 color;

uniform vec4 light_color = vec4(1.0f, 1.0f, 1.0f, 1.0f);
uniform vec2 light_position;
uniform float saturation = 1.0f;

uniform sampler2D texture1;
uniform sampler2D texture2;

const float limit = 1.0f;

in DATA
{
	vec4 position;
	vec3 color; 
	vec2 textCoord;
} fs_in;


void main() {
	float intensity = 1.0f*0.2f / length(fs_in.position.xy - light_position);
	intensity = ( intensity < limit ? intensity : limit );
	color =  texture(texture1, fs_in.textCoord) * vec4( fs_in.color * saturation * intensity, 1.0f);
	//color = vec4( fs_in.color * saturation * intensity, 1.0f);
	//color = vec4(fs_in.textCoord, 1.0f, 1.0f);
	//color = mix(texture(texture1, fs_in.textCoord), texture(texture2, fs_in.textCoord), 0.2);
}