
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace VoidWarrior
{
    class Gun
    {
        private int ammo;
        private float fireRate;
        private float lastFire;
        private Bullet template;
        private List<Bullet> bullets;

        public Gun(int ammo, float fireRate, Bullet template)
        {
            this.ammo = ammo;
            this.fireRate = fireRate;
            this.template = template;
            this.bullets = new List<Bullet>();
        }

        public bool Fire()
        {
            if (fireRate < lastFire)
            {
                bullets.Add(template.Clone);
                lastFire = 0;
                return true;
            }
            return false;
        }

        public void Range(Rectangle range)
        {
            bullets = bullets.Where(x => Globals.SCREEN.Contains(x.Bounds) || Globals.SCREEN.Intersects(x.Bounds)).ToList();
        }

        public void Update(GameTime gameTime)
        {
            bullets.ForEach(x => x.Update(gameTime));
            lastFire += gameTime.ElapsedGameTime.Milliseconds / 1000f;
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
    }
}
