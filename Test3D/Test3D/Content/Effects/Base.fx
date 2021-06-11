struct PixelToFrame
{
	float4 Color : COLOR0;
};

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float2 TexCoords : TEXCOORD0;
	float3 Normal : NORMAL0;
	float3 Binormal : BINORMAL0;
	float3 Tangent : TANGENT0;
};

struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float2 TexCoords : TEXCOORD0;
	float3 Normal : TEXCOORD1;
	float3 Position3D : TEXCOORD2;
	float3 View : TEXCOORD3;
	float3x3 WorldToTangentSpace : TEXCOORD4;
};

float3 xView;
float4x4 xWorld;
float4x4 xWorldViewProjection;
Texture xTexture;

sampler TextureSampler = sampler_state { texture = <xTexture>; magfilter = POINT; minfilter = POINT; mipfilter = POINT; AddressU = WRAP; AddressV = WRAP; };

VertexShaderOutput MainVS(VertexShaderInput input)
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

PixelToFrame MainPS(VertexShaderOutput input) : COLOR
{
	PixelToFrame Output = (PixelToFrame)0;
	float4 baseColor = tex2D(TextureSampler, input.TexCoords);
	Output.Color = baseColor;
	return Output;
}

technique SpriteDrawing
{
	pass P0
	{
		VertexShader = compile vs_5_0 MainVS();
		PixelShader = compile ps_5_0 MainPS();
	}
};