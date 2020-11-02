using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3D
{
    public class Camera
    {
        private Vector3 position;
        private Vector3 target;
        private Vector3 viewVector;
        private Matrix view;
        private Matrix projection;

        public Camera(Vector3 position, Vector3 target, GraphicsDeviceManager graphics)
        {
            this.position = position;
            this.target = target;
            viewVector = Vector3.Transform(target - position, Matrix.CreateRotationY(0));
            viewVector.Normalize();
            view = Matrix.CreateLookAt(position, target, new Vector3(0f, 1f, 0f));// Y up
            projection = Matrix.CreatePerspectiveFieldOfView(
                         MathHelper.ToRadians(45f), graphics.
                         GraphicsDevice.Viewport.AspectRatio,
                         1f, 1000f); // 1000 = distance d'affichage max
        }

        public Vector3 getPosition()
        {
            return position;
        }

        public Matrix getView()
        {
            return view;
        }

        public Matrix getProjection()
        {
            return projection;
        }

        public Vector3 getViewVector()
        {
            return viewVector;
        }

        public void Translate(float x, float y , float z)
        {
            position.X += x;
            position.Y += y;
            position.Z += z;
        }

        public void Update(GameTime gt, Vector3 target)
        {
            this.target = target;
            viewVector = Vector3.Transform(target - position, Matrix.CreateRotationY(0));
            viewVector.Normalize();
            view = Matrix.CreateLookAt(position, target, new Vector3(0f, 1f, 0f));
        }
    }
}
