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

        public Bullet(Texture2D texture, float X, float Y, Color color, float speed, double angle, Func<float, float> func)
        {
            this.startPos = new Vector2(X, Y);
            this.sprite = new Sprite(texture, 0, 0, color);
            this.path = new Path(speed, angle, func);
        }

        public Bullet(Texture2D texture, float X, float Y, float W, float H, Color color, float speed, double angle, Func<float, float> func)
        {
            this.startPos = new Vector2(X, Y);
            this.sprite = new Sprite(texture, 0, 0, W, H, color);
            this.path = new Path(speed, angle, func);
        }

        public void Update(GameTime gameTime)
        {
            path.Update(gameTime);
            sprite.Position = path.Position + startPos;
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
