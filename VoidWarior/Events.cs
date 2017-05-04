using Microsoft.Xna.Framework.Input;

namespace VoidWarior
{
    class Events
    {
        private static KeyboardState keystateOld = Keyboard.GetState();
        private static KeyboardState keystateNew = Keyboard.GetState();

        public static void Update()
        {
            keystateOld = keystateNew;
            keystateNew = Keyboard.GetState();
        }

        public static bool KeyDown(Keys key)
        {
            return keystateNew.IsKeyDown(key);
        }

        public static bool KeyUp(Keys key)
        {
            return keystateNew.IsKeyUp(key);
        }

        public static bool KeyPressed(Keys key)
        {
            if (keystateNew.IsKeyDown(key) && keystateOld.IsKeyUp(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}