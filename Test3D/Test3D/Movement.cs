using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3D
{
    public class Movement
    {
        public static float Xoffset = 0.2f;
        public static float Zoffset = 0.2f;
        public static enum commands : Keys { Gauche = Keys.Q, Droite = Keys.D, Haut = Keys.Z, Bas = Keys.S};
        public static enum notPressed { 
            Gauche = new List<Keys> {commands.Droite, commands.Bas, commands.Haut}, 
            Droite = new List<Keys> {commands.Gauche, commands.Bas, commands.Haut}, 
            Haut = new List<Keys> {commands.Droite, commands.Bas, commands.Gauche}, 
            Bas = new List<Keys> {commands.Droite, commands.Gauche, commands.Haut}
            };

        public static void Update(MainCharacter mc, Camera cam, Level level)
        {
            List<List<char>> collider = level.getCollider();
            Vector2 pos = mc.getPositionOnGrid();
            if (ManageKeys.IsJustUp(Keys.Z) || ManageKeys.IsJustUp(Keys.S) || ManageKeys.IsJustUp(Keys.Q) || ManageKeys.IsJustUp(Keys.D))
            {
                mc.Pause();
            }
            if (ManageKeys.IsPressed(Keys.Z) && ManageKeys.IsUp(Keys.S) && ManageKeys.IsUp(Keys.Q) && ManageKeys.IsUp(Keys.D))
            {
                mc.InitializeMove(Keys.Z);
                float speed = mc.getSpeed();
                /*
                if ("FE".Contains(collider[(int)(1 + mc.getPosition().Z - Zoffset - speed)][(int)(1 + mc.getPosition().X)])
                    && ((mc.getPosition().X % 1 >= 1 - Xoffset && "FE".Contains(collider[(int)(1 + mc.getPosition().Z - Zoffset - speed)][(int)(1 + mc.getPosition().X + Xoffset)]))
                    || (mc.getPosition().X % 1 <= Xoffset && "FE".Contains(collider[(int)(1 + mc.getPosition().Z - Zoffset - speed)][(int)(1 + mc.getPosition().X - Xoffset)]))
                    || (mc.getPosition().X % 1 < 1 - Xoffset && mc.getPosition().X % 1 > Xoffset)))
                {
                */
                    mc.Move(Keys.Z, speed);
                    cam.Translate(0, 0, -speed);
                /*
                    if (collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X)] == 'E')
                    {
                        mc.Climb(Keys.Z, speed);
                        cam.Translate(0, speed / 2f, 0);
                    }
                }
                else
                {
                    mc.Pause();
                }
                */
            }
            // else if (ManageKeys.checkPressedArr(new List<Keys> {Keys.S}, new List<Keys> { Keys.Z, Keys.Q, Keys.D}))
            // else if (ManageKeys.checkPressedArr(new List<Keys> {commandes.Bas}, notPressed.Bas))
            else if (ManageKeys.IsPressed(Keys.S) && ManageKeys.IsUp(Keys.Z) && ManageKeys.IsUp(Keys.Q) && ManageKeys.IsUp(Keys.D))
            {
                mc.InitializeMove(Keys.S);
                float speed = mc.getSpeed();
                /*
                if ("FE".Contains(collider[(int)(1 + mc.getPosition().Z + Zoffset + speed)][(int)(1 + mc.getPosition().X)])
                    && ((mc.getPosition().X % 1 >= 1 - Xoffset && "FE".Contains(collider[(int)(1 + mc.getPosition().Z + Zoffset + speed)][(int)(1 + mc.getPosition().X + Xoffset)]))
                    || (mc.getPosition().X % 1 <= Xoffset && "FE".Contains(collider[(int)(1 + mc.getPosition().Z + Zoffset + speed)][(int)(1 + mc.getPosition().X - Xoffset)]))
                    || (mc.getPosition().X % 1 < 1 - Xoffset && mc.getPosition().X % 1 > Xoffset)))
                {*/
                    mc.Move(Keys.S, speed);
                    cam.Translate(0, 0, +speed);
                /*
                    if (collider[(int)(1 + mc.getPosition().Z - speed)][(int)(1 + mc.getPosition().X)] == 'E')
                    {
                        mc.Climb(Keys.S, speed);
                        cam.Translate(0, -speed / 2f, 0);
                    }
                }
                else
                {
                    mc.Pause();
                }
                */
            }
            else if (ManageKeys.IsPressed(Keys.Q) && ManageKeys.IsUp(Keys.S) && ManageKeys.IsUp(Keys.Z) && ManageKeys.IsUp(Keys.D))
            {
                mc.InitializeMove(Keys.Q);
                float speed = mc.getSpeed();
                /*if ("FE".Contains(collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X - Xoffset - speed)])
                    && ((mc.getPosition().Z % 1 >= 1 - Zoffset && "FE".Contains(collider[(int)(1 + mc.getPosition().Z + Zoffset)][(int)(1 + mc.getPosition().X - Xoffset - speed)]))
                    || (mc.getPosition().Z % 1 <= Zoffset && "FE".Contains(collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X - Xoffset - speed)]))
                    || (mc.getPosition().Z % 1 < 1 - Zoffset && mc.getPosition().Z % 1 > Zoffset)))
                {
                */
                    mc.Move(Keys.Q, speed);
                    cam.Translate(-speed, 0, 0);
                /*}
                else
                {
                    mc.Pause();
                }
                */
            }
            else if (ManageKeys.IsPressed(Keys.D) && ManageKeys.IsUp(Keys.S) && ManageKeys.IsUp(Keys.Q) && ManageKeys.IsUp(Keys.Z))
            {
                mc.InitializeMove(Keys.D);
                float speed = mc.getSpeed();
                /*
                if ("FE".Contains(collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                    && ((mc.getPosition().Z % 1 >= 1 - Zoffset && "FE".Contains(collider[(int)(1 + mc.getPosition().Z + Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)]))
                    || (mc.getPosition().Z % 1 <= Zoffset && "FE".Contains(collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)]))
                    || (mc.getPosition().Z % 1 < 1 - Zoffset && mc.getPosition().Z % 1 > Zoffset)))
                {
                */
                    mc.Move(Keys.D, speed);
                    cam.Translate(speed, 0, 0);
                /*
                }
                else
                {
                    mc.Pause();
                }
                */
            }
            else
            {
                mc.Pause();
            }
        }
    }
}
