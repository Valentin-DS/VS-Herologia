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

        public Workspace()
        {
            models = new List<GraphicModel>();
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

            textures = new Texture2D[7];
            Model ampoule = LoadModel(Content, "pleine2", shader, out textures);
            texture = Content.Load<Texture2D>("ampoule");
            models.Add(new GraphicModel(texture, new Material(), ampoule, Matrix.CreateScale(0.001f) * Matrix.CreateRotationX((float)Math.PI / 2f) * Matrix.CreateWorld(new Vector3(7f, 2f, 7f), Vector3.Forward, Vector3.Up)));
            /*
            Model floor = LoadModel(Content, depository + "grand_plane", shader, out textures);
            models.Add(new GraphicModel(textures, new Material(), floor, Matrix.CreateScale(0.01f) * Matrix.CreateWorld(new Vector3(0f, -1, 0f), Vector3.Forward, Vector3.Up)));
            models.Add(new GraphicModel(textures, new Material(), floor, Matrix.CreateScale(0.01f) * Matrix.CreateWorld(new Vector3(0f, -1.5f, 11f), Vector3.Forward, Vector3.Up)));
            textures = new Texture2D[7];*/
            Model hacheBig = LoadModel(Content, depository + "hache_king_size", shader, out textures);
            models.Add(new GraphicModel(textures, new Material(), hacheBig, Matrix.CreateScale(0.01f) * Matrix.CreateWorld(new Vector3(3, 0, 3), Vector3.Forward, Vector3.Up)));
            textures = new Texture2D[7];
            Model hache = LoadModel(Content, depository + "hache", shader, out textures);
            models.Add(new GraphicModel(textures, new Material(), hache, Matrix.CreateScale(0.01f) * Matrix.CreateWorld(new Vector3(0, 0, 3), Vector3.Forward, Vector3.Up)));
            textures = new Texture2D[7];
            Model escalier = LoadModel(Content, depository + "escalier", shader, out textures);
            models.Add(new GraphicModel(textures, new Material(), escalier, Matrix.CreateScale(0.01f) * Matrix.CreateWorld(new Vector3(9.5f, -1.40715f, 10.5f), Vector3.Forward, Vector3.Up)));
            //
            //Model helico = LoadModel(Content, depository + "Helicopter", shader, out textures, out normals);
            //0,2,12
            //models.Add(new GraphicModel(textures, new Material(), helico, Matrix.CreateWorld(new Vector3(7f, -1f, 7f), Vector3.Forward, Vector3.Up)));
            //models.Add(new GraphicModel(textures, normals, new Material(), helico, Matrix.CreateWorld(new Vector3(3f, -1f, 7f), Vector3.Forward, Vector3.Up)));

            textures = new Texture2D[7];
            Model cuir = LoadModel(Content, depository + "cuir", shader, out textures, out normals);
            models.Add(new GraphicModel(textures, normals, new Material(1, 1, 1, 100), cuir, Matrix.CreateScale(0.01f) * Matrix.CreateWorld(new Vector3(1f, -1.5f, 17f), Vector3.Forward, Vector3.Up)));
            models.Add(new GraphicModel(textures, new Material(1, 1, 1, 100), cuir, Matrix.CreateScale(0.01f) * Matrix.CreateWorld(new Vector3(5f, -1.5f, 17f), Vector3.Forward, Vector3.Up)));

            textures = new Texture2D[7];
            normals = new Texture2D[7];
            Model house = LoadModel(Content, depository + "Solbourg_Maison", shader, out textures, out normals);
            models.Add(new GraphicModel(textures, normals, new Material(1, 1, 1f, 100), house, Matrix.CreateScale(0.01f) * Matrix.CreateWorld(new Vector3(0f, -1f, 6f), Vector3.Forward, Vector3.Up)));
            models.Add(new GraphicModel(textures, new Material(1, 1, 1f, 100), house, Matrix.CreateScale(0.01f) * Matrix.CreateWorld(new Vector3(7f, -1f, 6f), Vector3.Forward, Vector3.Up)));
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
            models[0].Update(texture, Matrix.CreateScale(0.001f) * Matrix.CreateRotationX((float)Math.PI / 2f) * Matrix.CreateWorld(new Vector3(whiteLight.getPosition().X - 0.1f, whiteLight.getPosition().Y - 0.1f, whiteLight.getPosition().Z - 0.1f), Vector3.Forward, Vector3.Up));
        }

        override
        public void Draw(Camera camera)
        {
            foreach (GraphicModel gm in models)
            {
                gm.Draw(camera);
            }
        }
    }
}
