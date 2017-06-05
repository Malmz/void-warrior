using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VoidWarrior.Ui.Menu;

namespace VoidWarrior
{
    class Bullet : IDynamic
    {
        private Sprite sprite;
        private Vector2 startPos;
        private Path path;
        private int damage;

        /// <summary>
        /// Create new bullet
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="damage"></param>
        /// <param name="path"></param>
        public Bullet(Sprite sprite, int damage, Path path)
        {
            startPos = sprite.Position;
            this.sprite = sprite;
            this.sprite.Position = new Vector2();
            this.path = path;
            this.damage = damage;
        }

        /// <summary>
        /// Move bullet along its path
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            path.Update(gameTime);
            sprite.Position = path.Position + startPos;
        }

        /// <summary>
        /// Draw bullet to screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }

        /// <summary>
        /// Create new identical bullet object
        /// </summary>
        public Bullet Clone
        {
            get
            {
                return new Bullet(new Sprite(sprite.Texture, startPos.X, startPos.Y, sprite.Width, sprite.Height, sprite.SourceRectangle, sprite.Color), damage, new Path(path.Speed, path.Angle, path.Func));
            }
        }

        public Rectangle Bounds
        {
            get
            {
                return sprite.Bounds;
            }
        }

        public int Damage
        {
            get
            {
                return damage;
            }

            set
            {
                damage = value;
            }
        }

        /// <summary>
        /// Start position form where the bullet will move
        /// </summary>
        public Vector2 StartPos
        {
            get { return startPos; }
            set { startPos = value; }
        }
    }
}
