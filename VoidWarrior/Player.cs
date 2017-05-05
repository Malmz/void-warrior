using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VoidWarrior
{
    enum Direction
    {
        TopLeft,
        Top, 
        TopRight,
        Left,
        Center,
        Right,
        BottomLeft,
        Bottom,
        BottomRight,
    }
    class Player
    {
        private const float SPEED = 0.3f;
        private AnimatedSprite sprite;
        private Vector2 move;
        Direction direction;

        public Player(Texture2D texture, float X, float Y, float W, float H, Color color)
        {
            sprite = new AnimatedSprite(texture, new Vector2(X, Y), new Vector2(W, H), color);
            sprite.AutoTile(54, 63);
            move = new Vector2();
            direction = Direction.Center;
        }


        public void Update(GameTime gameTime)
        {
            if (Events.KeyDown(Keys.W))
            {
                move += new Vector2(0, -1);
                direction = Direction.Top;
            }
            if (Events.KeyDown(Keys.S))
            {
                move += new Vector2(0, 1);
                direction = Direction.Bottom;
            }
            if (Events.KeyDown(Keys.A))
            {
                move += new Vector2(-1, 0);
                direction = Direction.Left;
            }
            if (Events.KeyDown(Keys.D))
            {
                move += new Vector2(1, 0);
                direction = Direction.Right;
            }

            if (move.X == -1 && move.Y == -1)
            {
                direction = Direction.TopLeft;
            }
            else if (move.X == 1 && move.Y == -1)
            {
                direction = Direction.TopRight;
            }
            else if (move.X == -1 && move.Y == 1)
            {
                direction = Direction.BottomLeft;
            }
            else if (move.X == 1 && move.Y == 1)
            {
                direction = Direction.BottomRight;
            }

            if (move.X != 0 || move.Y != 0)
            {
                move.Normalize();
            }
            else
            {
                direction = Direction.Center;
            }
            sprite.Position += move * SPEED * gameTime.ElapsedGameTime.Milliseconds;
            move -= move;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (direction)
            {
                case Direction.TopLeft:
                    sprite.Draw("R0C0", spriteBatch);
                    break;
                case Direction.Top:
                    sprite.Draw("R0C1", spriteBatch);
                    break;
                case Direction.TopRight:
                    sprite.Draw("R0C2", spriteBatch);
                    break;
                case Direction.Left:
                    sprite.Draw("R1C0", spriteBatch);
                    break;
                case Direction.Center:
                    sprite.Draw("R1C1", spriteBatch);
                    break;
                case Direction.Right:
                    sprite.Draw("R1C2", spriteBatch);
                    break;
                case Direction.BottomLeft:
                    sprite.Draw("R2C0", spriteBatch);
                    break;
                case Direction.Bottom:
                    sprite.Draw("R2C1", spriteBatch);
                    break;
                case Direction.BottomRight:
                    sprite.Draw("R2C2", spriteBatch);
                    break;
                default:
                    sprite.Draw("R1C1", spriteBatch);
                    break;
            }
        }

        public Vector2 Position
        {
            get
            {
                return sprite.Position;
            }

            set
            {
                sprite.Position = value;
            }
        }

        public Vector2 Size
        {
            get
            {
                return sprite.Size;
            }

            set
            {
                sprite.Size = value;
            }
        }
    }
}
