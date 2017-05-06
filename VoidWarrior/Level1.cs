using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace VoidWarrior
{
    class Level1
    {
        private Player player;
        private List<Enemy> enemies;
        private Texture2D enemyTexture;

        public Level1(ResourcePool res)
        {
            this.player = new Player(res.GetTexture("VoidShip"), res.GetTexture("pixel"), Globals.SCREEN_WIDTH / 2 - 50, Globals.SCREEN_HEIGHT - 175, 100, 100, Color.White);
            this.enemies = new List<Enemy>();
            enemyTexture = res.GetTexture("pixel");
        }

        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            enemies.ForEach(x => x.Update(gameTime));

            List<Enemy> deletedEnemies = new List<Enemy>();
            List<Bullet> deletedBullets = new List<Bullet>();

            enemies.ForEach(enemy => {
                player.Bullets.ForEach(bullet => {
                    if (enemy.Bounds.Intersects(bullet.Bounds))
                    {
                        deletedEnemies.Add(enemy);
                        deletedBullets.Add(bullet);
                    }
                });
            });

            deletedEnemies.ForEach(enemy => enemies.Remove(enemy));
            deletedBullets.ForEach(bullet => player.Bullets.Remove(bullet));

            if (Events.KeyPressed(Keys.T))
            {
                AddEnemy(new Enemy(enemyTexture, 500, 10, 100, 100, Color.White, 0.05f, 270, x => (float)Math.Sin(x / 100) * 100));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);
            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }
    }
}
