
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
            this.magazineSize = ammo;
            this.ammo = ammo;
            this.fireRate = fireRate;
            this.reloadTime = reloadTime;
            this.reloading = false;
            this.template = template;
            this.bullets = new List<Bullet>();
        }

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

        public void Reload()
        {
            reloading = true;
        }

        public void Range(Rectangle range)
        {
            bullets = bullets.Where(x => Globals.SCREEN.Contains(x.Bounds) || Globals.SCREEN.Intersects(x.Bounds)).ToList();
        }

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
    }
}
