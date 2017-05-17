using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using VoidWarrior.Ui.Menu;

namespace VoidWarrior.View
{
    class Level : IView
    {

        private List<Enemy> enemies;
        private List<Enemy> activeEnemies;
        private Player player;
        private int score;
        private int multiplier;
        private float time;
        private Text scoreUi;
        private Text multiplierUi;
        private List<ViewEvent> viewEvents;

        public Level(List<Enemy> enemies, Player player, SpriteFont uiFont)
        {
            this.enemies = enemies;
            this.player = player;
            activeEnemies = new List<Enemy>();
            viewEvents = new List<ViewEvent>();
            time = 0;
            score = 0;
            multiplier = 1;
            scoreUi = new Text(score.ToString(), uiFont, new Vector2(20, 0), Color.White);
            multiplierUi = new Text(multiplier.ToString(), uiFont, new Vector2(200, 0), Color.White, Align.Right);
        }

        public void Update(GameTime gameTime)
        {
            ViewEvent _event = ViewEvent.None;
            activeEnemies = enemies.Where(enemy => enemy.Delay < time).ToList();

            activeEnemies.ForEach(x => x.Update(gameTime));

            player.Update(gameTime);

            List<Enemy> deletedEnemies = new List<Enemy>();
            List<Bullet> deletedBullets = new List<Bullet>();

            activeEnemies.ForEach(enemy => {
                player.Bullets.ForEach(bullet => {
                    if (enemy.Bounds.Intersects(bullet.Bounds))
                    {
                        enemy.Health -= bullet.Damage;
                        if (enemy.Health <= 0)
                        {
                            score += enemy.Value * multiplier;
                            deletedEnemies.Add(enemy);
                            deletedBullets.Add(bullet);
                        }
                        else
                        {
                            deletedBullets.Add(bullet);
                        }
                    }
                });
                if (player.Bounds.Intersects(enemy.Bounds))
                {
                    _event = ViewEvent.Quit;
                }
            });

            deletedEnemies.ForEach(enemy => enemies.Remove(enemy));
            deletedBullets.ForEach(bullet => player.Bullets.Remove(bullet));

            time += gameTime.ElapsedGameTime.Milliseconds / 1000f;
            scoreUi.DisplayText = "Score: " + score.ToString();
            multiplierUi.DisplayText = "x" + multiplier.ToString();
            multiplierUi.X = scoreUi.X + scoreUi.Width + 10;

            viewEvents.Add(_event);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            activeEnemies.ForEach(x => x.Draw(spriteBatch));
            player.Draw(spriteBatch);
            scoreUi.Draw(spriteBatch);
            multiplierUi.Draw(spriteBatch);
        }

        public List<ViewEvent> Events { get { return viewEvents; } }
    }
}
