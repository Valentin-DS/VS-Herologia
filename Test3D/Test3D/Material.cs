using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3D
{
    /**
     * <summary>Une classe permettant de gérer des effets sur un Material</summary>
     */
    public class Material
    {
        /**
        * <summary>Intensité ambiante du Material</summary>
        */
        private float ambientIntensity;
        /**
        <summary>Intensité diffuse du Material</summary>
        */
        private float diffuseIntensity;
        /**
        <summary>Intensité du specular du Material</summary>
        */
        private float specularIntensity;
        /**
        <summary>Intensité de la brillance du Material</summary>
        */
        private float shininess;


        /**
        * <summary>Constructeur du Material</summary>
        */
        public Material(float ambientIntensity, float diffuseIntensity, float specularIntensity, float shininess)
        {
            this.ambientIntensity = ambientIntensity;
            this.diffuseIntensity = diffuseIntensity;
            this.specularIntensity = specularIntensity;
            this.shininess = shininess;
        }

        /**
        * <summary>Constructeur du Material</summary>
        */
        public Material()
        {
            this.ambientIntensity = 1;
            this.diffuseIntensity = 1;
            this.specularIntensity = 1;
            this.shininess = 100;
        }

        /**
        * <summary>Recupere l'intensité ambiante du Material</summary>
        */
        public float getAmbientIntensity()
        {
            return ambientIntensity;
        }

        /**
        * <summary>Recupere l'intensité diffuse du Material</summary>
        */
        public float getDiffuseIntensity()
        {
            return diffuseIntensity;
        }

        /**
        <summary>Recupere l'intensité du specular du Material</summary>
        */
        public float getSpecularIntensity()
        {
            return specularIntensity;
        }

        /**
        <summary>Recupere l'intensité de la brillance du Material</summary>
        */
        public float getShininess()
        {
            return shininess;
        }
    }
}
