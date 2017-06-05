
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace VoidWarrior
{
    class Gun
    {
        private int ammo;
        private int magazineSize;
        private bool reloading;
        private float reloadTime;
        private float fireRate;
        private float lastEvent;
        private Bullet template;
        private List<Bullet> bullets;

        public Gun(int ammo, float fireRate, float reloadTime, Bullet template)
        {
            magazineSize = ammo;
            this.ammo = ammo;
            this.fireRate = fireRate;
            this.reloadTime = reloadTime;
            reloading = false;
            this.template = template;
            bullets = new List<Bullet>();
        }

        /// <summary>
        /// Fires a bullet 
        /// </summary>
        /// <returns></returns>
        public bool Fire()
        {
            if (fireRate < lastEvent && ammo > 0 && !reloading)
            {
                bullets.Add(template.Clone);
                lastEvent = 0;
                ammo -= 1;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Refills the ammo counter
        /// </summary>
        public void Reload()
        {
            reloading = true;
        }

        /// <summary>
        /// Deletes bullets not within an area
        /// </summary>
        /// <param name="range"></param>
        public void Range(Rectangle range)
        {
            bullets = bullets.Where(x => range.Contains(x.Bounds) || range.Intersects(x.Bounds)).ToList();
        }

        /// <summary>
        /// Moves all bullets along their path and time reloading
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            bullets.ForEach(x => x.Update(gameTime));
            if (ammo <= 0)
            {
                Reload();
            }
            if (reloadTime < lastEvent && reloading)
            {
                reloading = false;
                ammo = magazineSize;
            }
            lastEvent += gameTime.ElapsedGameTime.Milliseconds / 1000f;
        }

        /// <summary>
        /// Draw all the bullets
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            bullets.ForEach(x => x.Draw(spriteBatch));
        }

        public int Ammo
        {
            get { return ammo; }
            set { ammo = value; }
        }

        public float FireRate
        {
            get { return fireRate; }
            set { fireRate = value; }
        }

        public List<Bullet> Bullets
        {
            get { return bullets; }
            set { bullets = value; }
        }

        public Vector2 Position
        {
            get { return template.StartPos; }
            set { template.StartPos = value; }
        }

        public int MagazineSize
        {
            get { return magazineSize; }
        }

        public void Reset()
        {
            ammo = magazineSize;
            lastEvent = 0;
            reloading = false;
            bullets.Clear();
        }
    }
}
