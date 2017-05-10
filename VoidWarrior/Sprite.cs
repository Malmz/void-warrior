using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VoidWarrior
{
    class Sprite
    {
        protected Vector2 position;
        protected Vector2 size;
        protected Rectangle rect;
        protected Texture2D texture;
        protected Color color;

        public Sprite(Texture2D texture, float X, float Y, Color color)
        {
            this.Position = new Vector2(X, Y);
            this.Size = texture.Bounds.Size.ToVector2();
            this.texture = texture;
            this.color = color;
        }

        public Sprite(Texture2D texture, float X, float Y, float W, float H, Color color)
        {
            this.Position = new Vector2(X, Y);
            this.Size = new Vector2(W, H);
            this.texture = texture;
            this.color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, color);
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
                rect.Location = position.ToPoint();
            }
        }

        public Vector2 Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
                rect.Size = size.ToPoint();
            }
        }

        public float X
        {
            get
            {
                return position.X;
            }
            set
            {
                position.X = value;
                rect.X = (int)position.X;
            }
        }
        public float Y
        {
            get
            {
                return position.Y;
            }
            set
            {
                position.Y = value;
                rect.Y = (int)position.Y;
            }
        }
        public float Width
        {
            get
            {
                return size.X;
            }
            set
            {
                size.X = value;
                rect.Width = (int)size.X;
            }
        }
        public float Height
        {
            get
            {
                return size.Y;
            }
            set
            {
                size.Y = value;
                rect.Height = (int)size.Y;
            }
        }
        public Rectangle Bounds
        {
            get
            {
                return rect;
            }
        }
        public Texture2D Texture
        {
            get { return texture; }
        }
        public Color Color
        {
            get { return color; }
        }
    }
}
