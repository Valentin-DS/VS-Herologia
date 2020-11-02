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
    public class Solbourg : Level
    {
        public Solbourg()
        {
            models = new List<GraphicModel>();
            collider = new List<List<char>>();
            depository = "Solbourg\\";
        }

        override
        public void Load(ContentManager Content, Effect shader)
        {
            //ici, on load les objets du level comme dans l'exemple ci dessous:
            //On initialise la liste des textures
            //On load le model avec la méthode LoadModel
            //On l'ajoute à la liste des models du level
            //on scale toujours par 1/100 les objets, la méthode d'importation fait par defaut un zoom de 100.
            //Create world est composé de trois Vector3, dont le premier est la position de l'origine blender du model sur notre grille
            Texture2D[] textures = new Texture2D[7];
            Texture2D[] normals = new Texture2D[7];

            textures = new Texture2D[7];
            Model floor = LoadModel(Content, depository + "Mod_Solbourg_Sol", shader, out textures);
            models.Add(new GraphicModel(textures, new Material(0.8f, 1f, 0f, 100f), floor, Matrix.CreateScale(0.03f) * Matrix.CreateWorld(new Vector3(5f, -1, 5f), Vector3.Forward, Vector3.Up)));
     
            textures = new Texture2D[7];
            normals = new Texture2D[7];
            Model newHouse = LoadModel(Content, depository + "Solbourg_Maison_New", shader, out textures, out normals);
            models.Add(new GraphicModel(textures, normals, new Material(1, 1, 0, 100), newHouse, Matrix.CreateScale(0.01f) * Matrix.CreateWorld(new Vector3(0f, -1f, 10f), Vector3.Forward, Vector3.Up)));
            

            //ici, on importe le fichier de collision
            using (StreamReader stream = new StreamReader(Content.RootDirectory + @"\..\..\..\..\.." + "\\map\\Solbourg.txt"))
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

            //ici tu peux initialiser les lumières que tu veux ajouter, elles sont stockées dans la classe Light, ça simplifie la gestion des shaders
            Light.clear(); //supprime les lumières existantes et réinitialise les paramètres
            Light.setAmbientIntensity(0.8f); //jour = 0.8f, nuit = 0.2f ou moins
            //Light redLight = new Light(new Vector3(7.2f, 1f, 7f), new Vector4(1, 0, 0, 1), 1, 4); //ajoute une lumière avec les paramètres de ton choix
        }

        override
        public void Update(GameTime gt)
        {
            //vide pour l'instant, utile pour faire des tests, sur les lumières ou les objets mobiles par exemple
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
