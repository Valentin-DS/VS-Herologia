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
    public abstract class Level
    {
        protected List<GraphicModel> models;
        protected List<List<Char>> collider;
        protected string depository;

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
                foreach (BasicEffect currentEffect in mesh.Effects)
                {
                    Debug.WriteLine(currentEffect.Texture);
                    textures[i] = currentEffect.Texture;
                    try
                    {
                        normals[i] = Content.Load<Texture2D>(currentEffect.Texture.Name.Replace("Texture", "Normal").Replace("_0", ""));
                    }
                    catch
                    {

                    }
                    i++;
                }
            foreach (ModelMesh mesh in newModel.Meshes)
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                    meshPart.Effect = shader.Clone();
            return newModel;
        }

        public abstract void Load(ContentManager Content, Effect shader);

        public abstract void Update(GameTime gt);

        public abstract void Draw(Camera camera);
    }
}
