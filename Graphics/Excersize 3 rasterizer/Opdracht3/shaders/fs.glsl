#version 430

// shader input
in vec2 uv;						// interpolated texture coordinates
in vec4 normal;					// interpolated normal
in vec3 positionV;
uniform sampler2D pixels;		// texture sampler

struct light{
    vec3 _position;
    float _intensity;
    vec4 _colorSpec;
	vec4 _colorDiff;
};

layout(std430) buffer Light{
	light Lights[];
};

uniform vec3 camPos;
uniform vec4 ambient;
uniform mat4 transform;
uniform mat4 worldTrans;

// shader output
out vec4 outputColor;

// fragment shader
void main()
{
 vec3 actualPosition = (worldTrans * vec4(positionV, 1.0)).xyz;
	//Setup ambient as in the tutorial
	//vec4 As = vec4 (0.1f, 0.1f, 0.1f, 1.0f);
	//vec4 ambient = (Lights[0]._ambient + ambient ) + ( As + ambient);
	vec4 ambientt = ambient;
	vec4 outputColorTemp = ambientt;
	vec4 outputColorTempDiffuse = vec4 (0f, 0f, 0f, 0f);

	for(int i = 0; i < Lights.length(); i++){

		//Setup diffuse
		vec4 Dl = Lights[i]._colorDiff;
		//Dl = vec4 (1.5f, 1.5f, 1.5f, 1.0f);
		vec4 Dm = vec4 (1.5f, 1.5f, 1.5f, 1.0f);
		vec3 l = normalize(actualPosition - Lights[i]._position);
		vec3 nnn = vec3 (normal.x, normal.y, normal.z);
		vec3 nn = normalize(nnn);
		float distatt = Lights[i]._intensity / pow(distance(actualPosition, Lights[i]._position), 2);
	
		float LambertTerm = dot(nn,l);

		if (LambertTerm > 0.0f)
		{

			vec4 diffuse = Dl * Dm * LambertTerm;
			outputColorTempDiffuse += (diffuse * distatt);

			//Setup specular
			vec4 Sl = Lights[i]._colorSpec;
			vec4 Sm = texture( pixels, uv );
			vec3 R = reflect(-l, nn);

			float f = 60.0f; //hardcoded shininess

			vec3 E = normalize(camPos - actualPosition);
			vec4 specular = Sm * Sl * pow( max(dot(R, E), 0.0), f );
			outputColorTemp += (specular * distatt); 
		}
	}

	outputColor = texture( pixels, uv ) * outputColorTemp * outputColorTempDiffuse;


}