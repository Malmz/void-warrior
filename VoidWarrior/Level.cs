using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace VoidWarrior
{
    class Level
    {

        private List<Enemy> enemies;
        private List<Enemy> activeEnemies;
        private Player player;
        private float time = 0;

        public Level(List<Enemy> enemies, Player player)
        {
            this.enemies = enemies;
            this.player = player;
            this.activeEnemies = new List<Enemy>();
        }

        public MenuEvent Update(GameTime gameTime)
        {
            MenuEvent _event = MenuEvent.None;
            activeEnemies = enemies.Where(enemy => enemy.Delay < time).ToList();

            activeEnemies.ForEach(x => x.Update(gameTime));
            if (activeEnemies.Count == 3)
            {

            }
            player.Update(gameTime);

            List<Enemy> deletedEnemies = new List<Enemy>();
            List<Bullet> deletedBullets = new List<Bullet>();

            activeEnemies.ForEach(enemy => {
                player.Bullets.ForEach(bullet => {
                    if (enemy.Bounds.Intersects(bullet.Bounds))
                    {
                        deletedEnemies.Add(enemy);
                        deletedBullets.Add(bullet);
                    }
                });
                if (player.Bounds.Intersects(enemy.Bounds))
                {
                    _event = MenuEvent.Back;
                }
            });

            deletedEnemies.ForEach(enemy => enemies.Remove(enemy));
            deletedBullets.ForEach(bullet => player.Bullets.Remove(bullet));

            time += gameTime.ElapsedGameTime.Milliseconds / 1000f;
            return _event;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            activeEnemies.ForEach(x => x.Draw(spriteBatch));
            player.Draw(spriteBatch);
        }


    }
}
