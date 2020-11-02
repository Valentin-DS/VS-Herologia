using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3D
{
    public class Material
    {
        private float ambientIntensity;
        private float diffuseIntensity;
        private float specularIntensity;
        private float shininess;

        public Material(float ambientIntensity, float diffuseIntensity, float specularIntensity, float shininess)
        {
            this.ambientIntensity = ambientIntensity;
            this.diffuseIntensity = diffuseIntensity;
            this.specularIntensity = specularIntensity;
            this.shininess = shininess;
        }

        public Material()
        {
            this.ambientIntensity = 1;
            this.diffuseIntensity = 1;
            this.specularIntensity = 1;
            this.shininess = 100;
        }

        public float getAmbientIntensity()
        {
            return ambientIntensity;
        }

        public float getDiffuseIntensity()
        {
            return diffuseIntensity;
        }

        public float getSpecularIntensity()
        {
            return specularIntensity;
        }

        public float getShininess()
        {
            return shininess;
        }
    }
}
