
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VoidWarrior
{
    class Button : Text
    {
        private bool isActive;
        private Color activeColor;
        private Rectangle boundingBox;
        private MenuEvent menuEvent;

        public Button(
            string text, 
            SpriteFont font, 
            Vector2 position, 
            Color color, 
            Color activeColor, 
            MenuEvent menuEvent, 
            Align align = Align.Left) 
            : base(text, font, position, color, align)
        {
            this.activeColor = activeColor;
            boundingBox = new Rectangle(this.position.ToPoint(), font.MeasureString(text).ToPoint());
            this.menuEvent = menuEvent;
        }

        public Button(
            string text, 
            SpriteFont font, 
            float X, float Y, 
            Color color, 
            Color activeColor, 
            MenuEvent menuEvent, 
            Align align = Align.Left)
            : base(text, font, new Vector2(X, Y), color, align)
        {
            this.activeColor = activeColor;
            boundingBox = new Rectangle(this.position.ToPoint(), font.MeasureString(text).ToPoint());
            this.menuEvent = menuEvent;
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

        /// <summary>
        /// Checks if the vector is inside the button. If so, set it as active.
        /// </summary>
        /// <param name="vector"></param>
        public void Update(Point point)
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

        public MenuEvent Event
        {
            get { return menuEvent; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
    }

    
}
