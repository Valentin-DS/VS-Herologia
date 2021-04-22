using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3D
{
    public class Face
    {
        private Vector3 x1;
        private Vector3 x2;
        private Vector3 z1;
        private Vector3 z2;

        public Vector3 X1 { get { return this.x1; } }
        public Vector3 X2 { get { return this.x2; } }
        public Vector3 Z1 { get { return this.z1; } }
        public Vector3 Z2 { get { return this.z2; } }

        public Face(Vector3 x1, Vector3 x2, Vector3 z1, Vector3 z2)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.z1 = z1;
            this.z2 = z2;
        }
    }
}
