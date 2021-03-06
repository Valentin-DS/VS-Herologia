#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

// our inputs per model
matrix World;
matrix WorldViewProj;       //This is world * view * projection

// our inputs per change in lighting / draw

float3 CameraPosition;
float3 SunLightDirection;
float4 SunLightColor;
float SunLightIntensity;

#define MAXLIGHT 20

float3 PointLightPosition[MAXLIGHT];
float4 PointLightColor[MAXLIGHT];
float PointLightIntensity[MAXLIGHT];
float PointLightRadius[MAXLIGHT];
int MaxLightsRendered = 0;

//////////////////////////////////////////////////////////////

Texture2D DiffuseTexture;
SamplerState textureSampler
{
    MinFilter = linear;
    MagFilter = Anisotropic;
    AddressU = Wrap;
    AddressV = Wrap;
};

///////////////////////////////////////

//The inputs we need from the model
struct VertexShaderInput
{
    float4 Position : SV_POSITION0;
    float3 Normal : NORMAL0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION0;
    float3 Normal : NORMAL0;
    float2 TexCoord : TEXCOORD0;
    float3 WorldPos : TEXCOORD2;
}

////////////////////////////////////////////////////////////////


//  Our Vertex Shader
VertexShaderOutput VertexShader(VertexShaderInput input)
{
    VertexShaderOutput Output;
    
    //Calculate the position on screen
    Output.Position = mul(input.Position, WorldViewProj);

    //Transform the normal to world space
    Output.Normal = mul(float4(input.Normal, 0), World).xyz;

    //UV coordinates for our textures
    Output.TexCoord = input.TexCoord;

    //The position of our vertex in world space
    Output.WorldPos = mul(input.Position, World).xyz;
    
    return Output;
}

// Our lighting equations

float4 CalcDiffuseLight(float3 normal, float3 lightDirection, float4 lightColor, float lightIntensity)
{
    return saturate(dot(normal, -lightDirection)) * lightIntensity * lightColor;
}

float4 CalcSpecularLight(float3 normal, float3 lightDirection, float3 cameraDirection, float4 lightColor, float lightIntensity)
{
    float3 halfVector = normalize(lightDirection + cameraDirection);
    float specular = saturate(dot(halfVector, normal));

    //I have all models be the same reflectance
    float specularPower = 2;

    return lightIntensity * lightColor * pow(abs(specular), specularPower);
}

// The squared length of a vector
float lengthSquared(float3 v1)
{
    return v1.x * v1.x + v1.y * v1.y + v1.z * v1.z;
}

//  Our pixel Shader

float4 PixelShader(VertexShaderOutput input)
{
    float4 baseColor = DiffuseTexture.Sample(textureSampler, input.TexCoord);

    float4 diffuseLight = float4(0, 0, 0, 0);
    float4 specularLight = float4(0, 0, 0, 0);

    //calculate our viewDirection
    float3 cameraDirection = normalize(input.WorldPos - CameraPosition);

    //calculate our sunlight
    diffuseLight += CalcDiffuseLight(input.Normal, SunLightDirection, SunLightColor, SunLightIntensity);
    diffuseSpecular += CalcSpecularLight(input.Normal, SunLightDirection, cameraDirection, SunLightColor, SunLightIntensity);

    //calculate our pointLights
    [loop]
    for (int i = 0; i < MaxLightsRendered; i++)
    {
        float3 PointLightDirection = input.WorldPos - PointLightPosition[i];
                           
        float DistanceSq = lengthSquared(PointLightDirection );



        float radius = PointLightRadius[i];
             
        [branch]
        if (DistanceSq < abs(radius * radius))
        {
            float Distance = sqrt(DistanceSq);

            PointLightDirection /= Distance;

            float du = Distance / (1 - DistanceSq / (radius * radius - 1));

            float denom = du / abs(radius) + 1;

            //The attenuation is the falloff of the light depending on distance basically
            float attenuation = 1 / (denom * denom);

            diffuseLight += CalcDiffuseLight(input.Normal, PointLightDirection, PointLightColor[i], PointLightIntensity[i]) * attenuation; 
            
            specularLight += CalcSpecularLight(input.Normal, PointLightDirection, cameraDirection, PointLightColor[i], PointLightIntensity[i]) * attenuation;
        }
    }

    return diffuseLight * baseColor + diffuseSpecular;
}

technique BasicLightShader
{
    pass Pass1
    {
        VertexShader = compile vs_5_0 VertexShader();
        PixelShader = compile ps_5_0 PixelShader();
    }
}