using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3D
{
    public class MainCharacter : Character
    {
        private Texture2D subTexture;
        private int line;
        private int column;
        private float timer;
        private enum movementMode { STAND, WALK, RUN, SWIM };
        private movementMode currentMovementMode;

        public MainCharacter(Model model, Texture2D texture, Matrix position, Vector2 positionOnGrid, Effect shader)
        {
            this.position = position;
            Texture2D[] textures = new Texture2D[7];
            int i = 0;
            foreach (ModelMesh mesh in model.Meshes)
                foreach (BasicEffect currentEffect in mesh.Effects)
                    textures[i++] = currentEffect.Texture;
            foreach (ModelMesh mesh in model.Meshes)
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                    meshPart.Effect = shader.Clone();
            this.texture = texture;
            this.positionOnGrid = positionOnGrid;
            subTexture = new Texture2D(texture.GraphicsDevice, 100, 100);
            line = 2;
            column = 0;
            timer = 0;
            this.model = new GraphicModel("Lissandra", "Lissandra", texture, new Material(), model, position);
            currentMovementMode = movementMode.STAND;
        }

        public float getSpeed()
        {
            if (currentMovementMode == movementMode.RUN)
            {
                return 0.15f;
            }
            else if (currentMovementMode == movementMode.WALK)
            {
                return 0.1f;
            }
            else if (currentMovementMode == movementMode.STAND)
            {
                return 0;
            }
            else if (currentMovementMode == movementMode.SWIM)
            {
                return 0.08f;
            }
            else
            {
                return -1;
            }
        }

        private void Animate(GameTime gt)
        {
            if (currentMovementMode != movementMode.STAND)
            {
                timer += (float)gt.ElapsedGameTime.TotalMilliseconds / 2;
                if (timer >= 60)
                {
                    column++;
                    timer = 0;
                    if ((column + 1) % 6 == 1)
                    {
                        column -= 6;
                    }
                }
            }


            Color[] imageData = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(imageData);
            Color[] color = new Color[100 * 100];
            for (int x = 0; x < 100; x++)
                for (int y = 0; y < 100; y++)
                    color[x + y * 100] = imageData[x + 100 * column + (y + 100 * line) * 2400];
            subTexture.SetData<Color>(color);
        }

        override
        public void Update(GameTime gt)
        {
            Animate(gt);
            model.Update(subTexture, position);
        }

        public void InitializeMove(Keys k)
        {
            if (k == Keys.Z)
            {
                if (currentMovementMode == movementMode.STAND)
                {
                    column = 6;
                }
                if (ManageKeys.IsPressed(Keys.LeftShift))
                {
                    currentMovementMode = movementMode.RUN;
                    line = 0;
                }
                else
                {
                    currentMovementMode = movementMode.WALK;
                    line = 1;
                }
            }
            else if (k == Keys.S)
            {
                if (currentMovementMode == movementMode.STAND)
                {
                    column = 0;
                }
                if (ManageKeys.IsPressed(Keys.LeftShift))
                {
                    currentMovementMode = movementMode.RUN;
                    line = 0;
                }
                else
                {
                    currentMovementMode = movementMode.WALK;
                    line = 1;
                }
            }
            else if (k == Keys.Q)
            {
                if (currentMovementMode == movementMode.STAND)
                {
                    column = 12;
                }
                if (ManageKeys.IsPressed(Keys.LeftShift))
                {
                    currentMovementMode = movementMode.RUN;
                    line = 0;
                }
                else
                {
                    currentMovementMode = movementMode.WALK;
                    line = 1;
                }
            }
            else if (k == Keys.D)
            {
                if (currentMovementMode == movementMode.STAND)
                {
                    column = 18;
                }
                if (ManageKeys.IsPressed(Keys.LeftShift))
                {
                    currentMovementMode = movementMode.RUN;
                    line = 0;
                }
                else
                {
                    currentMovementMode = movementMode.WALK;
                    line = 1;
                }
            }
        }

        public void Move(Keys k, float speed)
        {
            int xCoord = (int)position.Translation.X;
            int zCoord = (int)position.Translation.Z;
            if (k == Keys.Z)
            {
                position *= Matrix.CreateTranslation(0, 0, -speed);
                if (zCoord - (int)position.Translation.Z >= 1)
                {
                    positionOnGrid.Y--;
                }
            }
            else if (k == Keys.S)
            {
                position *= Matrix.CreateTranslation(0, 0, speed);
                if ((int)position.Translation.Z - zCoord >= 1)
                {
                    positionOnGrid.Y++;
                }
            }
            else if (k == Keys.Q)
            {
                position *= Matrix.CreateTranslation(-speed, 0, 0);
                if (xCoord - (int)position.Translation.X >= 1)
                {
                    positionOnGrid.X--;
                }
            }
            else if (k == Keys.D)
            {
                position *= Matrix.CreateTranslation(speed, 0, 0);
                if ((int)position.Translation.X - xCoord >= 1)
                {
                    positionOnGrid.X++;
                }
            }
        }
        public void Climb(Keys k, float speed)
        {
            if (k == Keys.Z)
            {
                position *= Matrix.CreateTranslation(0, speed/2f, 0);
            }
            else if (k == Keys.S)
            {
                position *= Matrix.CreateTranslation(0, -speed/2f, 0);
            }
        }

        public void Pause()
        {
            if (currentMovementMode != movementMode.STAND)
            {
                line = 2;
                column = column / 6;
                currentMovementMode = movementMode.STAND;
            }
        }

        override
        public void Draw(Camera camera)
        {
            model.Draw(camera);
        }
    }
}
