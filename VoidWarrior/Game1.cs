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
        MainMenu menu;
        Parallax parallax;
        Level1 level;
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
            IsMouseVisible = true;
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
            level = new Level1(res);
            menu = new MainMenu(res);
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
                
            switch (currentView)
            {
                case 0:
                    switch (menu.Update())
                    {
                        case MenuEvent.None:
                            break;
                        case MenuEvent.LevelSelect:
                            break;
                        case MenuEvent.Quit:
                            Exit();
                            break;
                        case MenuEvent.L1:
                            currentView = 1;
                            break;
                        case MenuEvent.L2:
                            break;
                        case MenuEvent.L3:
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    level.Update(gameTime);
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
                case 0:
                    menu.Draw(spriteBatch);
                    break;
                case 1:
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
