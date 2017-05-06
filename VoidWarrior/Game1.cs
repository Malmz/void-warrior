using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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
        Parallax parallax;
        Level1 level;
        Sprite cursor;
        int currentView;

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
            cursor = new Sprite(res.GetTexture("pixel"), 0, 0, 15, 15, Color.Orange);
            level = new Level1(res);
            mainMenu = new Menu();
            mainMenu.AddText("Void Warrior", res.GetFont("Earth Orbiter"), Globals.SCREEN_WIDTH / 2, 100, Color.Yellow, Align.Center);
            mainMenu.AddButton("Start", res.GetFont("Guardians"), Globals.SCREEN_WIDTH / 2, 400, Color.White, Color.Yellow, MenuEvent.ChangeView(2), Align.Center);
            mainMenu.AddButton("Level Select", res.GetFont("Guardians"), Globals.SCREEN_WIDTH / 2, 500, Color.White, Color.Yellow, MenuEvent.ChangeView(1), Align.Center);
            mainMenu.AddButton("Quit", res.GetFont("Guardians"), Globals.SCREEN_WIDTH / 2, 600, Color.White, Color.Yellow, MenuEvent.Quit, Align.Center);


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
            Events.Update();
            if (Events.KeyDown(Keys.Escape))
            {
                Exit();
            }
            if (Events.KeyPressed(Keys.F))
            {
                graphics.ToggleFullScreen();
            }

            parallax.Update(gameTime);
            MenuEvent menuEvent = mainMenu.Update();
            
            
            switch (currentView)
            {
                case 0:
                    switch (menuEvent.Event)
                    {
                        case MenuEvent.EventType.None:
                            break;
                        case MenuEvent.EventType.Quit:
                            Exit();
                            break;
                        case MenuEvent.EventType.ChangeView:
                            currentView = menuEvent.View;
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    level.Update(gameTime);
                    break;
                default:
                    break;
            }

            cursor.Position = Events.MousePosition.ToVector2();

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
                case 0:
                    mainMenu.Draw(spriteBatch);
                    break;
                case 2:
                    level.Draw(spriteBatch);
                    break;
                default:
                    break;
            }
            cursor.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
