using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3D
{
    public abstract class Level
    {
        public enum ModelType { TwoDimensional, ThreeDimensional }
        protected List<object> objects;
        protected List<GraphicModel> models3D;
        public List<GraphicModel> models2D;
        protected List<Character> characters;
        protected List<List<Char>> collider;
        protected string depository;
        private IEnumerable<GraphicModel> filteredModels2D;

        public List<List<Char>> getCollider()
        {
            return collider;
        }

        protected Model LoadModel(ContentManager Content, string assetName, Effect shader, out Texture2D[] textures)
        {
            Model newModel = Content.Load<Model>(assetName);
            textures = new Texture2D[7];
            int i = 0;
            foreach (ModelMesh mesh in newModel.Meshes)
                foreach (BasicEffect currentEffect in mesh.Effects)
                    textures[i++] = currentEffect.Texture;
            foreach (ModelMesh mesh in newModel.Meshes)
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                    meshPart.Effect = shader.Clone();
            return newModel;
        }

        protected Model LoadModel(ContentManager Content, string assetName, Effect shader, out Texture2D[] textures, out Texture2D[] normals)
        {
            Model newModel = Content.Load<Model>(assetName);
            textures = new Texture2D[7];
            normals = new Texture2D[7];
            int i = 0;
            foreach (ModelMesh mesh in newModel.Meshes)
            {
                foreach (BasicEffect currentEffect in mesh.Effects)
                {
                    // Console.WriteLine(currentEffect.Texture.Name.Replace("Texture", "Normal").Replace("_0", ""));
                    textures[i] = currentEffect.Texture;
                    string normalMapRelativePath = currentEffect.Texture.Name.Replace("Texture", "Normal").Replace("_0", "");
                    if (File.Exists(Constants.XNB_CONTENT_PATH + normalMapRelativePath + Constants.XNB_EXTENSION))
                    {
                        normals[i] = Content.Load<Texture2D>(normalMapRelativePath);
                    }

                    i++;
                }
            }

            foreach (ModelMesh mesh in newModel.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = shader.Clone();
                }
            }

            return newModel;
        }

        protected void AddModel(ContentManager content, Texture2D[] textures, string importName, string instanceName, Effect shader, Material material, Matrix scale, Vector3 position, ModelType modelType)
        {
            textures = new Texture2D[7];
            Model model = LoadModel(content, depository + importName, shader, out textures);
            GraphicModel graphicModel = new GraphicModel(depository + importName, instanceName, textures, material, model, scale * Matrix.CreateWorld(position, Vector3.Forward, Vector3.Up));
            if (modelType.Equals(ModelType.ThreeDimensional))
            {
                this.models3D.Add(graphicModel);
            }
            else
            {
                this.models2D.Add(graphicModel);
            }
        }

        protected void AddModel(Texture2D[] textures, string importName, string instanceName, Effect shader, Material material, Matrix scale, Vector3 position, ModelType modelType)
        {
            textures = new Texture2D[7];
            if (modelType.Equals(ModelType.ThreeDimensional))
            {
                GraphicModel graphicModel = new GraphicModel(depository + importName,
                                                             instanceName,
                                                             textures,
                                                             material,
                                                             this.models3D.First(m => m.GetImportName().Equals(depository + importName)).GetModel(),
                                                             scale * Matrix.CreateWorld(position, Vector3.Forward, Vector3.Up));
                this.models3D.Add(graphicModel);
            }
            else
            {
                GraphicModel graphicModel = new GraphicModel(depository + importName, instanceName, textures, material,
                                                             this.models2D.First(m => m.GetImportName().Equals(depository + importName)).GetModel(),
                                                             scale * Matrix.CreateWorld(position, Vector3.Forward, Vector3.Up));
                this.models2D.Add(graphicModel);
            }
        }

        protected void AddModel(ContentManager content, Texture2D[] textures, Texture2D[] normalMaps, string importName, string instanceName, Effect shader, Material material, Matrix scale, Vector3 position, ModelType modelType)
        {
            textures = new Texture2D[7];
            normalMaps = new Texture2D[7];
            Model model = LoadModel(content, depository + importName, shader, out textures, out normalMaps);
            GraphicModel graphicModel = new GraphicModel(depository + importName, instanceName, textures, normalMaps, material, model, scale * Matrix.CreateWorld(position, Vector3.Forward, Vector3.Up));
            if (modelType.Equals(ModelType.ThreeDimensional))
            {
                this.models3D.Add(graphicModel);
            }
            else
            {
                this.models2D.Add(graphicModel);
            }
        }

        protected void AddModel(Texture2D[] textures, Texture2D[] normalMaps, string importName, string instanceName, Effect shader, Material material, Matrix scale, Vector3 position, ModelType modelType)
        {
            textures = new Texture2D[7];
            normalMaps = new Texture2D[7];
            if (modelType.Equals(ModelType.ThreeDimensional))
            {
                GraphicModel graphicModel = new GraphicModel(depository + importName, instanceName, textures, material,
                                                             this.models3D.First(m => m.GetImportName().Equals(depository + importName)).GetModel(),
                                                             scale * Matrix.CreateWorld(position, Vector3.Forward, Vector3.Up));
                this.models3D.Add(graphicModel);
            }
            else
            {
                GraphicModel graphicModel = new GraphicModel(depository + importName, instanceName, textures, material,
                                                             this.models2D.First(m => m.GetImportName().Equals(depository + importName)).GetModel(),
                                                             scale * Matrix.CreateWorld(position, Vector3.Forward, Vector3.Up));
                this.models2D.Add(graphicModel);
            }
        }

        public abstract void Load(ContentManager Content, Effect shader);

        public abstract void Update(GameTime gt);

        public virtual void Draw(Camera camera)
        {
            foreach (GraphicModel gm in this.models3D)
            {
                gm.Draw(camera);
            }

            this.filteredModels2D = this.models2D.OrderByDescending(model => (camera.getPosition().Z - model.GetMatrix().Translation.Z));
            float mcToCamZDistance = camera.getPosition().Z - this.characters[0].getPosition().Z;
            bool hasMcBeenDrawn = false;
            foreach (GraphicModel gm in filteredModels2D)
            {
                float currentZDistance = camera.getPosition().Z - gm.GetMatrix().Translation.Z;
                if (mcToCamZDistance > currentZDistance && !hasMcBeenDrawn)
                {
                    this.characters[0].Draw(camera);
                    hasMcBeenDrawn = true;
                }

                gm.Draw(camera);
            }

            if (!hasMcBeenDrawn)
            {
                this.characters[0].Draw(camera);
            }
        }
    }
}
