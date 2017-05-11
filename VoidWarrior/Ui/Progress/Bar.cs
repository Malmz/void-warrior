
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VoidWarrior.Ui.Progress
{
    class Bar
    {
        private float maxWidth;
        private float currentValue;
        private float maxValue;
        private Sprite sprite;

        public Bar(Texture2D texture, float X, float Y, float W, float H, float value, float maxValue, Color color)
        {
            currentValue = value;
            this.maxValue = maxValue;
            maxWidth = W;
            sprite = new Sprite(texture, X, Y, maxWidth * currentValue / this.maxValue, H, color);
        }

        public float Value
        {
            get { return currentValue; }
            set
            {
                currentValue = value;
                if (currentValue > maxValue)
                {
                    currentValue = maxValue;
                }
                sprite.Width = maxWidth * currentValue / maxValue;
            }
        }

        public float MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                if (maxValue < currentValue)
                {
                    currentValue = maxValue;
                }
                sprite.Width = maxWidth * currentValue / maxValue;
            }
        }

        public bool Full
        {
            get { return currentValue == maxValue; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }

        public Vector2 Location
        {
            get { return sprite.Position; }
            set { sprite.Position = value; }
        }

        public Vector2 Size
        {
            get { return new Vector2(maxWidth, sprite.Height); }
            set
            {
                Width = value.X;
                sprite.Height = value.Y;
            }
        }

        public float X
        {
            get { return sprite.X; }
            set { sprite.X = value; }
        }

        public float Y
        {
            get { return sprite.Y; }
            set { sprite.Y = value; }
        }

        public float Width
        {
            get { return maxWidth; }
            set
            {
                maxWidth = value;
                sprite.Width = maxWidth * currentValue / maxValue;
            }
        }

        public float Height
        {
            get { return sprite.Height; }
            set { sprite.Height = value; }
        }
    }
}
