using Microsoft.Xna.Framework;
using System;

namespace VoidWarrior
{
    /// <summary>
    /// A Path following the graph of `func`
    /// </summary>
    class Path
    {
        private Vector2 position;
        private float speed;
        private double angle;
        private Func<float, float> func;


        public Path(float speed, double angle, Func<float, float> func)
        {
            this.speed = speed;
            this.angle = -angle * Math.PI / 180;
            this.func = func;
        }

        public void Update(GameTime gameTime)
        {
            position.X += speed * gameTime.ElapsedGameTime.Milliseconds / 1000f;
            position.Y = func(position.X);
        }

        public Vector2 Position
        {
            get
            {
                return new Vector2(position.X * (float)Math.Cos(angle) - position.Y * (float)Math.Sin(angle), position.X * (float)Math.Sin(angle) + position.Y * (float)Math.Cos(angle));
            }
        }

        public float X
        {
            get
            {
                return position.X * (float)Math.Cos(angle) - position.Y * (float)Math.Sin(angle);
            }
        }
        public float Y
        {
            get
            {
                return position.X * (float)Math.Sin(angle) + position.Y * (float)Math.Cos(angle);
            }
        }

        public float Speed
        {
            get { return speed; }
        }

        public double Angle
        {
            get { return -angle * 180 / Math.PI; }
        }

        public Func<float, float> Func
        {
            get { return func; }
        }

        public void Reset()
        {
            position.X = 0;
        }
    }
}
