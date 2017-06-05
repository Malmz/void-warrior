
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VoidWarrior.Ui.Menu;

namespace VoidWarrior
{
    class AnimatedSprite : Sprite, IDynamic
    {
        protected float frameTime;
        protected float updateTime;
        protected Rectangle currentFrame;

        public AnimatedSprite(Texture2D texture, float X, float Y, float W, float H, int frameWith, int frameHeight, Color color, float frameTime) : base(texture, X, Y, W, H, color)
        {
            this.frameTime = frameTime;
            this.currentFrame = new Rectangle(0, 0, frameWith, frameHeight);
        }

        public new void Update(GameTime gameTime)
        {
            if (frameTime < updateTime)
            {
                currentFrame.X += currentFrame.Width;
                updateTime = 0;
            }
            updateTime += gameTime.ElapsedGameTime.Milliseconds / 1000f;
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, currentFrame, color);
        }
    }
}
