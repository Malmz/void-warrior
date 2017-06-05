using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Linq;
using VoidWarrior.Ui.Menu;
using VoidWarrior.View;

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
        IView currentView;

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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            res.LoadTexture("VoidShipSpriteSheet");
            res.LoadTexture("BulletSpriteSheet");
            res.LoadTexture("EnemyShip");
            res.LoadTexture("spaceship_enemy_red");
            res.LoadTexture("pixel");
            res.LoadTexture("BackgroundBack");
            res.LoadTexture("BackgroundFront");
            res.LoadFont("Guardians");
            res.LoadFont("Earth Orbiter");
            res.LoadFont("Forced");
            res.LoadSound("Shoot");

            LateInit();
        }

        protected void LateInit()
        {
            levelSelect = new Menu();
            res.AddView("levelSelect", levelSelect);
            mainMenu = new Menu();
            res.AddView("mainMenu", mainMenu);

            var tempPos = new Vector2(Globals.SCREEN_WIDTH / 6, 280);

            Directory.EnumerateFiles("Levels").ToList().ForEach(element =>
            {
                res.AddView(element, Parser.ParseLevel(element, res));
                levelSelect.AddInteractive(new Button(
                    System.IO.Path.GetFileNameWithoutExtension(element),
                    res.GetFont("Forced"),
                    tempPos,
                    Color.White,
                    Color.Yellow,
                    ViewEvent.ChangeView(res.GetView(element))));
                if (tempPos.Y < Globals.SCREEN_HEIGHT - 100) { tempPos.Y += 100; } else { tempPos.Y += 280; tempPos.X += Globals.SCREEN_WIDTH / 6; }
            });


            levelSelect.AddStatic(new Text("Select Level", res.GetFont("Earth Orbiter"), new Vector2(Globals.SCREEN_WIDTH / 2, 100), Color.Yellow, Align.Center));
            levelSelect.BackEvent = ViewEvent.ChangeView(res.GetView("mainMenu"));

            mainMenu.AddStatic(new Text(
                "Void Warrior", 
                res.GetFont("Earth Orbiter"), 
                new Vector2(Globals.SCREEN_WIDTH / 2, 100), 
                Color.Yellow, 
                Align.Center));
            mainMenu.AddInteractive(new Button(
                "Start", 
                res.GetFont("Guardians"), 
                new Vector2(Globals.SCREEN_WIDTH / 2, 400), 
                Color.White, Color.Yellow, 
                ViewEvent.ChangeView(levelSelect),
                Align.Center));
            mainMenu.AddInteractive(new Button(
                "Quit", 
                res.GetFont("Guardians"),
                new Vector2(Globals.SCREEN_WIDTH / 2, 500), 
                Color.White, Color.Yellow, 
                ViewEvent.Back, 
                Align.Center));

            currentView = mainMenu;

            

            //controllerStatus = new Text(Input.GamepadConnected.ToString(), res.GetFont("Guardians"), 0, 0, Color.White);
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

            //controllerStatus.DisplayText = Input.GamepadConnected.ToString();

            // Update background
            parallax.Update(gameTime);

            currentView.Update(gameTime);

            switch (currentView.Event.Event)
            {
                case ViewEvent.EventType.None:
                    break;
                case ViewEvent.EventType.Back:
                    Exit();
                    break;
                case ViewEvent.EventType.ChangeView:
                    currentView = currentView.Event.View;
                    currentView.Reset();
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
            //controllerStatus.Draw(spriteBatch);
            currentView.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
