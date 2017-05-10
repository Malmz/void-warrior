using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VoidWarrior
{
    class Enemy
    {
        private Sprite sprite;
        private Vector2 startPosition;
        private Path path;
        private int health;
        private float delay;
        public Enemy(Texture2D texture, float X, float Y, Color color, int health, float speed, double angle, Func<float, float> func, float delay = 0)
        {
            this.sprite = new Sprite(texture, X, Y, color);
            this.startPosition = new Vector2(X, Y);
            path = new Path(speed, angle, func);
            this.delay = delay;
            this.health = health;
        }

        public Enemy(Texture2D texture, float X, float Y, float W, float H, Color color, int health, float speed, double angle, Func<float, float> func, float delay = 0)
        {
            this.sprite = new Sprite(texture, X, Y, W, H, color);
            this.startPosition = new Vector2(X, Y);
            path = new Path(speed, angle, func);
            this.delay = delay;
            this.health = health;
        }

        public void Update(GameTime gameTime)
        {
            path.Update(gameTime);
            sprite.Position = path.Position + startPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }

        public float X
        {
            get { return path.X + startPosition.X; }
            set { startPosition.X = value - path.X; }
        }

        public float Y
        {
            get { return path.Y + startPosition.Y; }
            set { startPosition.Y = value - path.Y; }
        }

        public float startX
        {
            get { return startPosition.X; }
            set { startPosition.X = value; }
        }

        public float startY
        {
            get { return startPosition.Y; }
            set { startPosition.Y = value; }
        }

        public float Width
        {
            get { return sprite.Width; }
        }

        public float Height
        {
            get { return sprite.Height; }
        }

        public Rectangle Bounds
        {
            get
            {
                return sprite.Bounds;
            }
        }
        public float Delay { get { return delay; } }
    }
}
