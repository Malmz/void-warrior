using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VoidWarrior
{
    class Bullet
    {
        private Sprite sprite;
        private Vector2 startPos;
        private Path path;
        private int damage;

        public Bullet(Sprite sprite, int damage, Path path)
        {
            startPos = sprite.Position;
            this.sprite = sprite;
            this.sprite.Position = new Vector2();
            this.path = path;
            this.damage = damage;
        }

        /*
        public Bullet(Texture2D texture, float X, float Y, Color color, int damage, float speed, double angle, Func<float, float> func)
        {
            this.startPos = new Vector2(X, Y);
            this.sprite = new Sprite(texture, 0, 0, color);
            this.path = new Path(speed, angle, func);
            this.damage = damage;
        }

        
        public Bullet(Texture2D texture, float X, float Y, float W, float H, Color color, int damage, float speed, double angle, Func<float, float> func)
        {
            this.startPos = new Vector2(X, Y);
            this.sprite = new Sprite(texture, 0, 0, W, H, color);
            this.path = new Path(speed, angle, func);
            this.damage = damage;
        }

        */
        public void Update(GameTime gameTime)
        {
            path.Update(gameTime);
            sprite.Position = path.Position + startPos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }

        public Bullet Clone
        {
            get
            {
                return new Bullet(new Sprite(sprite.Texture, startPos.X, startPos.Y, sprite.Width, sprite.Height, sprite.SourceRectangle, sprite.Color), damage, new Path(path.Speed, path.Angle, path.Func));
            }
        }

        public Rectangle Bounds
        {
            get
            {
                return sprite.Bounds;
            }
        }

        public int Damage
        {
            get
            {
                return damage;
            }

            set
            {
                damage = value;
            }
        }

        public Vector2 StartPos
        {
            get { return startPos; }
            set { startPos = value; }
        }
    }
}
