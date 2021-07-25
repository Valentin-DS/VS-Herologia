using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Test3D.Constants;

namespace Test3D
{
    public class Movement
    {
        public static float Xoffset = 0.2f;
        public static float Zoffset = 0.2f;

        public static void UpdateOld(MainCharacter mc, Camera cam, Level level)
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
                if (AUTHORIZED_CHARS.Contains(collider[(int)(1 + mc.getPosition().Z - Zoffset - speed)][(int)(1 + mc.getPosition().X)])
                    && ((mc.getPosition().X % 1 >= 1 - Xoffset && AUTHORIZED_CHARS.Contains(collider[(int)(1 + mc.getPosition().Z - Zoffset - speed)][(int)(1 + mc.getPosition().X + Xoffset)]))
                    || (mc.getPosition().X % 1 <= Xoffset && AUTHORIZED_CHARS.Contains(collider[(int)(1 + mc.getPosition().Z - Zoffset - speed)][(int)(1 + mc.getPosition().X - Xoffset)]))
                    || (mc.getPosition().X % 1 < 1 - Xoffset && mc.getPosition().X % 1 > Xoffset)))
                {
                    mc.Move(Keys.Z, speed);
                    cam.Translate(0, 0, -speed);
                    CheckAltitude(collider, mc, cam, speed);
                    CheckClimbInputs(collider, cam, speed, Keys.Z, mc);
                    //if (collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X)] == 'V')
                    //{
                    //    mc.Climb(Keys.Z, speed);
                    //    cam.Translate(0, speed / 2f, 0);
                    //}
                }
                else
                {
                    mc.Pause();
                }
            }
            else if (ManageKeys.IsPressed(Keys.S) && ManageKeys.IsUp(Keys.Z) && ManageKeys.IsUp(Keys.Q) && ManageKeys.IsUp(Keys.D))
            {
                mc.InitializeMove(Keys.S);
                float speed = mc.getSpeed();
                if (AUTHORIZED_CHARS.Contains(collider[(int)(1 + mc.getPosition().Z + Zoffset + speed)][(int)(1 + mc.getPosition().X)])
                    && ((mc.getPosition().X % 1 >= 1 - Xoffset && AUTHORIZED_CHARS.Contains(collider[(int)(1 + mc.getPosition().Z + Zoffset + speed)][(int)(1 + mc.getPosition().X + Xoffset)]))
                    || (mc.getPosition().X % 1 <= Xoffset && AUTHORIZED_CHARS.Contains(collider[(int)(1 + mc.getPosition().Z + Zoffset + speed)][(int)(1 + mc.getPosition().X - Xoffset)]))
                    || (mc.getPosition().X % 1 < 1 - Xoffset && mc.getPosition().X % 1 > Xoffset)))
                {
                    mc.Move(Keys.S, speed);
                    cam.Translate(0, 0, +speed);
                    CheckAltitude(collider, mc, cam, speed);
                    CheckClimbInputs(collider, cam, speed, Keys.S, mc);
                    //if (collider[(int)(1 + mc.getPosition().Z - speed)][(int)(1 + mc.getPosition().X)] == 'f')
                    //{
                    //    mc.Climb(Keys.S, speed);
                    //    cam.Translate(0, -speed / 2f, 0);
                    //}
                }
                else
                {
                    mc.Pause();
                }
            }
            else if (ManageKeys.IsPressed(Keys.Q) && ManageKeys.IsUp(Keys.S) && ManageKeys.IsUp(Keys.Z) && ManageKeys.IsUp(Keys.D))
            {
                mc.InitializeMove(Keys.Q);
                float speed = mc.getSpeed();
                if (AUTHORIZED_CHARS.Contains(collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X - Xoffset - speed)])
                    && ((mc.getPosition().Z % 1 >= 1 - Zoffset && AUTHORIZED_CHARS.Contains(collider[(int)(1 + mc.getPosition().Z + Zoffset)][(int)(1 + mc.getPosition().X - Xoffset - speed)]))
                    || (mc.getPosition().Z % 1 <= Zoffset && AUTHORIZED_CHARS.Contains(collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X - Xoffset - speed)]))
                    || (mc.getPosition().Z % 1 < 1 - Zoffset && mc.getPosition().Z % 1 > Zoffset)))
                {
                    mc.Move(Keys.Q, speed);
                    cam.Translate(-speed, 0, 0);
                    CheckAltitude(collider, mc, cam, speed);
                    CheckClimbInputs(collider, cam, speed, Keys.Q, mc);
                }
                else
                {
                    mc.Pause();
                }
            }
            else if (ManageKeys.IsPressed(Keys.D) && ManageKeys.IsUp(Keys.S) && ManageKeys.IsUp(Keys.Q) && ManageKeys.IsUp(Keys.Z))
            {
                mc.InitializeMove(Keys.D);
                float speed = mc.getSpeed();
                if (AUTHORIZED_CHARS.Contains(collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                    && ((mc.getPosition().Z % 1 >= 1 - Zoffset && AUTHORIZED_CHARS.Contains(collider[(int)(1 + mc.getPosition().Z + Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)]))
                    || (mc.getPosition().Z % 1 <= Zoffset && AUTHORIZED_CHARS.Contains(collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)]))
                    || (mc.getPosition().Z % 1 <= Zoffset && AUTHORIZED_CHARS.Contains(collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)]))
                    || (mc.getPosition().Z % 1 < 1 - Zoffset && mc.getPosition().Z % 1 > Zoffset)))
                {
                    mc.Move(Keys.D, speed);
                    cam.Translate(speed, 0, 0);
                    CheckAltitude(collider, mc, cam, speed);
                    CheckClimbInputs(collider, cam, speed, Keys.D, mc);
                }
                else
                {
                    mc.Pause();
                }
            }
        }

        public static void Update(MainCharacter mc, Camera cam, Level level)
        {
            List<List<char>> collider = level.getCollider();
            Vector2 pos = mc.getPositionOnGrid();
            if (ManageKeys.IsJustUp(Keys.Z) || ManageKeys.IsJustUp(Keys.S) || ManageKeys.IsJustUp(Keys.Q) || ManageKeys.IsJustUp(Keys.D))
            {
                //mc.Pause();
            }

            //region pressed keys list
            List<Keys> listPressed = new List<Keys>();
            
            if (ManageKeys.IsPressed(Keys.D) && ManageKeys.IsUp(Keys.Q) && (ManageKeys.IsUp(Keys.S) || ManageKeys.IsUp(Keys.Z)) && listPressed.Count < 2)
            {
                listPressed.Add(Keys.D);
            }
            else if (ManageKeys.IsPressed(Keys.Q) && (ManageKeys.IsUp(Keys.D)) && (ManageKeys.IsUp(Keys.S) || ManageKeys.IsUp(Keys.Z)) && listPressed.Count < 2)
            {
                listPressed.Add(Keys.Q);
            }
            if (ManageKeys.IsPressed(Keys.Z) && !ManageKeys.IsJustUp(Keys.Z) && ManageKeys.IsUp(Keys.S) && (ManageKeys.IsUp(Keys.Q) || ManageKeys.IsUp(Keys.D)) && listPressed.Count < 2)
            {
                listPressed.Add(Keys.Z);
            }
            else if (ManageKeys.IsPressed(Keys.S) && !ManageKeys.IsJustUp(Keys.S) && (ManageKeys.IsUp(Keys.Z)) && (ManageKeys.IsUp(Keys.D) || ManageKeys.IsUp(Keys.Q)) && listPressed.Count < 2)
            {
                listPressed.Add(Keys.S);
            }
            Console.WriteLine(listPressed);
            /*
            if (ManageKeys.IsPressed(Keys.D) && ManageKeys.IsPressed(Keys.Z) && ManageKeys.IsUp(Keys.S) && ManageKeys.IsUp(Keys.Q))
            {
                listPressed.Add(Keys.D);
                listPressed.Add(Keys.Z);
            }
            else if (ManageKeys.IsPressed(Keys.D) && ManageKeys.IsPressed(Keys.S) && ManageKeys.IsUp(Keys.Z) && ManageKeys.IsUp(Keys.Q))
            {
                listPressed.Add(Keys.D);
                listPressed.Add(Keys.S);
            }
            else if (ManageKeys.IsPressed(Keys.Q) && ManageKeys.IsPressed(Keys.Z) && ManageKeys.IsUp(Keys.S) && ManageKeys.IsUp(Keys.D))
            {
                listPressed.Add(Keys.Q);
                listPressed.Add(Keys.Z);
            }
            else if (ManageKeys.IsPressed(Keys.Q) && ManageKeys.IsPressed(Keys.S) && ManageKeys.IsUp(Keys.Z) && ManageKeys.IsUp(Keys.D))
            {
                listPressed.Add(Keys.Q);
                listPressed.Add(Keys.S);
            }

            else if (ManageKeys.IsPressed(Keys.D) && ManageKeys.IsUp(Keys.Z) && ManageKeys.IsUp(Keys.S) && ManageKeys.IsUp(Keys.Q))
            {
                listPressed.Add(Keys.D);
            }
            else if (ManageKeys.IsPressed(Keys.S) && ManageKeys.IsUp(Keys.D) && ManageKeys.IsUp(Keys.Z) && ManageKeys.IsUp(Keys.Q))
            {
                listPressed.Add(Keys.S);
            }
            else if (ManageKeys.IsPressed(Keys.Q) && ManageKeys.IsUp(Keys.Z) && ManageKeys.IsUp(Keys.S) && ManageKeys.IsUp(Keys.D))
            {
                listPressed.Add(Keys.Q);
            }
            else if (ManageKeys.IsPressed(Keys.Z) && ManageKeys.IsUp(Keys.Q) && ManageKeys.IsUp(Keys.S) && ManageKeys.IsUp(Keys.D))
            {
                listPressed.Add(Keys.Z);
            }
            */


            //mc.setMoveModeShift();

            int nbPressed = listPressed.Count;
            if (nbPressed > 0)
            {
                
                mc.InitializeMove(listPressed[nbPressed-1]);

                float speed = (float) mc.getSpeed() / (listPressed.Count >=2 ? (float) Math.Sqrt(2) : 1);
                    //float speed = (float)Math.Sqrt(mc.getSpeed());
                    //testColiderLeft(collider, mc, speed)
                    List<float> l = new List<float> { listPressed.Contains(Keys.Q) && testColiderLeft(collider, mc, speed) 
                        ? -speed 
                        : (listPressed.Contains(Keys.D) && testColiderRight(collider, mc, speed) 
                        ? speed : 0),
                        0,
                        listPressed.Contains(Keys.Z) && testColiderTop(collider, mc, speed)  
                        ? -speed 
                        : (listPressed.Contains(Keys.S) && testColiderBottom(collider, mc, speed)
                        ? speed 
                        : 0) };
                    //endregion

                    mc.Move2(listPressed, l);
                    cam.Translate(l[0], l[1], l[2]);

                    CheckAltitude(collider, mc, cam, speed);
                    foreach (var key in listPressed)
                    {
                        CheckClimbInputs(collider, cam, speed, key, mc);
                    }
            }
            else
            {
                mc.Pause();
            }
        }

        public static bool testColider(Keys keyPressed, List<List<char>> collider, MainCharacter mc, float speed)
        {
            //ZSQD
            float posZ = 1 + mc.getPosition().Z;
            float posX = 1 + mc.getPosition().X;
            float mcX1 = mc.getPosition().X % 1;
            float mcZ1 = mc.getPosition().Z % 1;

            if (keyPressed == Keys.Z)
            {
                float zSpeed = posZ - Zoffset - speed;
                return AUTHORIZED_CHARS.Contains(collider[(int)(zSpeed)][(int)(posX)])
            && ((mcX1 >= 1 - Xoffset && AUTHORIZED_CHARS.Contains(collider[(int)(zSpeed)][(int)(posX + Xoffset)]))
            || (mcX1 <= Xoffset && AUTHORIZED_CHARS.Contains(collider[(int)(zSpeed)][(int)(posX - Xoffset)]))
            || (mcX1 < 1 - Xoffset && mcX1 > Xoffset));
            }
            else if (keyPressed == Keys.S)
            {
                float sSpeed = posZ + Zoffset + speed;
                return AUTHORIZED_CHARS.Contains(collider[(int)(sSpeed)][(int)(posX)])
            && ((mcX1 >= 1 - Xoffset && AUTHORIZED_CHARS.Contains(collider[(int)(sSpeed)][(int)(posX + Xoffset)]))
            || (mcX1 <= Xoffset && AUTHORIZED_CHARS.Contains(collider[(int)(sSpeed)][(int)(posX - Xoffset)]))
            || (mcX1 < 1 - Xoffset && mcX1 > Xoffset));
            }
            else if (keyPressed == Keys.Q)
            {
                float gSpeed = posX - Xoffset - speed;
                return AUTHORIZED_CHARS.Contains(collider[(int)(posZ)][(int)(gSpeed)])
            && ((mcZ1 >= 1 - Zoffset && AUTHORIZED_CHARS.Contains(collider[(int)(posZ + Zoffset)][(int)(gSpeed)]))
            || (mcZ1 <= Zoffset && AUTHORIZED_CHARS.Contains(collider[(int)(posZ - Zoffset)][(int)(gSpeed)]))
            || (mcZ1 < 1 - Zoffset && mcZ1 > Zoffset));
            }
            else if (keyPressed == Keys.D)
            {
                float dSpeed = posX + Xoffset + speed;
                return AUTHORIZED_CHARS.Contains(collider[(int)(posZ)][(int)(dSpeed)])
            && ((mcZ1 >= 1 - Zoffset && AUTHORIZED_CHARS.Contains(collider[(int)(posZ + Zoffset)][(int)(dSpeed)]))
            || (mcZ1 <= Zoffset && AUTHORIZED_CHARS.Contains(collider[(int)(posZ - Zoffset)][(int)(dSpeed)]))
            || (mcZ1 <= Zoffset && AUTHORIZED_CHARS.Contains(collider[(int)(posZ - Zoffset)][(int)(dSpeed)]))
            || (mcZ1 < 1 - Zoffset && mcZ1 > Zoffset));
            }
            return false;

        }

        public static bool testColiderTop(List<List<char>> collider, MainCharacter mc, float speed)
        {
            //ZSQD
            float posZ = 1 + mc.getPosition().Z;
            float posX = 1 + mc.getPosition().X;
            float mcX1 = mc.getPosition().X % 1;
            
            float zSpeed = posZ - Zoffset - speed;
            return AUTHORIZED_CHARS.Contains(collider[(int)(zSpeed)][(int)(posX)])
            && ((mcX1 >= 1 - Xoffset && AUTHORIZED_CHARS.Contains(collider[(int)(zSpeed)][(int)(posX + Xoffset)]))
            || (mcX1 <= Xoffset && AUTHORIZED_CHARS.Contains(collider[(int)(zSpeed)][(int)(posX - Xoffset)]))
            || (mcX1 < 1 - Xoffset && mcX1 > Xoffset));
        }


        public static bool testColiderBottom(List<List<char>> collider, MainCharacter mc, float speed)
        {
            //ZSQD
            float posZ = 1 + mc.getPosition().Z;
            float posX = 1 + mc.getPosition().X;
            float mcX1 = mc.getPosition().X % 1;

            float sSpeed = posZ + Zoffset + speed;
            return AUTHORIZED_CHARS.Contains(collider[(int)(sSpeed)][(int)(posX)])
            && ((mcX1 >= 1 - Xoffset && AUTHORIZED_CHARS.Contains(collider[(int)(sSpeed)][(int)(posX + Xoffset)]))
            || (mcX1 <= Xoffset && AUTHORIZED_CHARS.Contains(collider[(int)(sSpeed)][(int)(posX - Xoffset)]))
            || (mcX1 < 1 - Xoffset && mcX1 > Xoffset));
        }

        public static bool testColiderLeft(List<List<char>> collider, MainCharacter mc, float speed)
        {
            float posZ = 1 + mc.getPosition().Z;
            float posX = 1 + mc.getPosition().X;
            float mcZ1 = mc.getPosition().Z % 1;
            
            float gSpeed = posX - Xoffset - speed;
            return AUTHORIZED_CHARS.Contains(collider[(int)(posZ)][(int)(gSpeed)])
            && ((mcZ1 >= 1 - Zoffset && AUTHORIZED_CHARS.Contains(collider[(int)(posZ + Zoffset)][(int)(gSpeed)]))
            || (mcZ1 <= Zoffset && AUTHORIZED_CHARS.Contains(collider[(int)(posZ - Zoffset)][(int)(gSpeed)]))
            || (mcZ1 < 1 - Zoffset && mcZ1 > Zoffset));
        }



        public static bool testColiderRight(List<List<char>> collider, MainCharacter mc, float speed)
        {
            float posZ = 1 + mc.getPosition().Z;
            float posX = 1 + mc.getPosition().X;
            float mcZ1 = mc.getPosition().Z % 1;
            
            float dSpeed = posX + Xoffset + speed;
            return AUTHORIZED_CHARS.Contains(collider[(int)(posZ)][(int)(dSpeed)])
            && ((mcZ1 >= 1 - Zoffset && AUTHORIZED_CHARS.Contains(collider[(int)(posZ + Zoffset)][(int)(dSpeed)]))
            || (mcZ1 <= Zoffset && AUTHORIZED_CHARS.Contains(collider[(int)(posZ - Zoffset)][(int)(dSpeed)]))
            || (mcZ1 <= Zoffset && AUTHORIZED_CHARS.Contains(collider[(int)(posZ - Zoffset)][(int)(dSpeed)]))
            || (mcZ1 < 1 - Zoffset && mcZ1 > Zoffset));
        }




        public static bool IsCurrentChar(char c, List<List<char>> collider, MainCharacter mc, float speed)
        {
            return c == collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X + Xoffset + speed)] && ((mc.getPosition().Z % 1 >= 1 - Zoffset && c == collider[(int)(1 + mc.getPosition().Z + Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                    || (mc.getPosition().Z % 1 <= Zoffset && c == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                    || (mc.getPosition().Z % 1 <= Zoffset && c == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                    || (mc.getPosition().Z % 1 < 1 - Zoffset && mc.getPosition().Z % 1 > Zoffset));
        }

        public static void CheckAltitude(List<List<char>> collider, MainCharacter mc, Camera camera, float speed)
        {
            if (IsCurrentChar(FLAT_IDENTIFIERS["FIRST_FLOOR"], collider, mc, speed))
            {
                if (mc.getPosition().Y != FLAT_VALUES["FIRST_FLOOR"])
                {
                    mc.setPosition(new Vector3(mc.getPosition().X, FLAT_VALUES["FIRST_FLOOR"], mc.getPosition().Z), camera);
                }
            }

            else if (IsCurrentChar(FLAT_IDENTIFIERS["SECOND_FLOOR"], collider, mc, speed))
            {
                if (mc.getPosition().Y != FLAT_VALUES["SECOND_FLOOR"])
                {
                    mc.setPosition(new Vector3(mc.getPosition().X, FLAT_VALUES["SECOND_FLOOR"], mc.getPosition().Z), camera);
                }
            }

            else if (IsCurrentChar(FLAT_IDENTIFIERS["THIRD_FLOOR"], collider, mc, speed))
            {
                if (mc.getPosition().Y != FLAT_VALUES["THIRD_FLOOR"])
                {
                    mc.setPosition(new Vector3(mc.getPosition().X, FLAT_VALUES["THIRD_FLOOR"], mc.getPosition().Z), camera);
                }
            }

            else if (IsCurrentChar(FLAT_IDENTIFIERS["FOURTH_FLOOR"], collider, mc, speed))
            {
                if (mc.getPosition().Y != FLAT_VALUES["FOURTH_FLOOR"])
                {
                    mc.setPosition(new Vector3(mc.getPosition().X, FLAT_VALUES["FOURTH_FLOOR"], mc.getPosition().Z), camera);
                }
            }

            else if (IsCurrentChar(FLAT_IDENTIFIERS["FIFTH_FLOOR"], collider, mc, speed))
            {
                if (mc.getPosition().Y != FLAT_VALUES["FIFTH_FLOOR"])
                {
                    mc.setPosition(new Vector3(mc.getPosition().X, FLAT_VALUES["FIFTH_FLOOR"], mc.getPosition().Z), camera);
                }
            }

            else if (IsCurrentChar(FLAT_IDENTIFIERS["SIXTH_FLOOR"], collider, mc, speed))
            {
                if (mc.getPosition().Y != FLAT_VALUES["SIXTH_FLOOR"])
                {
                    mc.setPosition(new Vector3(mc.getPosition().X, FLAT_VALUES["SIXTH_FLOOR"], mc.getPosition().Z), camera);
                }
            }

            else if (IsCurrentChar(FLAT_IDENTIFIERS["SEVENTH_FLOOR"], collider, mc, speed))
            {
                if (mc.getPosition().Y != FLAT_VALUES["SEVENTH_FLOOR"])
                {
                    mc.setPosition(new Vector3(mc.getPosition().X, FLAT_VALUES["SEVENTH_FLOOR"], mc.getPosition().Z), camera);
                }
            }
        }


        public static void CheckClimbInputs(List<List<char>> collider, Camera cam, float speed, Keys key, MainCharacter mc)
        {
            if (CLIMB_IDENTIFIERS["Vertical-20°"] == collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X + Xoffset + speed)]
                && ((mc.getPosition().Z % 1 >= 1 - Zoffset && CLIMB_IDENTIFIERS["Vertical-20°"] == collider[(int)(1 + mc.getPosition().Z + Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                  || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Vertical-20°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Vertical-20°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 < 1 - Zoffset && mc.getPosition().Z % 1 > Zoffset)))
            {
                mc.Climb(key, ElevatingDirection.Vertical, 20f);
            }
            else if (CLIMB_IDENTIFIERS["Vertical-27.5°"] == collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X + Xoffset + speed)]
                && ((mc.getPosition().Z % 1 >= 1 - Zoffset && CLIMB_IDENTIFIERS["Vertical-27.5°"] == collider[(int)(1 + mc.getPosition().Z + Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                  || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Vertical-27.5°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Vertical-27.5°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 < 1 - Zoffset && mc.getPosition().Z % 1 > Zoffset)))
            {
                mc.Climb(key, ElevatingDirection.Vertical, 25f);
            }
            else if (CLIMB_IDENTIFIERS["Vertical-40°"] == collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X + Xoffset + speed)]
                && ((mc.getPosition().Z % 1 >= 1 - Zoffset && CLIMB_IDENTIFIERS["Vertical-40°"] == collider[(int)(1 + mc.getPosition().Z + Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                  || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Vertical-40°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Vertical-40°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 < 1 - Zoffset && mc.getPosition().Z % 1 > Zoffset)))
            {
                mc.Climb(key, ElevatingDirection.Vertical, 40f);
            }
            else if (CLIMB_IDENTIFIERS["Horizontal-Right-20°"] == collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X + Xoffset + speed)]
                && ((mc.getPosition().Z % 1 >= 1 - Zoffset && CLIMB_IDENTIFIERS["Horizontal-Right-20°"] == collider[(int)(1 + mc.getPosition().Z + Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                  || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Horizontal-Right-20°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Horizontal-Right-20°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 < 1 - Zoffset && mc.getPosition().Z % 1 > Zoffset)))
            {
                mc.Climb(key, ElevatingDirection.Horizontal, 20f, HorizontalElevationConfig.RightInclination);
            }
            else if (CLIMB_IDENTIFIERS["Horizontal-Right-30°"] == collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X + Xoffset + speed)]
                && ((mc.getPosition().Z % 1 >= 1 - Zoffset && CLIMB_IDENTIFIERS["Horizontal-Right-30°"] == collider[(int)(1 + mc.getPosition().Z + Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                  || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Horizontal-Right-30°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Horizontal-Right-30°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 < 1 - Zoffset && mc.getPosition().Z % 1 > Zoffset)))
            {
                mc.Climb(key, ElevatingDirection.Horizontal, 30f, HorizontalElevationConfig.RightInclination);
            }
            else if (CLIMB_IDENTIFIERS["Horizontal-Right-40°"] == collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X + Xoffset + speed)]
                && ((mc.getPosition().Z % 1 >= 1 - Zoffset && CLIMB_IDENTIFIERS["Horizontal-Right-40°"] == collider[(int)(1 + mc.getPosition().Z + Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                  || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Horizontal-Right-40°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Horizontal-Right-40°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 < 1 - Zoffset && mc.getPosition().Z % 1 > Zoffset)))
            {
                mc.Climb(key, ElevatingDirection.Horizontal, 40f, HorizontalElevationConfig.RightInclination);
            }
            else if (CLIMB_IDENTIFIERS["Horizontal-Left-20°"] == collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X + Xoffset + speed)]
                && ((mc.getPosition().Z % 1 >= 1 - Zoffset && CLIMB_IDENTIFIERS["Horizontal-Left-20°"] == collider[(int)(1 + mc.getPosition().Z + Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                  || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Horizontal-Left-20°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Horizontal-Left-20°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 < 1 - Zoffset && mc.getPosition().Z % 1 > Zoffset)))
            {
                mc.Climb(key, ElevatingDirection.Horizontal, 20f, HorizontalElevationConfig.LeftInclination);
            }
            else if (CLIMB_IDENTIFIERS["Horizontal-Left-30°"] == collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X + Xoffset + speed)]
                && ((mc.getPosition().Z % 1 >= 1 - Zoffset && CLIMB_IDENTIFIERS["Horizontal-Left-30°"] == collider[(int)(1 + mc.getPosition().Z + Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                  || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Horizontal-Left-30°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Horizontal-Left-30°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 < 1 - Zoffset && mc.getPosition().Z % 1 > Zoffset)))
            {
                mc.Climb(key, ElevatingDirection.Horizontal, 30f, HorizontalElevationConfig.LeftInclination);
            }
            else if (CLIMB_IDENTIFIERS["Horizontal-Left-40°"] == collider[(int)(1 + mc.getPosition().Z)][(int)(1 + mc.getPosition().X + Xoffset + speed)]
                && ((mc.getPosition().Z % 1 >= 1 - Zoffset && CLIMB_IDENTIFIERS["Horizontal-Left-40°"] == collider[(int)(1 + mc.getPosition().Z + Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                  || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Horizontal-Left-40°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 <= Zoffset && CLIMB_IDENTIFIERS["Horizontal-Left-40°"] == collider[(int)(1 + mc.getPosition().Z - Zoffset)][(int)(1 + mc.getPosition().X + Xoffset + speed)])
                || (mc.getPosition().Z % 1 < 1 - Zoffset && mc.getPosition().Z % 1 > Zoffset)))
            {
                mc.Climb(key, ElevatingDirection.Horizontal, 40f, HorizontalElevationConfig.LeftInclination);
            }
        }
    }
}
