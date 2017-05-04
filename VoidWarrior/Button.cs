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

        //TODO: Optimize with textures instead of sprites

        public Button(string text, SpriteFont font, Vector2 position, Color activeColor, Color InActiveColor)
        {
            this.text = text;
            this.font = font;
            this.position = position;
            this.activeColor = activeColor;
            this.InActiveColor = InActiveColor;
            boundingBox = new Rectangle(position.ToPoint(), new Point());
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
        public void Hover(Vector2 vector)
        {
            if (boundingBox.Contains(vector.X, vector.Y))
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
        }
    }

    
}
