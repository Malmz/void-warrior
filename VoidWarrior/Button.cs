using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VoidWarrior
{
    class Button
    {
        private bool isActive;
        private string text;
        private SpriteFont font;
        private Vector2 position;
        private Rectangle boundingBox;
        private Color activeColor;
        private Color InActiveColor;

        public Button(string text, SpriteFont font, Vector2 position, Color InActiveColor, Color activeColor, Align align = Align.Left)
        {
            this.text = text;
            this.font = font;
            this.activeColor = activeColor;
            this.InActiveColor = InActiveColor;
            this.position = position;
            switch (align)
            {
                case Align.Left:
                    this.position = position;
                    break;
                case Align.Center:
                    this.position = position;
                    this.position.X -= font.MeasureString(text).X / 2;
                    break;
                case Align.Right:
                    this.position = position;
                    this.position.X -= font.MeasureString(text).X;
                    break;
            }
            boundingBox = new Rectangle(this.position.ToPoint(), font.MeasureString(text).ToPoint());
        }

        /// <summary>
        /// Draw the button to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                spriteBatch.DrawString(font, text, position, activeColor);
            }
            else
            {
                spriteBatch.DrawString(font, text, position, InActiveColor);
            }
        }

        /// <summary>
        /// Checks if the vector is inside the button. If so, set it as active.
        /// </summary>
        /// <param name="vector"></param>
        public void Hover(Point point)
        {
            if (boundingBox.Contains(point))
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
        }

        public bool Contains(Point point)
        {
            return boundingBox.Contains(point);
        }
    }

    
}
