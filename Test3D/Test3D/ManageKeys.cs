using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Test3D
{
    /**
     * <summary>Une classe permettant de gérer les entrées clavier</summary>
     * 
     * A MODIFIER: rien
     * A OPTIMISER: rien
     */
    public class ManageKeys
    {
        /**
        * <summary>Le KeyboardState actuel</summary>
        */
        public static KeyboardState KeyboardInput;
        /**
        * <summary>Le précédent KeyboardState</summary>
        */
        public static KeyboardState PreviousKeyboardInput;

        /**
        * <summary>Met à jour l'état des touches</summary>
        */
        public static void Update()
        {
            PreviousKeyboardInput = KeyboardInput;
            KeyboardInput = Keyboard.GetState();
        }

        /**
        * <summary>Initialise les données relatives aux touches</summary>
        */
        public static void initialize()
        {
            KeyboardInput = Keyboard.GetState();
            PreviousKeyboardInput = Keyboard.GetState();
        }


        /**
        * <summary>Permet de savoir si les clés devant être pressées le sont bien, 
        * et si les clés devant être relaché le sont bien.
        * On utilise Deux liste de key, la premiere pour les clés qui doivent être pressées <paramref name="keysPressed"/>,
        * la seconde pour celle devant être relachées <paramref name="keysNotPressed"/></summary>
        * <param name="keysPressed">Une liste de touches du clavier devant être pressées</param>
        * <param name="keysNotPressed">Une liste de touches du clavier ne devant pas être pressé</param>
        * <return> bool</return>
        */
        public static bool checkPressed(List<Keys> keysPressed, List<Keys> keysNotPressed)
        {
            bool arePressed = true;
            // check si les keys qui doivent être pressed le sont bien, sinon on return false
            foreach (Keys key in keysPressed)
            {
                arePressed = IsPressed(key);
                if (!arePressed)
                {
                    return arePressed;
                }
            }
            //si on en sort, c'est que arePressed = true

            bool areNotPressed = true;
            // si les keys qui ne doivent pas être pressed sont pressed, on return false
            foreach (Keys key in keysNotPressed)
            {
                areNotPressed = IsUp(key);
                if (!areNotPressed)
                {
                    return areNotPressed;
                }
            }
            return true;
        }

        /**
        * <summary>Permet de savoir si la clés devant être pressées l'est bien, 
        * et si les clés devant être relaché le sont bien.
        * On utilise 1 key et 1 liste de key, la key est celle devant être pressé <paramref name="keyPressed"/>,
        * la liste dekey correspond au keys devant être relachées <paramref name="keysNotPressed"/></summary>
        * <param name="keyPressed">Une touche du clavier devant être pressé</param>
        * <param name="keysNotPressed">Une liste de touches du clavier ne devant pas être pressé</param>
        * <return> bool</return>
        */
        public static bool checkPressed(Keys keyPressed, List<Keys> keysNotPressed)
        {
            bool arePressed = IsPressed(keyPressed);
            if (!arePressed)
            {
                return arePressed;
            }

            bool areNotPressed = true;
            // si les keys qui ne doivent pas être pressed sont pressed, on return false
            foreach (Keys key in keysNotPressed)
            {
                areNotPressed = IsUp(key);
                if (!areNotPressed)
                {
                    return areNotPressed;
                }
            }
            return true;
        }

        /**
        * <summary>Retourne si la touche concernée vient d'être pressée.
        * On vérifie donc qu'elle était d'abord Up, et qu'elle est maintenant Down.</summary>
        * <param>key : une touche du clavier (keyboard)</param>
        * <return> bool</return>
        */
        public static bool IsJustPressed(Keys key)
        {
            return PreviousKeyboardInput.IsKeyUp(key) && KeyboardInput.IsKeyDown(key);
        }

        /**
        * <summary>Retourne si la touche concernée vient d'être levée.
        * On vérifie donc qu'elle était d'abord Down, et qu'elle est maintenant Up.</summary>
        * <param>key : une touche du clavier (keyboard)</param>
        * <return> bool</return>
        */
        public static bool IsJustUp(Keys key)
        {
            return PreviousKeyboardInput.IsKeyDown(key) && KeyboardInput.IsKeyUp(key);
        }

        /**
        * <summary>Retourne si la touche est enfoncée</summary>
        * <param>key : une touche du clavier (keyboard)</param>
        * <return> bool</return>
        */
        public static bool IsPressed(Keys key)
        {
            return KeyboardInput.IsKeyDown(key);
        }

        /**
        * <summary>Retourne si la touche est levée</summary>
        * <param>key : une touche du clavier (keyboard)</param>
        * <return> bool</return>
        */
        public static bool IsUp(Keys key)
        {
            return KeyboardInput.IsKeyUp(key);
        }
    }
}
