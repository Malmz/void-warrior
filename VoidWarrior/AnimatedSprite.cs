using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace VoidWarrior
{
    class AnimatedSprite
    {
        private Texture2D texture;
        private Dictionary<string, Rectangle> sourceRects;
        private Rectangle destRect;
        private Vector2 position;
        private Vector2 size;
        private Color color;

        public AnimatedSprite(Texture2D texture, Vector2 position, Vector2 size, Color color)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            this.color = color;
            this.destRect = new Rectangle(position.ToPoint(), size.ToPoint());
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
            spriteBatch.Draw(texture, destRect, sourceRects[name], color);
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
                destRect.Location = position.ToPoint();
            }
        }

        public Vector2 Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
                destRect.Size = size.ToPoint();
            }
        }
    }
}
