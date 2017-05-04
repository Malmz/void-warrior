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
        private string text;
        private SpriteFont font;
        private Vector2 position;
        private Color color;

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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, color);
        }
    }
}
