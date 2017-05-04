using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace VoidWarrior
{
    enum MouseButtons
    {
        Left,
        Right,
        Middle,
    }

    class Events
    {
        private static KeyboardState keystateOld = Keyboard.GetState();
        private static KeyboardState keystateNew = Keyboard.GetState();
        private static MouseState mousestateOld = Mouse.GetState();
        private static MouseState mousestateNew = Mouse.GetState();

        public static void Update()
        {
            keystateOld = keystateNew;
            keystateNew = Keyboard.GetState();
            mousestateOld = mousestateNew;
            mousestateNew = Mouse.GetState();
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

        public static Point MousePosition()
        {
            return mousestateNew.Position;
        }

        public static bool MouseDown(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    if (mousestateNew.LeftButton == ButtonState.Pressed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case MouseButtons.Middle:
                    if (mousestateNew.MiddleButton == ButtonState.Pressed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case MouseButtons.Right:
                    if (mousestateNew.RightButton == ButtonState.Pressed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }

        public static bool MouseUp(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    if (mousestateNew.LeftButton == ButtonState.Released)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case MouseButtons.Middle:
                    if (mousestateNew.MiddleButton == ButtonState.Released)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case MouseButtons.Right:
                    if (mousestateNew.RightButton == ButtonState.Released)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }

        public static bool MouseReleased(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    if (mousestateNew.LeftButton == ButtonState.Released && mousestateOld.LeftButton == ButtonState.Pressed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case MouseButtons.Middle:
                    if (mousestateNew.MiddleButton == ButtonState.Released && mousestateOld.MiddleButton == ButtonState.Pressed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case MouseButtons.Right:
                    if (mousestateNew.RightButton == ButtonState.Released && mousestateOld.RightButton == ButtonState.Pressed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
    }
}