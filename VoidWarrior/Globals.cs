using Microsoft.Xna.Framework;

namespace VoidWarrior
{
    class Globals
    {
        public const int SCREEN_WIDTH = 1280;
        public const int SCREEN_HEIGHT = 720;
        private static Rectangle screen = new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT);

        public static Rectangle SCREEN
        {
            get
            {
                return screen;
            }
        }
    }
}
