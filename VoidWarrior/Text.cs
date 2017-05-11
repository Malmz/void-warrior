using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

enum Align
{
    Right,
    Left,
    Center
}

namespace VoidWarrior
{
    class Text
    {
        protected string text;
        protected SpriteFont font;
        protected Vector2 position;
        protected Color color;

        public string DisplayText {
            get { return text; } 
            set { text = value; }
        }

        public float X
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public float Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float Width
        {
            get { return Size.X; }
        }

        public float Height
        {
            get { return Size.Y; }
        }

        public Vector2 Size
        {
            get { return font.MeasureString(text); }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle(Position.ToPoint(), Size.ToPoint()); }
        }

        public Text(string text, SpriteFont font, Vector2 position, Color color, Align align = Align.Left)
        {
            this.text = text;
            this.font = font;
            this.color = color;
            switch (align)
            {
                case Align.Left:
                    this.position = position;
                    break;
                case Align.Center:
                    this.position = position - new Vector2(font.MeasureString(text).X / 2, 0);
                    break;
                case Align.Right:
                    this.position = position - new Vector2(font.MeasureString(text).X, 0);
                    break;
            }
        }

        public Text(string text, SpriteFont font, float X, float Y, Color color, Align align = Align.Left)
        {
            this.text = text;
            this.font = font;
            this.color = color;
            switch (align)
            {
                case Align.Left:
                    this.position = new Vector2(X, Y);
                    break;
                case Align.Center:
                    this.position = new Vector2(X - font.MeasureString(text).X / 2, Y);
                    break;
                case Align.Right:
                    this.position = new Vector2(X - font.MeasureString(text).X, Y);
                    break;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, color);
        }
    }
}
