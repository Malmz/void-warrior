using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using VoidWarrior.Ui.Progress;

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
        private SpriteSheet sprite;
        private SoundEffect shootSound;
        private Vector2 move;
        private int health;
        private Gun gun;
        private Bar ammoBar;
        private Direction direction;
        

        public Player(ResourcePool res, float X, float Y, float W, float H, Color color, int health = 1)
        {
            sprite = new SpriteSheet(res.GetTexture("VoidShipSpriteSheet"), X, Y, W, H, color);
            sprite.AutoTile(512, 512);
            move = new Vector2();
            direction = Direction.Center;
            this.health = health;
            this.shootSound = res.GetSound("Shoot");
            gun = new Gun(
                ammo: 14, fireRate: 0.35f, reloadTime: 2f, 
                template: new Bullet(
                    new Sprite(res.GetTexture("BulletSpriteSheet"), sprite.X + sprite.Width / 2 - 10, sprite.Y, 20, 34, new Rectangle(466, 254, 10, 17), Color.White),
                    1, new Path(500, 90, x => 0)));

            ammoBar = new Bar(res.GetTexture("pixel"), 10, Globals.SCREEN_HEIGHT - 20, 100, 10, gun.MagazineSize, gun.MagazineSize, Color.Yellow);
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
            else if (sprite.X + sprite.Width > parent.X + parent.Width)
            {
                sprite.X = parent.X + parent.Width - sprite.Width;
            }
            if (sprite.Y < parent.Y)
            {
                sprite.Y = parent.Y;
            }
            else if (sprite.Y + sprite.Height > parent.Y + parent.Height)
            {
                sprite.Y = parent.Y + parent.Height - sprite.Height;
            }
        }
        public void Update(GameTime gameTime)
        {
            if (Input.FireHold)
            {
                gun.Position = new Vector2(sprite.X + sprite.Width / 2 - 10, sprite.Y);
                if (gun.Fire())
                {
                    shootSound.Play();
                }
            }

            if (Input.Reload)
            {
                gun.Reload();
            }

            ammoBar.Value = gun.Ammo;

            var rangeSwitch = new Dictionary<Func<double, bool>, Action>
            {
                { x => x < 22.5 || x > 337.5,   () => direction = Direction.Right },
                { x => x < 65.5,                () => direction = Direction.TopRight },
                { x => x < 112.5,               () => direction = Direction.Top },
                { x => x < 157.5,               () => direction = Direction.TopLeft },
                { x => x < 202.5,               () => direction = Direction.Left },
                { x => x < 247.5,               () => direction = Direction.BottomLeft },
                { x => x < 292.5,               () => direction = Direction.Bottom },
                { x => x < 337.5,               () => direction = Direction.BottomRight },
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
            gun.Range(Globals.SCREEN);
            gun.Update(gameTime);
        }

        private double Angle(Vector2 vector)
        {
            return Math.Atan2(vector.Y, -vector.X) * 180 / Math.PI + 180;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            gun.Draw(spriteBatch);
            ammoBar.Draw(spriteBatch);
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
                return gun.Bullets;
            }

            set
            {
                gun.Bullets = value;
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
