using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3D
{
    public class Light
    {
        private static Vector3 ambientPosition = new Vector3(0, 50, 0);
        private static float ambientIntensity = 0.2f;
        private static Vector4 ambientColor = new Vector4(1, 1, 1, 1);
        private const int MAXLIGHT = 20;
        private static int lightNumber = 0;

        private static Light[] lights = new Light[MAXLIGHT];
        private static Vector3[] positions = new Vector3[MAXLIGHT];
        private static Vector4[] diffuseColors = new Vector4[MAXLIGHT];
        private static Vector4[] specularColors = new Vector4[MAXLIGHT];
        private static float[] radii = new float[MAXLIGHT];
        private static float[] intensities = new float[MAXLIGHT];

        private Vector3 position;
        private Vector4 diffuseColor;
        private Vector4 specularColor;
        private float radius;
        private float intensity;

        public Light(Vector3 position, Vector4 diffuseColor, Vector4 specularColor, float intensity, float radius)
        {
            this.position = position;
            this.intensity = intensity;
            this.diffuseColor = diffuseColor;
            this.specularColor = specularColor;
            this.radius = radius;
            if (lightNumber < MAXLIGHT)
            {
                lights[lightNumber] = this;
                lightNumber++;
            }
        }

        public Light(Vector3 position, Vector4 color, float intensity, float radius)
        {
            this.position = position;
            this.intensity = intensity;
            this.diffuseColor = color;
            this.specularColor = color;
            this.radius = radius;
            if (lightNumber < MAXLIGHT)
            {
                lights[lightNumber] = this;
                lightNumber++;
            }
        }

        public static void clear()
        {
            lightNumber = 0;
            lights = new Light[MAXLIGHT];
            positions = new Vector3[MAXLIGHT];
            diffuseColors = new Vector4[MAXLIGHT];
            specularColors = new Vector4[MAXLIGHT];
            radii = new float[MAXLIGHT];
            intensities = new float[MAXLIGHT];
            ambientPosition = new Vector3(0, 50, 0);
            ambientIntensity = 0.6f;
            ambientColor = new Vector4(1, 1, 1, 1);
    }

        public static int getLightNumber()
        {
            return lightNumber;
        }

        public static void UpdateArrays()
        {
            for (int i = 0; i < lightNumber; i++)
            {
                positions[i] = lights[i].getPosition();
                diffuseColors[i] = lights[i].getDiffuseColor();
                specularColors[i] = lights[i].getSpecularColor();
                intensities[i] = lights[i].getIntensity();
                radii[i] = lights[i].getRadius();
            }
        }

        public static Vector3[] getPositions()
        {
            return positions;
        }

        public static Vector4[] getDiffuseColors()
        {
            return diffuseColors;
        }

        public static Vector4[] getSpecularColors()
        {
            return specularColors;
        }

        public static float[] getIntensities()
        {
            return intensities;
        }

        public static float[] getRadii()
        {
            return radii;
        }

        public static float getAmbientIntensity()
        {
            return ambientIntensity;
        }

        public static void setAmbientIntensity(float newIntensity)
        {
            ambientIntensity = newIntensity;
        }

        public static Vector4 getAmbientColor()
        {
            return ambientColor;
        }

        public static void changeAmbientColor(Vector4 newAmbientColor)
        {
            ambientColor = newAmbientColor;
        }

        public static Vector3 getAmbientPosition()
        {
            return ambientPosition;
        }

        public static void changeAmbientPosition(Vector3 newPosition)
        {
            ambientPosition = newPosition;
        }

        public Vector3 getPosition()
        {
            return position;
        }

        public float getIntensity()
        {
            return intensity;
        }

        public float getRadius()
        {
            return radius;
        }

        public void setRadius(float radius)
        {
            this.radius = radius;
        }

        public Vector4 getDiffuseColor()
        {
            return diffuseColor;
        }

        public void changeDiffuseColor(Vector4 newDiffuseColor)
        {
            diffuseColor = newDiffuseColor;
        }

        public Vector4 getSpecularColor()
        {
            return specularColor;
        }

        public void changeSpecularColor(Vector4 newSpecularColor)
        {
            specularColor = newSpecularColor;
        }

        public void changeColor(Vector4 newColor)
        {
            diffuseColor = newColor;
            specularColor = newColor;
        }

        public void Update(GameTime gt)
        {
            if (ManageKeys.IsPressed(Keys.Left))
            {
                position.X -= 0.1f;
            }
            if (ManageKeys.IsPressed(Keys.Right))
            {
                position.X += 0.1f;
            }
            if (ManageKeys.IsPressed(Keys.Up))
            {
                position.Z -= 0.1f;
            }
            if (ManageKeys.IsPressed(Keys.Down))
            {
                position.Z += 0.1f;
            }
            if (ManageKeys.IsPressed(Keys.NumPad0))
            {
                position.Y -= 0.1f;
            }
            if (ManageKeys.IsPressed(Keys.NumPad1))
            {
                position.Y += 0.1f;
            }
            if (ManageKeys.IsJustPressed(Keys.A))
            {
                if (ambientIntensity == 0.2f)
                    ambientIntensity = 0.0f;
                else
                    ambientIntensity = 0.2f;
            }
        }
    }
}
