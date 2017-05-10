using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace VoidWarrior
{
    class ResourcePool
    {
        ContentManager content;
        Dictionary<string, Texture2D> textures;
        Dictionary<string, SpriteFont> fonts;
        Dictionary<string, SoundEffect> soundEffects;

        public ResourcePool(ContentManager content)
        {
            this.content = content;
            textures = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();
            soundEffects = new Dictionary<string, SoundEffect>();
        }

        public void LoadTexture(string key)
        {
            textures.Add(key, content.Load<Texture2D>(key));
        }

        public void LoadFont(string key)
        {
            fonts.Add(key, content.Load<SpriteFont>(key));
        }

        public void LoadSound(string key)
        {
            soundEffects.Add(key, content.Load<SoundEffect>(key));
        }


        public Texture2D GetTexture(string key)
        {
            return textures[key];
        }

        public SpriteFont GetFont(string key)
        {
            return fonts[key];
        }

        public SoundEffect GetSound(string key)
        {
            return soundEffects[key];
        }
    }
}
