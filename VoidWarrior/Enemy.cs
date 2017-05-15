using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VoidWarrior.Ui.Progress;

namespace VoidWarrior
{
    class Enemy
    {
        private Sprite sprite;
        private Vector2 startPosition;
        private Path path;
        private int health;
        private Bar healthBar;
        private float delay;
        private int value;
        public Enemy(Texture2D texture, Texture2D healthBarTexture, float X, float Y, Color color, int health, int value, float speed, double angle, Func<float, float> func, float delay = 0)
        {
            sprite = new Sprite(texture, X, Y, color);
            startPosition = new Vector2(X, Y);
            path = new Path(speed, angle, func);
            this.delay = delay;
            this.health = health;
            this.value = value;
            healthBar = new Bar(healthBarTexture, X + sprite.Width / 2 - 80 / 2, Y + sprite.Height + 10, 80, 6, health, health, Color.Red);
        }

        public Enemy(Texture2D texture, Texture2D healthBarTexture, float X, float Y, float W, float H, Color color, int health, int value, float speed, double angle, Func<float, float> func, float delay = 0)
        {
            sprite = new Sprite(texture, X, Y, W, H, color);
            startPosition = new Vector2(X, Y);
            path = new Path(speed, angle, func);
            this.delay = delay;
            this.health = health;
            this.value = value;
            healthBar = new Bar(healthBarTexture, X + sprite.Width / 2 - 80 / 2, Y + sprite.Height + 10, 80, 6, health, health, Color.Red);

        }

        public Enemy(Sprite sprite, Texture2D healthBarTexture, int health, int value, Path path, float delay = 0)
        {
            this.sprite = sprite;
            startPosition = sprite.Position;
            this.path = path;
            this.delay = delay;
            this.health = health;
            this.value = value;
            healthBar = new Bar(healthBarTexture, X + sprite.Width / 2 - 80 / 2, Y + sprite.Height + 10, 80, 6, health, health, Color.Red);
        }

        public void Update(GameTime gameTime)
        {
            path.Update(gameTime);
            sprite.Position = path.Position + startPosition;
            healthBar.Value = health;
            healthBar.X = X + sprite.Width / 2 - 80 / 2;
            healthBar.Y = Y + sprite.Height + 10;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
            healthBar.Draw(spriteBatch);
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

        public float StartX
        {
            get { return startPosition.X; }
            set { startPosition.X = value; }
        }

        public float StartY
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

        public int Health {
            get { return health; }
            set { health = value; }
        }

        public int Value
        {
            get { return value; }
        }
    }
}
