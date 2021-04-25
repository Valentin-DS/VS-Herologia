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
            if (ManageKeys.IsJustUp(commands.Haut) || ManageKeys.IsJustUp(commands.Bas) || ManageKeys.IsJustUp(commands.Gauche) || ManageKeys.IsJustUp(commands.Droite))
            {
                mc.Pause();
            }

            //if (ManageKeys.checkPressedArr(commandes.Haut, notPressed.Haut)){}
            if (ManageKeys.IsPressed(commands.Haut) && ManageKeys.IsUp(commands.Bas) && ManageKeys.IsUp(commands.Gauche) && ManageKeys.IsUp(commands.Droite))
            {
                mc.InitializeMove(commands.Haut);
                float speed = mc.getSpeed();
                if ("FE".Contains(collider[(int)(1 + mc.getPosition().Z - Zoffset - speed)][(int)(1 + mc.getPosition().X)])
                    && ((mc.getPosition().X % 1 >= 1 - Xoffset && "FE".Contains(collider[(int)(1 + mc.getPosition().Z - Zoffset - speed)][(int)(1 + mc.getPosition().X + Xoffset)]))
                    || (mc.getPosition().X % 1 <= Xoffset && "FE".Contains(collider[(int)(1 + mc.getPosition().Z - Zoffset - speed)][(int)(1 + mc.getPosition().X - Xoffset)]))
                    || (mc.getPosition().X % 1 < 1 - Xoffset && mc.getPosition().X % 1 > Xoffset)))
                {
                    mc.Move(commands.Haut, speed);
                    cam.Translate(0, 0, -speed);
                    if (collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X)] == 'E')
                    {
                        mc.Climb(commands.Haut, speed);
                        cam.Translate(0, speed / 2f, 0);
                    }
                }
                else
                {
                    mc.Pause();
                }
            }
            //else if (ManageKeys.checkPressedArr(commandes.Bas, notPressed.Bas)){}
            else if (ManageKeys.IsPressed(commands.Bas) && ManageKeys.IsUp(commands.Haut) && ManageKeys.IsUp(commands.Gauche) && ManageKeys.IsUp(commands.Droite))
            {
                mc.InitializeMove(commands.Bas);
                float speed = mc.getSpeed();
                if ("FE".Contains(collider[(int)(1 + mc.getPosition().Z + Zoffset + speed)][(int)(1 + mc.getPosition().X)])
                    && ((mc.getPosition().X % 1 >= 1 - Xoffset && "FE".Contains(collider[(int)(1 + mc.getPosition().Z + Zoffset + speed)][(int)(1 + mc.getPosition().X + Xoffset)]))
                    || (mc.getPosition().X % 1 <= Xoffset && "FE".Contains(collider[(int)(1 + mc.getPosition().Z + Zoffset + speed)][(int)(1 + mc.getPosition().X - Xoffset)]))
                    || (mc.getPosition().X % 1 < 1 - Xoffset && mc.getPosition().X % 1 > Xoffset)))
                    mc.Move(commands.Bas, speed);
                    cam.Translate(0, 0, +speed);
                    if (collider[(int)(1 + mc.getPosition().Z - speed)][(int)(1 + mc.getPosition().X)] == 'E')
                    {
                        mc.Climb(commands.Bas, speed);
                        cam.Translate(0, -speed / 2f, 0);
                    }
                }
                else
                {
                    mc.Pause();
                }
            }
            //else if (ManageKeys.checkPressedArr(commandes.Gauche, notPressed.Gauche)){}
            else if (ManageKeys.IsPressed(commands.Gauche) && ManageKeys.IsUp(commands.Bas) && ManageKeys.IsUp(commands.Haut) && ManageKeys.IsUp(commands.Droite))
            {
                mc.InitializeMove(commands.Gauche);
                float speed = mc.getSpeed();
                if ("FE".Contains(collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X - Xoffset - speed)])
                    && ((mc.getPosition().Z % 1 >= 1 - Zoffset && "FE".Contains(collider[(int)(1 + mc.getPosition().Z + Zoffset)][(int)(1 + mc.getPosition().X - Xoffset - speed)]))
                    || (mc.getPosition().Z % 1 <= Zoffset && "FE".Contains(collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X - Xoffset - speed)]))
                    || (mc.getPosition().Z % 1 < 1 - Zoffset && mc.getPosition().Z % 1 > Zoffset)))
                {
                    mc.Move(commands.Gauche, speed);
                    cam.Translate(-speed, 0, 0);
                }
                else
                {
                    mc.Pause();
                }
            }
            //else if (ManageKeys.checkPressedArr(commandes.Droite, notPressed.Droite)){}
            else if (ManageKeys.IsPressed(commands.Droite) && ManageKeys.IsUp(commands.Bas) && ManageKeys.IsUp(commands.Gauche) && ManageKeys.IsUp(commands.Haut))
            {
                mc.InitializeMove(commands.Droite);
                float speed = mc.getSpeed();
                if ("FE".Contains(collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                    && ((mc.getPosition().Z % 1 >= 1 - Zoffset && "FE".Contains(collider[(int)(1 + mc.getPosition().Z + Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)]))
                    || (mc.getPosition().Z % 1 <= Zoffset && "FE".Contains(collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)]))
                    || (mc.getPosition().Z % 1 <= Zoffset && "FE".Contains(collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)]))
                    || (mc.getPosition().Z % 1 < 1 - Zoffset && mc.getPosition().Z % 1 > Zoffset)))
                {
                    mc.Move(commands.Droite, speed);
                    cam.Translate(speed, 0, 0);
                }
                else
                {
                    mc.Pause();
                }
            }
            else
            {
                mc.Pause();
            }
        }
    }
}
