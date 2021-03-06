﻿using Microsoft.Xna.Framework;
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
        public Solbourg(MainCharacter mc)
        {
            this.objects = new List<object>();
            this.models3D = new List<GraphicModel>();
            this.models2D = new List<GraphicModel>();
            this.characters = new List<Character>()
            {
                mc
            };

            collider = new List<List<char>>();
            depository = "Solbourg\\";
        }

        override
        public void Load(ContentManager Content, Effect shader)
        {
            //On ajoute le model avec la méthode DrawModel
            //on scale toujours par 1/100 les objets, la méthode d'importation fait par defaut un zoom de 100.
            //Create world est composé de trois Vector3, dont le premier est la position de l'origine blender du model sur notre grille
            Texture2D[] textures = new Texture2D[7];
            Texture2D[] normals = new Texture2D[7];

            this.AddModel(Content, textures, "Solbourg_Sol", "Sol_1", shader, new Material(0.8f, 1f, 0f, 1), Matrix.CreateScale(0.03f), new Vector3(5f, -1, 5f), ModelType.ThreeDimensional);
            this.AddModel(Content, textures, normals, "Solbourg_Maison", "Maison_1", shader, new Material(1f, 1f, 0f, 1f), Matrix.CreateScale(0.01f), new Vector3(0f, -1f, 10f), ModelType.ThreeDimensional);
            this.AddModel(Content, textures, "Solbourg_Fontaine", "Fontaine_1", shader, new Material(1, 1, 0, 1), Matrix.CreateScale(0.0025f), new Vector3(9f, -1f, 10f), ModelType.ThreeDimensional);
            this.AddModel(Content, textures, "Solbourg_Muraille", "Muraille_1", shader, new Material(1, 1, 0, 1), Matrix.CreateScale(0.004f), new Vector3(-10f, -1, -10f), ModelType.ThreeDimensional);
            this.AddModel(Content, textures, "Solbourg_Puits", "Puits_1", shader, new Material(1, 1, 0, 1), Matrix.CreateScale(0.004f), new Vector3(2f, -1, 5f), ModelType.ThreeDimensional);
            this.AddModel(Content, textures, "Solbourg_Buisson", "Buisson_1", shader, new Material(1, 1, 0, 1), Matrix.CreateScale(0.006f), new Vector3(8f, -1, 2.05f), ModelType.TwoDimensional);
            this.AddModel(textures, "Solbourg_Buisson", "Buisson_2", shader, new Material(1, 1, 0, 1), Matrix.CreateScale(0.006f), new Vector3(8f, -1, 5.05f), ModelType.TwoDimensional);
            this.AddModel(textures, "Solbourg_Buisson", "Buisson_3", shader, new Material(1, 1, 0, 1), Matrix.CreateScale(0.006f), new Vector3(8f, -1, 4.05f), ModelType.TwoDimensional);
            this.AddModel(textures, "Solbourg_Buisson", "Buisson_4", shader, new Material(1, 1, 0, 1), Matrix.CreateScale(0.006f), new Vector3(10f, -1, 7.05f), ModelType.TwoDimensional);
            this.AddModel(Content, textures, "Solbourg_Palace", "Palace_1", shader, new Material(1, 1, 0, 1), Matrix.CreateScale(0.0015f), new Vector3(0, -1, -10f), ModelType.ThreeDimensional);
            this.AddModel(Content, textures, "Solbourg_Commerce", "Commerce_1", shader, new Material(1, 1, 0, 1), Matrix.CreateScale(0.005f), new Vector3(-6, -1, 8f), ModelType.ThreeDimensional);


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

        public override void Update(GameTime gt)
        {
            //vide pour l'instant, utile pour faire des tests, sur les lumières ou les objets mobiles par exemple
        }

        public new void Draw(Camera camera)
        {
            base.Draw(camera);
        }
    }
}
