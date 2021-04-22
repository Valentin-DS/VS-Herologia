using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3D
{
    class Shaderlab : Level
    {
        Effect shaderGlass;
        Light whiteLight;
        public Shaderlab(MainCharacter mc)
        {
            this.models3D = new List<GraphicModel>();
            this.models2D = new List<GraphicModel>();
            this.characters = new List<Character>()
            {
                mc
            };

            depository = "Shaderlab\\";
        }

        public override void Load(ContentManager Content, Effect shader)
        {
            whiteLight = new Light(new Vector3(0, 3, 5), new Vector4(1, 1, 1, 1), 1.0f, 15);
            this.shaderGlass = Content.Load<Effect>(depository + "glass");

            Texture2D[] textures = new Texture2D[7];
            Texture2D[] normals = new Texture2D[7];
            this.AddModel(Content, textures, "ShaderlabDefaultCube", "Cube", shader, new Material(), Matrix.CreateScale(0.01f), new Vector3(-5, 0, 0), Vector3.Up, 0f, ModelType.ThreeDimensional, false);
            this.AddModel(Content, textures, "ShaderlabDefaultPlane", "Plane", shader, new Material(), Matrix.CreateScale(0.01f), new Vector3(5, 0, 0), Vector3.Up, 0f, ModelType.TwoDimensional, false);
        }

        public override void Update(GameTime gt)
        {
            whiteLight.Update(gt);
        }

        public override void Draw(Camera camera)
        {
            base.Draw(camera);
        }
    }
}
