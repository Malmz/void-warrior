using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace VoidWarrior
{
    public enum MouseButtons
    {
        Left,
        Right,
        Middle,
    }

    class Input
    {
        private static KeyboardState keystateOld = Keyboard.GetState();
        private static KeyboardState keystateNew = Keyboard.GetState();
        private static MouseState mousestateOld = Mouse.GetState();
        private static MouseState mousestateNew = Mouse.GetState();
        private static GamePadState gamepadstateOld = GamePad.GetState(PlayerIndex.One);
        private static GamePadState gamepadstateNew = GamePad.GetState(PlayerIndex.One);
        private static bool gamepadConnected = gamepadstateNew.IsConnected;
        private static Vector2 move = new Vector2();

        public static void Update()
        {
            keystateOld = keystateNew;
            keystateNew = Keyboard.GetState();
            mousestateOld = mousestateNew;
            mousestateNew = Mouse.GetState();
            gamepadstateOld = gamepadstateNew;
            gamepadstateNew = GamePad.GetState(PlayerIndex.One);
            gamepadConnected = gamepadstateNew.IsConnected;
        }

        public static Vector2 Joystick
        {
            get
            {
                move -= move;
                if (gamepadConnected)
                {
                    move = gamepadstateNew.ThumbSticks.Left;
                }
                else
                {
                    if (KeyDown(Keys.W))
                    {
                        move += new Vector2(0, -1);
                    }
                    if (KeyDown(Keys.S))
                    {
                        move += new Vector2(0, 1);
                    }
                    if (KeyDown(Keys.A))
                    {
                        move += new Vector2(-1, 0);
                    }
                    if (KeyDown(Keys.D))
                    {
                        move += new Vector2(1, 0);
                    }
                }
                if (move.X == 0 && move.Y == 0)
                {
                    return move;
                }
                else
                {
                    move.Normalize();
                    return move;
                }
            }
        }

        public static bool Fire
        {
            get
            {
                return ButtonPressed(Buttons.A) || KeyPressed(Keys.Space);
            }
        }

        public static bool ButtonDown(Buttons button)
        {
            return gamepadstateNew.IsButtonDown(button);
        }

        public static bool ButtonUp(Buttons button)
        {
            return gamepadstateNew.IsButtonUp(button);
        }

        public static bool ButtonPressed(Buttons button)
        {
            if (gamepadstateOld.IsButtonUp(button) && gamepadstateNew.IsButtonDown(button))
            {
                return true;
            }
            else
            {
                return false;
            }
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

        public static Point MousePosition
        {
            get
            {
                return mousestateNew.Position;
            }
        }

        public static bool GamepadConnected {
            get { return gamepadConnected; }
            set { gamepadConnected = value; }
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