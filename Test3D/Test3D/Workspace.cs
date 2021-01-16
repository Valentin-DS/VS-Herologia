using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3D
{
    public class Workspace : Level
    {

        Light whiteLight;
        //Light whiteLight2;
        Texture2D texture;

        public Workspace(MainCharacter mc)
        {
            this.models3D = new List<GraphicModel>();
            this.models2D = new List<GraphicModel>();
            this.characters = new List<Character>()
            {
                mc
            };

            collider = new List<List<char>>();
            depository = "workspace\\";
        }

        override
        public void Load(ContentManager Content, Effect shader)
        {
            whiteLight = new Light(new Vector3(7, 2, 7), new Vector4(1, 1, 1, 1), 1.0f, 4);
            //whiteLight2 = new Light(new Vector3(3, 2, 7), new Vector4(1, 1, 1, 1), 1.0f, 4);

            Texture2D[] textures = new Texture2D[7];
            Texture2D[] normals = new Texture2D[7];

            this.AddModel(Content, textures, "ampoule", "Ampoule", shader, new Material(), Matrix.CreateScale(0.001f), new Vector3(7f, 2f, 7f), ModelType.TwoDimensional);
            this.AddModel(Content, textures, "hache_king_size", "Hache King Size", shader, new Material(), Matrix.CreateScale(0.01f), new Vector3(3, 0, 3), ModelType.ThreeDimensional);
            this.AddModel(Content, textures, "hache", "Hache", shader, new Material(), Matrix.CreateScale(0.01f), new Vector3(0, 0, 3), ModelType.ThreeDimensional);
            this.AddModel(Content, textures, "escalier", "Escalier", shader, new Material(), Matrix.CreateScale(0.01f), new Vector3(9.5f, -1.40715f, 10.5f), ModelType.ThreeDimensional);
            this.AddModel(Content, textures, normals, "cuir", "Cuir NormalMapped", shader, new Material(1, 1, 1, 100), Matrix.CreateScale(0.01f), new Vector3(1f, -1.5f, 17f), ModelType.ThreeDimensional);
            this.AddModel(textures, "cuir", "Cuir", shader, new Material(1, 1, 1, 100), Matrix.CreateScale(0.01f), new Vector3(5f, -1.5f, 17f), ModelType.ThreeDimensional);
            this.AddModel(Content, textures, normals, "Solbourg_Maison", "Maison NormalMapped", shader, new Material(1, 1, 1, 100), Matrix.CreateScale(0.01f), new Vector3(0f, -1f, 6f), ModelType.ThreeDimensional);
            this.AddModel(textures, "Solbourg_Maison", "Maison", shader, new Material(1, 1, 1, 100), Matrix.CreateScale(0.01f), new Vector3(7f, -1f, 6f), ModelType.ThreeDimensional);
            this.AddModel(Content, textures, "grand_plane", "Sol 1", shader, new Material(), Matrix.CreateScale(0.01f), new Vector3(0f, -1, 0f), ModelType.ThreeDimensional);
            this.AddModel(textures, "grand_plane", "Sol 2", shader, new Material(), Matrix.CreateScale(0.01f), new Vector3(0f, -1.5f, 11f), ModelType.ThreeDimensional);
            this.AddModel(Content, textures, "Helicopter", "Hélicoptère 1", shader, new Material(), Matrix.CreateScale(0.01f), new Vector3(7f, -1f, 7f), ModelType.ThreeDimensional);
            this.AddModel(textures, "Helicopter", "Hélicoptère 2", shader, new Material(), Matrix.CreateScale(0.01f), new Vector3(3f, -1f, 7f), ModelType.ThreeDimensional);

            using (StreamReader stream = new StreamReader(Content.RootDirectory + @"\..\..\..\..\.." + "\\map\\grand_plane_collider.txt"))
            {
                string str = "";
                while (str != null)
                {
                    str = stream.ReadLine();
                    if (str == "")
                    {
                        str = stream.ReadLine();
                    }
                    else if (str != null)
                    {
                        List<char> line = new List<char>();
                        foreach (char c in str)
                        {
                            line.Add(c);
                        }
                        collider.Add(line);
                    }
                }
            }
        }

        override
        public void Update(GameTime gt)
        {
            whiteLight.Update(gt);
            this.models2D[0].Update(texture, Matrix.CreateScale(0.001f) * Matrix.CreateRotationX((float)Math.PI / 2f) * Matrix.CreateWorld(new Vector3(whiteLight.getPosition().X - 0.1f, whiteLight.getPosition().Y - 0.1f, whiteLight.getPosition().Z - 0.1f), Vector3.Forward, Vector3.Up));
        }

        override
        public void Draw(Camera camera)
        {
            base.Draw(camera);
        }
    }
}
