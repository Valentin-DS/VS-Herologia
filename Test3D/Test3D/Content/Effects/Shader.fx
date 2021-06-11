struct VertexShaderOutput
{
    float4 Position     : POSITION0;    
    float2 TexCoords    : TEXCOORD0;
	float3 Normal : TEXCOORD1;
	float3 Position3D    : TEXCOORD2;
	float3 View : TEXCOORD3;
	float3x3 WorldToTangentSpace : TEXCOORD4;
    
};

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float2 TexCoords : TEXCOORD0;
	float3 Normal : NORMAL0;
	float3 Binormal : BINORMAL0;
	float3 Tangent : TANGENT0;
};

struct PixelToFrame
{
    float4 Color        : COLOR0;
};

float xAmbientIntensity;
float4 xAmbientColor;
float3 xAmbientPosition;

float xMaterialAmbient;
float xMaterialDiffuse;
float xMaterialSpecular;
float xMaterialShininess;

#define MaxLight 20
int xLightNumber = 0;
float3 xLightPos[MaxLight];
float xLightRadius[MaxLight];
float xLightIntensity[MaxLight];
float4 xDiffuseColor[MaxLight];
float4 xSpecularColor[MaxLight];

float3 xView;
float4x4 xWorld;
float4x4 xWorldInverseTranspose;
float4x4 xWorldViewProjection;
Texture xTexture;
Texture xNormalMap;

float4x4 xLightsWorldViewProjection;

sampler TextureSampler = sampler_state { texture = <xTexture>; magfilter = POINT; minfilter = POINT; mipfilter = POINT; AddressU = WRAP; AddressV = WRAP; };
sampler NormalSampler = sampler_state { texture = <xNormalMap>; magfilter = POINT; minfilter = POINT;  mipfilter = POINT; AddressU = WRAP; AddressV = WRAP; };


VertexShaderOutput SimplestVertexShader(VertexShaderInput input)
{
    VertexShaderOutput Output = (VertexShaderOutput)0;
    
    Output.Position = mul(input.Position, xWorldViewProjection);
    Output.TexCoords = input.TexCoords;
	Output.Normal = normalize(mul(input.Normal, xWorld));
	Output.Position3D = mul(input.Position, xWorld);
	Output.View = normalize(float4(xView, 1.0) - Output.Position3D);
	Output.WorldToTangentSpace[0] = normalize(mul(input.Tangent, xWorld));
	Output.WorldToTangentSpace[1] = normalize(mul(input.Binormal, xWorld));
	Output.WorldToTangentSpace[2] = normalize(mul(input.Normal, xWorld));

    return Output;
}

float4 CalculateDiffuse(float3 position, float3 normal, float3 lightPosition, float4 diffuseColor, float intensity) {
	float3 lightDir = normalize(position - lightPosition);
	float diffuseLightingFactor = saturate(dot(-lightDir, normal));
	return diffuseLightingFactor * diffuseColor * intensity * xMaterialDiffuse;
}

float4 CalculateSpecular(float3 position, float3 normalInput, float3 view, float3 lightPosition, float4 specularColor, float intensity) {
	float3 lightDir = normalize(position - lightPosition);
	float3 normal = normalize(normalInput);
	float3 r = normalize(2.0 * saturate(dot(-lightDir, normal)) * normal - float4(lightDir, 1.0));
	return max(pow(saturate(dot(r, view)), xMaterialShininess), 0) * specularColor * intensity * xMaterialSpecular;
}

PixelToFrame OurFirstPixelShader(VertexShaderOutput PSIn)
{
    PixelToFrame Output = (PixelToFrame)0;

	float4 diffuse = CalculateDiffuse(PSIn.Position3D, PSIn.Normal, xAmbientPosition, xAmbientColor, xAmbientIntensity);
	float4 specular = CalculateSpecular(PSIn.Position3D, PSIn.Normal, PSIn.View, xAmbientPosition, xAmbientColor, xAmbientIntensity);

	float attenuation = 1;

	for (int i = 0; i < xLightNumber; i++)
	{
		attenuation = 1 - (pow(((PSIn.Position3D.x - xLightPos[i].x) / xLightRadius[i]), 2) + pow(((PSIn.Position3D.y - xLightPos[i].y) / xLightRadius[i]), 2) + pow(((PSIn.Position3D.z - xLightPos[i].z) / xLightRadius[i]), 2));
		if (attenuation < 0) attenuation = 0;

		diffuse += CalculateDiffuse(PSIn.Position3D, PSIn.Normal, xLightPos[i], xDiffuseColor[i], xLightIntensity[i]) * attenuation;
		specular += CalculateSpecular(PSIn.Position3D, PSIn.Normal, PSIn.View, xLightPos[i], xSpecularColor[i], xLightIntensity[i]) * attenuation;
	}

	float4 baseColor = tex2D(TextureSampler, PSIn.TexCoords);
	Output.Color = baseColor*(diffuse + specular + xAmbientIntensity * xMaterialAmbient * xAmbientColor);
	Output.Color.a = baseColor.a;

    return Output;
}

PixelToFrame NormalPixelShader(VertexShaderOutput PSIn)
{
	PixelToFrame Output = (PixelToFrame)0;

	float3 normalColor = 200 * (2.0 * (tex2D(NormalSampler, PSIn.TexCoords)) - 1.0);
	normalColor = normalize(mul(normalColor, PSIn.WorldToTangentSpace));
	float4 normal = float4(normalColor, 1.0);

	float4 diffuse = CalculateDiffuse(PSIn.Position3D, normal, xAmbientPosition, xAmbientColor, xAmbientIntensity);
	float4 specular = CalculateSpecular(PSIn.Position3D, normal, PSIn.View, xAmbientPosition, xAmbientColor, xAmbientIntensity);

	float attenuation = 1;

	for (int i = 0; i < xLightNumber; i++)
	{
		attenuation = 1 - (pow(((PSIn.Position3D.x - xLightPos[i].x) / xLightRadius[i]), 2) + pow(((PSIn.Position3D.y - xLightPos[i].y) / xLightRadius[i]), 2) + pow(((PSIn.Position3D.z - xLightPos[i].z) / xLightRadius[i]), 2));
		if (attenuation < 0) attenuation = 0;

		diffuse += CalculateDiffuse(PSIn.Position3D, normal, xLightPos[i], xDiffuseColor[i], xLightIntensity[i]) * attenuation;
		specular += CalculateSpecular(PSIn.Position3D, normal, PSIn.View, xLightPos[i], xSpecularColor[i], xLightIntensity[i]) * attenuation;
	}

	float4 baseColor = tex2D(TextureSampler, PSIn.TexCoords);
	Output.Color = baseColor * (diffuse + specular + xAmbientIntensity * xMaterialAmbient * xAmbientColor);
	Output.Color.a = baseColor.a;

	return Output;
}



technique SimplestTex
{
    pass Pass0
    {
        VertexShader = compile vs_5_0 SimplestVertexShader();
        PixelShader = compile ps_5_0 OurFirstPixelShader();
    }
}

technique NormalTex
{
	pass Pass0
	{
		VertexShader = compile vs_5_0 SimplestVertexShader();
		PixelShader = compile ps_5_0 NormalPixelShader();
	}
}