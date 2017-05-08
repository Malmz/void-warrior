using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Linq;

namespace VoidWarrior
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ResourcePool res;
        Menu mainMenu;
        Menu levelSelect;
        Parallax parallax;
        Level level;
        MenuEvent.ViewType currentView;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = Globals.SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = Globals.SCREEN_HEIGHT;
            graphics.ApplyChanges();

            res = new ResourcePool(Content);
            currentView = MenuEvent.ViewType.MainMenu;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            res.AddTexture("VoidShip");
            res.AddTexture("pixel");
            res.AddTexture("BackgroundBack");
            res.AddTexture("BackgroundFront");
            res.AddFont("Guardians");
            res.AddFont("Earth Orbiter");

            LateInit();
        }

        protected void LateInit()
        {
            mainMenu = new Menu();
            mainMenu.AddText("Void Warrior", res.GetFont("Earth Orbiter"), Globals.SCREEN_WIDTH / 2, 100, Color.Yellow, Align.Center);
            mainMenu.AddButton("Start", res.GetFont("Guardians"), Globals.SCREEN_WIDTH / 2, 400, Color.White, Color.Yellow, MenuEvent.ChangeToLevel("Levels/Level1.json"), Align.Center);
            mainMenu.AddButton("Level Select", res.GetFont("Guardians"), Globals.SCREEN_WIDTH / 2, 500, Color.White, Color.Yellow, MenuEvent.ChangeView(MenuEvent.ViewType.LevelSelect), Align.Center);
            mainMenu.AddButton("Quit", res.GetFont("Guardians"), Globals.SCREEN_WIDTH / 2, 600, Color.White, Color.Yellow, MenuEvent.Back, Align.Center);

            levelSelect = new Menu();
            levelSelect.AddText("Select Level", res.GetFont("Earth Orbiter"), Globals.SCREEN_WIDTH / 2, 100, Color.Yellow, Align.Center);

            var tempPos = new Vector2();

            Directory.EnumerateFiles("Levels").ToList().ForEach(element => 
            {
                levelSelect.AddButton(element, res.GetFont("Guardians"), tempPos * 100, Color.White, Color.Yellow, MenuEvent.ChangeToLevel(element));
                if (tempPos.X % 5 == 0) { tempPos.X += 1; } else { tempPos.Y += 1; tempPos.X = 0; }
                
            });

            parallax = new Parallax(res.GetTexture("BackgroundBack"), res.GetTexture("BackgroundFront"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Update Input
            Input.Update();

            // Toggle Fullscreen
            if (Input.KeyPressed(Keys.F))
            {
                graphics.ToggleFullScreen();
            }

            // Update background
            parallax.Update(gameTime);

            switch (currentView)
            {
                // Main Menu game loop
                case MenuEvent.ViewType.MainMenu:

                    // Exit on Ecs
                    if (Input.KeyPressed(Keys.Escape)) { Exit(); }

                    // Get events from main menu
                    MenuEvent mainMenuEvent = mainMenu.Update(gameTime);

                    // Filter events
                    switch (mainMenuEvent.Event)
                    {
                        case MenuEvent.EventType.None:
                            break;
                        case MenuEvent.EventType.Back:
                            Exit();
                            break;
                        
                         // Change current view to view in event
                        case MenuEvent.EventType.ChangeView:
                            currentView = mainMenuEvent.View;
                            if (mainMenuEvent.View == MenuEvent.ViewType.Level)
                            {
                                level = LevelParser.Parse(mainMenuEvent.LevelName, res);
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                
                // Level select game loop
                case MenuEvent.ViewType.LevelSelect:

                    if (Input.KeyPressed(Keys.Escape)) { currentView = MenuEvent.ViewType.MainMenu; }
                    MenuEvent levelSelectEvent = levelSelect.Update(gameTime);

                    switch (levelSelectEvent.Event)
                    {
                        case MenuEvent.EventType.None:
                            break;
                        case MenuEvent.EventType.Back:
                            currentView = MenuEvent.ViewType.MainMenu;
                            break;
                        case MenuEvent.EventType.ChangeView:
                            currentView = levelSelectEvent.View;
                            if (levelSelectEvent.View == MenuEvent.ViewType.Level)
                            {
                                level = LevelParser.Parse(levelSelectEvent.LevelName, res);
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case MenuEvent.ViewType.Level:

                    if (Input.KeyPressed(Keys.Escape)) { currentView = MenuEvent.ViewType.MainMenu; }

                    MenuEvent levelEvent = level.Update(gameTime);
                    switch (levelEvent.Event)
                    {
                        case MenuEvent.EventType.None:
                            break;
                        case MenuEvent.EventType.Back:
                            currentView = MenuEvent.ViewType.MainMenu;
                            break;
                        case MenuEvent.EventType.ChangeView:
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            parallax.Draw(spriteBatch);
            switch (currentView)
            {
                case MenuEvent.ViewType.MainMenu:
                    mainMenu.Draw(spriteBatch);
                    break;
                case MenuEvent.ViewType.LevelSelect:
                    break;
                case MenuEvent.ViewType.Level:
                    level.Draw(spriteBatch);
                    break;
                default:
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
