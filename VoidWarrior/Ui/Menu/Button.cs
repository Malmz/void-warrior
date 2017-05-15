
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VoidWarrior.View;

namespace VoidWarrior.Ui.Menu
{
    class Button : Text, IInteractive
    {
        private bool isActive;
        private Color activeColor;
        private Rectangle boundingBox;
        private ViewEvent viewEvent;

        public Button(
            string text, 
            SpriteFont font, 
            Vector2 position, 
            Color color, 
            Color activeColor, 
            ViewEvent viewEvent, 
            Align align = Align.Left) 
            : base(text, font, position, color, align)
        {
            this.activeColor = activeColor;
            boundingBox = new Rectangle(this.position.ToPoint(), font.MeasureString(text).ToPoint());
            this.viewEvent = viewEvent;
        }

        /// <summary>
        /// Draw the button to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                spriteBatch.DrawString(font, text, position, activeColor);
            }
            else
            {
                spriteBatch.DrawString(font, text, position, color);
            }
        }

        public ViewEvent Event
        {
            get { return viewEvent; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
    }

    
}
