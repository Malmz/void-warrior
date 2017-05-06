using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace VoidWarrior
{
    class AnimatedSprite : Sprite
    {
        private Dictionary<string, Rectangle> sourceRects;

        public AnimatedSprite(Texture2D texture, float X, float Y, float W, float H, Color color) : base(texture, X, Y, W, H, color)
        {
            sourceRects = new Dictionary<string, Rectangle>();
        }

        public void Add(string name, Rectangle sourceRect)
        {
            sourceRects.Add(name, sourceRect);
        }

        public bool AutoTile(int spriteWidth, int spriteHeight)
        {
            if (texture.Width % spriteWidth != 0 && texture.Height % spriteHeight != 0)
            {
                return false;
            }
            int tilesX = texture.Width / spriteWidth;
            int tilesY = texture.Height / spriteHeight;
            for (int i = 0; i < tilesX; i++)
            {
                for (int j = 0; j < tilesY; j++)
                {
                    Add(("R"+j+"C"+i), new Rectangle(i * spriteWidth, j * spriteHeight, spriteWidth, spriteHeight));
                }
            }
            return true;
        }

        public void Draw(string name, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, sourceRects[name], color);
        }
    }
}
