using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Test3D
{
    /*
     * Une classe permettant de gérer les entrées clavier
     * 
     * A MODIFIER: rien
     * A OPTIMISER: rien
     */
    public class ManageKeys
    {
        public static KeyboardState KeyboardInput;
        public static KeyboardState PreviousKeyboardInput;

        //met à jour l'état des touches
        public static void Update()
        {
            PreviousKeyboardInput = KeyboardInput;
            KeyboardInput = Keyboard.GetState();
        }

        //initialise les données relatives auc touches
        public static void initialize()
        {
            KeyboardInput = Keyboard.GetState();
            PreviousKeyboardInput = Keyboard.GetState();
        }

        //retourne si la touche concernée vient d'être pressée
        public static bool IsJustPressed(Keys key)
        {
            return PreviousKeyboardInput.IsKeyUp(key) && KeyboardInput.IsKeyDown(key);
        }

        //retourne si la touche concernée vient d'être levée
        public static bool IsJustUp(Keys key)
        {
            return PreviousKeyboardInput.IsKeyDown(key) && KeyboardInput.IsKeyUp(key);
        }

        //retourne si la touche est enfoncée
        public static bool IsPressed(Keys key)
        {
            return KeyboardInput.IsKeyDown(key);
        }

        //retourne si la touche est levée
        public static bool IsUp(Keys key)
        {
            return KeyboardInput.IsKeyUp(key);
        }
    }
}
