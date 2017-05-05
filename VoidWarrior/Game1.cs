using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace VoidWarrior
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D shipTexture;
        SpriteFont guardians;
        SpriteFont earthorbiter;
        Player player;
        MainMenu menu;
        Parallax parallax;
        Bullet bullet;

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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            shipTexture = Content.Load<Texture2D>("VoidShip");
            parallax = new Parallax(Content.Load<Texture2D>("BackgroundBack"), Content.Load<Texture2D>("BackgroundFront"));
            bullet = new Bullet(new Sprite(Content.Load<Texture2D>("pixel"), 0, 0, 50, 50, Color.Red), new Vector2(150, 150), 0.01f, Math.PI * (8.0/7.0), x => (float)Math.Sin(x));
            guardians = Content.Load<SpriteFont>("Guardians");
            earthorbiter = Content.Load<SpriteFont>("Earth Orbiter");

            LateInit();
        }

        protected void LateInit()
        {
            player = new Player(shipTexture, 100, 100, 150, 150, Color.White);
            menu = new MainMenu("Void Warrior", earthorbiter, guardians);
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
                Exit();

            parallax.Update(gameTime);
            bullet.Update(gameTime);
            player.Update(gameTime);
            MenuEvent e = menu.Update();
            if (e == MenuEvent.Quit)
            {
                Exit();
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
            bullet.Draw(spriteBatch);
            player.Draw(spriteBatch);
            menu.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
