using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VoidWarrior
{
    class Bullet
    {
        private Sprite sprite;
        private Vector2 position;
        private double angle;
        private Func<float, float> path;
        private float speed;

        public Bullet(Sprite sprite, Vector2 position, float speed, double angle, Func<float, float> path)
        {
            this.position = position;
            this.sprite = sprite;
            this.angle = angle;
            this.speed = speed;
            this.path = path;
        }

        public void Update(GameTime gameTime)
        {
            position.X += speed * gameTime.ElapsedGameTime.Milliseconds;
            position.Y = path(position.X);
            sprite.X = position.X * (float)Math.Cos(angle) - position.Y * (float)Math.Sin(angle);
            sprite.Y = position.X * (float)Math.Sin(angle) + position.Y * (float)Math.Cos(angle);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }
        
    }
}
