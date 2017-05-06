using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VoidWarrior
{
    class Parallax
    {
        private Texture2D back;
        private Texture2D front;
        private Vector2 backPos = new Vector2();
        private Vector2 frontPos;

        public Parallax(Texture2D back, Texture2D front)
        {
            this.back = back;
            this.front = front;
        }

        public void Update(GameTime gameTime)
        {
            frontPos.Y += 0.05f * gameTime.ElapsedGameTime.Milliseconds;
            backPos.Y += 0.02f * gameTime.ElapsedGameTime.Milliseconds;
            if (frontPos.Y > front.Height || frontPos.Y < -back.Height)
            {
                frontPos.Y = 0;
            }
            if (backPos.Y > back.Height || backPos.Y < -back.Height)
            {
                backPos.Y = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int backWidth = Globals.SCREEN_WIDTH / back.Width + 1;
            int backHeight = Globals.SCREEN_HEIGHT / back.Height + 1;
            for (int i = -1; i < backHeight; i++)
            {
                for (int j = 0; j < backWidth; j++)
                {
                    spriteBatch.Draw(back, backPos + new Vector2(j * back.Width, i * back.Height), Color.White);
                }
            }

            int frontWidth = Globals.SCREEN_WIDTH / front.Width + 1;
            int frontHeight = Globals.SCREEN_HEIGHT / front.Height + 1;
            for (int i = -1; i < frontHeight; i++)
            {
                for (int j = 0; j < frontWidth; j++)
                {
                    spriteBatch.Draw(front, frontPos + new Vector2(j * front.Width, i * front.Height), Color.White);
                }
            }

        }
    }
}
