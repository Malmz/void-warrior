using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VoidWarrior
{
    class Sprite
    {
        private Vector2 position;
        private Vector2 size;
        private Rectangle rect;
        private Texture2D texture;
        private Color color;

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
        public Rectangle Rectangle
        {
            get
            {
                return rect;
            }
        }
    }
}
