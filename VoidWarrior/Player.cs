using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private const float SPEED = 300f;
        private AnimatedSprite sprite;
        private Texture2D bulletTexture;
        private Vector2 move;
        private List<Bullet> bullets;
        private int health;
        private Direction direction;
        

        public Player(Texture2D shipTexture, Texture2D bulletTexture, float X, float Y, float W, float H, Color color, int health = 1)
        {
            this.bulletTexture = bulletTexture;
            this.bullets = new List<Bullet>();
            sprite = new AnimatedSprite(shipTexture, X, Y, W, H, color);
            sprite.AutoTile(512, 512);
            move = new Vector2();
            direction = Direction.Center;
            this.health = health;

        }

        private void MoveInside(Rectangle parent)
        {
            if (parent.Contains(sprite.Bounds))
            {
                return;
            }
            else if (sprite.X < parent.X)
            {
                sprite.X = parent.X;
            }
            else if (sprite.X + sprite.Width > parent.Width)
            {
                sprite.X = parent.Width - sprite.Width;
            }
            if (sprite.Y < parent.Y)
            {
                sprite.Y = parent.Y;
            }
            else if (sprite.Y + sprite.Height > parent.Height)
            {
                sprite.Y = parent.Height - sprite.Height;
            }
        }
        public void Update(GameTime gameTime)
        {
            if (Input.KeyPressed(Keys.Space))
            {
                bullets.Add(new Bullet(bulletTexture, sprite.X + sprite.Width / 2 - 2, sprite.Y, 4, 25, Color.Red, 1, 500, 90, x => 0));
            }

            var rangeSwitch = new Dictionary<Func<double, bool>, Action>
            {
                { x => x < 22.5 || x > 337.5,   () => direction = Direction.Left },
                { x => x < 65.5,                () => direction = Direction.TopLeft },
                { x => x < 112.5,               () => direction = Direction.Top },
                { x => x < 157.5,               () => direction = Direction.TopRight },
                { x => x < 202.5,               () => direction = Direction.Right },
                { x => x < 247.5,               () => direction = Direction.BottomRight },
                { x => x < 292.5,               () => direction = Direction.Bottom },
                { x => x < 337.5,               () => direction = Direction.BottomLeft },
            };

            move = Input.Joystick;
            if (move.X == 0 && move.Y == 0)
            {
                direction = Direction.Center;
            }
            else
            {
                rangeSwitch.First(sw => sw.Key(Angle(move))).Value();
            }

            sprite.Position += move * SPEED * gameTime.ElapsedGameTime.Milliseconds / 1000f;
            move -= move;
            MoveInside(Globals.SCREEN);
            bullets = bullets.Where(x => Globals.SCREEN.Contains(x.Bounds)).ToList();
            bullets.ForEach(x => x.Update(gameTime));
        }

        private double Angle(Vector2 vector)
        {
            return Math.Atan(vector.Y / vector.X) * 180 / Math.PI;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bullets.ForEach(x => x.Draw(spriteBatch));
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

        public List<Bullet> Bullets
        {
            get
            {
                return bullets;
            }

            set
            {
                bullets = value;
            }
        }
        public Rectangle Bounds
        {
            get { return sprite.Bounds; }
        }

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }
    }
}
