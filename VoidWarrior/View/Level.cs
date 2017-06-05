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
        private List<Enemy> aliveEnemies;
        private List<Enemy> activeEnemies;
        private Player player;
        private Vector2 playerStartPos;
        private int score;
        private int multiplier;
        private float time;
        private Text scoreUi;
        private Text multiplierUi;
        private Menu pauseMenu;
        private Menu winMenu;
        private float winDelay;
        private bool paused;
        private ViewEvent viewEvent;
        public IDynamic Background { get; set; }

        public Level(List<Enemy> enemies, Player player, SpriteFont uiFont, ResourcePool res)
        {
            this.enemies = enemies;
            aliveEnemies = new List<Enemy>(enemies.Count);
            enemies.ForEach(x => aliveEnemies.Add(x));
            this.player = player;
            playerStartPos = player.Position;
            activeEnemies = new List<Enemy>();
            viewEvent = ViewEvent.None;
            time = 0;
            score = 0;
            multiplier = 1;
            scoreUi = new Text(score.ToString(), uiFont, new Vector2(20, 0), Color.White);
            multiplierUi = new Text(multiplier.ToString(), uiFont, new Vector2(200, 0), Color.White, Align.Right);

            pauseMenu = new Menu();
            pauseMenu.AddInteractive(new Button(
                text: "Continue", 
                font: uiFont,
                position: new Vector2(Globals.SCREEN_WIDTH / 2, Globals.SCREEN_HEIGHT / 2 - 50), 
                color: Color.White, 
                activeColor: Color.Yellow, 
                viewEvent: ViewEvent.Back, 
                align: Align.Center));

            pauseMenu.AddInteractive(new Button(
                text: "Back To Menu",
                font: uiFont,
                position: new Vector2(Globals.SCREEN_WIDTH / 2, Globals.SCREEN_HEIGHT / 2 + 50),
                color: Color.White,
                activeColor: Color.Yellow,
                viewEvent: ViewEvent.ChangeView(res.GetView("mainMenu")),
                align: Align.Center));

            pauseMenu.AddStatic(new Sprite(res.GetTexture("pixel"), 0, 0, Globals.SCREEN_WIDTH, Globals.SCREEN_HEIGHT, new Color(Color.Black, 0.8f)));

            winMenu = new Menu();
            winMenu.AddInteractive(new Button(
                text: "Back",
                font: uiFont,
                position: new Vector2(Globals.SCREEN_WIDTH / 2, Globals.SCREEN_HEIGHT / 2 + 50),
                color: Color.White,
                activeColor: Color.Yellow,
                viewEvent: ViewEvent.ChangeView(res.GetView("mainMenu")),
                align: Align.Center
            ));
            winMenu.AddStatic(new Sprite(res.GetTexture("pixel"), 0, 0, Globals.SCREEN_WIDTH, Globals.SCREEN_HEIGHT, new Color(Color.Black, 0.8f)));
            winMenu.AddStatic(new Text("You Win!", res.GetFont("Guardians"), new Vector2(Globals.SCREEN_WIDTH / 2, Globals.SCREEN_HEIGHT / 2 - 75), Color.White, Align.Center));
        }

        public void Update(GameTime gameTime)
        {
            if (Background != null)
            {
                Background.Update(gameTime);
            } 
            viewEvent = ViewEvent.None;
            if (Input.Pause && !paused && winDelay <= 0)
            {
                paused = true;
            }
            else if (Input.Pause && paused)
            {
                paused = false;
            }

            if (paused == true)
            {
                pauseMenu.Update(gameTime);
                switch (pauseMenu.Event.Event)
                {
                    case ViewEvent.EventType.Back:
                        paused = false;
                        break;
                    case ViewEvent.EventType.ChangeView:
                        viewEvent = pauseMenu.Event;
                        paused = false;
                        break;
                    default:
                        break;
                }
            }
            else if (winDelay > 3)
            {
                winMenu.Update(gameTime);
                switch (winMenu.Event.Event)
                {
                    case ViewEvent.EventType.ChangeView:
                        viewEvent = winMenu.Event;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                activeEnemies = aliveEnemies.Where(enemy => enemy.Delay < time).ToList();

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
                        viewEvent = ViewEvent.Back;
                    }
                });

                deletedEnemies.ForEach(enemy => aliveEnemies.Remove(enemy));
                deletedBullets.ForEach(bullet => player.Bullets.Remove(bullet));

                time += gameTime.ElapsedGameTime.Milliseconds / 1000f;
                scoreUi.DisplayText = "Score: " + score.ToString();
                multiplierUi.DisplayText = "x" + multiplier.ToString();
                multiplierUi.X = scoreUi.X + scoreUi.Width + 10;

                if (aliveEnemies.Count == 0)
                {
                    winDelay += gameTime.ElapsedGameTime.Milliseconds / 1000f;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Background != null)
            {
                Background.Draw(spriteBatch);
            }
            activeEnemies.ForEach(x => x.Draw(spriteBatch));
            player.Draw(spriteBatch);
            scoreUi.Draw(spriteBatch);
            multiplierUi.Draw(spriteBatch);
            if (paused == true)
            {
                pauseMenu.Draw(spriteBatch);
            }
            else if (winDelay > 3)
            {
                winMenu.Draw(spriteBatch);
            }
        }

        public ViewEvent Event { get { return viewEvent; } }

        public void Reset()
        {
            enemies.ForEach(x => x.Reset());
            aliveEnemies.Clear();
            enemies.ForEach(x => aliveEnemies.Add(x));
            player.Reset(playerStartPos);
            time = 0;
            score = 0;
        }
    }
}
