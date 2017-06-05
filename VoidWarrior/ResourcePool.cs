using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using VoidWarrior.View;

namespace VoidWarrior
{
    class ResourcePool
    {
        ContentManager content;
        Dictionary<string, Texture2D> textures;
        Dictionary<string, SpriteFont> fonts;
        Dictionary<string, SoundEffect> soundEffects;
        Dictionary<string, IView> viewCache;


        public ResourcePool(ContentManager content)
        {
            this.content = content;
            textures = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();
            soundEffects = new Dictionary<string, SoundEffect>();
            viewCache = new Dictionary<string, IView>();
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

        /// <summary>
        /// Add a view to the cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="view"></param>
        public void AddView(string key, IView view)
        {
            viewCache.Add(key, view);
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

        public IView GetView(string key)
        {
            return viewCache[key];
        }
    }
}
