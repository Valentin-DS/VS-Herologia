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

        public virtual void Update(GameTime gt)
        {
        }
        public virtual void Draw(Camera camera)
        {
        }
    }
}