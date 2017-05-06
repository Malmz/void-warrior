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
        public Enemy(Texture2D texture, float X, float Y, Color color, float speed, double angle,  Func<float, float> func)
        {
            this.sprite = new Sprite(texture, X, Y, color);
            this.startPosition = new Vector2(X, Y);
            path = new Path(speed, angle, func);
        }

        public Enemy(Texture2D texture, float X, float Y, float W, float H, Color color, float speed, double angle, Func<float, float> func)
        {
            this.sprite = new Sprite(texture, 0, 0, W, H, color);
            this.startPosition = new Vector2(X, Y);
            path = new Path(speed, angle, func);
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

        public Rectangle Bounds
        {
            get
            {
                return sprite.Bounds;
            }
        }
    }
}
