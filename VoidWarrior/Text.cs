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

        public string DisplayText { get => text; set => text = value; }

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
