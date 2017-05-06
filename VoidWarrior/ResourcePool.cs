using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace VoidWarrior
{
    class ResourcePool
    {
        ContentManager content;
        Dictionary<string, Texture2D> textures;
        Dictionary<string, SpriteFont> fonts;

        public ResourcePool(ContentManager content)
        {
            this.content = content;
            textures = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();
        }

        public void AddTexture(string key)
        {
            textures.Add(key, content.Load<Texture2D>(key));
        }

        public void AddFont(string key)
        {
            fonts.Add(key, content.Load<SpriteFont>(key));
        }

        public Texture2D GetTexture(string key)
        {
            return textures[key];
        }

        public SpriteFont GetFont(string key)
        {
            return fonts[key];
        }
    }
}
