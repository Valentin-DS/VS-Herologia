using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3D
{
    public class Character
    {
        protected Matrix position;
        protected Vector2 positionOnGrid;
        protected Texture2D texture;
        protected GraphicModel model;

        public Vector3 getPosition()
        {
            return position.Translation;
        }

        public Vector2 getPositionOnGrid()
        {
            return positionOnGrid;
        }

        public void setPosition(Vector3 position, Camera camera)
        {
            this.position.Translation = position;
            camera.setPosition(new Vector3(position.X, position.Y + 3.5f, position.Z + 7.5f));
        }

        public virtual void Update(GameTime gt)
        {
        }
        public virtual void Draw(Camera camera)
        {
        }
    }
}