using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3D
{
    public class GraphicModel
    {
        protected string importName;
        protected string instanceName;
        protected Texture2D[] textures;
        protected Texture2D texture;
        protected Texture2D[] normalMaps;
        protected bool defaultTexture;
        protected bool normalMapped;
        protected Model model;
        protected Matrix mMatrix;
        protected Material material;

        public Matrix GetMatrix()
        {
            return this.mMatrix;
        }

        public string GetInstanceName()
        {
            return this.instanceName;
        }

        public string GetImportName()
        {
            return this.importName;
        }

        public Model GetModel()
        {
            return this.model;
        }

        public GraphicModel(string importName, string instanceName, Texture2D[] textures, Material material, Model model, Matrix mMatrix)
        {
            this.textures = textures;
            this.material = material;
            this.model = model;
            this.mMatrix = mMatrix;
            this.importName = importName;
            this.instanceName = instanceName;
            defaultTexture = true;
            normalMapped = false;
        }

        public GraphicModel(string importName, string instanceName, Texture2D[] textures, Texture2D[] normalMaps, Material material, Model model, Matrix mMatrix)
        {
            this.textures = textures;
            this.normalMaps = normalMaps;
            this.material = material;
            this.model = model;
            this.mMatrix = mMatrix;
            this.importName = importName;
            this.instanceName = instanceName;
            defaultTexture = true;
            normalMapped = true;
        }

        public GraphicModel(string importName, string instanceName, Texture2D texture, Material material, Model model, Matrix mMatrix)
        {
            this.importName = importName;
            this.instanceName = instanceName;
            this.texture = texture;
            this.material = material;
            this.model = model;
            this.mMatrix = mMatrix;
            defaultTexture = false;
        }

        public void Update(Texture2D texture, Matrix mMatrix)
        {
            this.texture = texture;
            this.mMatrix = mMatrix;
        }

        public void Draw(Camera camera)
        {
            Light.UpdateArrays();

            Matrix[] modelTransforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(modelTransforms);
            int i = 0;
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect currentEffect in mesh.Effects)
                {
                    var _diffuseColor = currentEffect.Parameters["xDiffuseColor"];
                    var _specularColor = currentEffect.Parameters["xSpecularColor"];
                    var _intensity = currentEffect.Parameters["xLightIntensity"];
                    var _lightPos = currentEffect.Parameters["xLightPos"];
                    var _lightRadius = currentEffect.Parameters["xLightRadius"];

                    Matrix worldMatrix = modelTransforms[mesh.ParentBone.Index] * mMatrix;
                    currentEffect.Parameters["xWorldViewProjection"].SetValue(worldMatrix * camera.getView() * camera.getProjection());
                    if (defaultTexture)
                    {
                        if (textures != null && textures[i] != null)
                        {
                            if (normalMapped && normalMaps[i] != null)
                                currentEffect.Parameters["xNormalMap"].SetValue(normalMaps[i]);
                            currentEffect.Parameters["xTexture"].SetValue(textures[i]);
                        }
                    }
                    else
                    {
                        currentEffect.Parameters["xTexture"].SetValue(texture);
                    }
                    currentEffect.CurrentTechnique = currentEffect.Techniques["SimplestTex"];
                    if (normalMapped && normalMaps != null)
                    {
                        if (normalMaps[i] != null)
                            currentEffect.CurrentTechnique = currentEffect.Techniques["NormalTex"];
                    }
                    currentEffect.Parameters["xWorld"].SetValue(worldMatrix);
                    currentEffect.Parameters["xWorldInverseTranspose"].SetValue(Matrix.Transpose(Matrix.Invert(mesh.ParentBone.Transform * mMatrix)));
                    currentEffect.Parameters["xAmbientIntensity"].SetValue(Light.getAmbientIntensity());
                    currentEffect.Parameters["xAmbientColor"].SetValue(Light.getAmbientColor());
                    currentEffect.Parameters["xAmbientPosition"].SetValue(Light.getAmbientPosition());
                    currentEffect.Parameters["xView"].SetValue(camera.getPosition());

                    currentEffect.Parameters["xMaterialAmbient"].SetValue(material.getAmbientIntensity());
                    currentEffect.Parameters["xMaterialDiffuse"].SetValue(material.getDiffuseIntensity());
                    currentEffect.Parameters["xMaterialSpecular"].SetValue(material.getSpecularIntensity());
                    currentEffect.Parameters["xMaterialShininess"].SetValue(material.getShininess());

                    _diffuseColor.SetValue(Light.getDiffuseColors());
                    _specularColor.SetValue(Light.getSpecularColors());
                    _intensity.SetValue(Light.getIntensities());
                    _lightPos.SetValue(Light.getPositions());
                    _lightRadius.SetValue(Light.getRadii());

                    currentEffect.Parameters["xLightNumber"].SetValue(Light.getLightNumber());

                    i++;
                }

                mesh.Draw();
            }
        }
    }
}
