using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VoidWarior
{
    class Player
    {
        private const float SPEED = 0.3f;
        private Sprite ship;
        private Vector2 move;

        public Player(Texture2D texture, float X, float Y, Color color)
        {
            ship = new Sprite(texture, X, Y, color);
            move = new Vector2();

        }

        public void Update(GameTime gameTime)
        {
            if (Events.KeyDown(Keys.W))
            {
                move += new Vector2(0, -1);
            }
            if (Events.KeyDown(Keys.S))
            {
                move += new Vector2(0, 1);
            }
            if (Events.KeyDown(Keys.A))
            {
                move += new Vector2(-1, 0);
            }
            if (Events.KeyDown(Keys.D))
            {
                move += new Vector2(1, 0);
            }
            if (move.X != 0 || move.Y != 0)
            {
                move.Normalize();
            }
            ship.Position += move * SPEED * gameTime.ElapsedGameTime.Milliseconds;
            move -= move;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ship.Draw(spriteBatch);
        }

        public Vector2 Position
        {
            get
            {
                return ship.Position;
            }

            set
            {
                ship.Position = value;
            }
        }

        public Vector2 Size
        {
            get
            {
                return ship.Size;
            }

            set
            {
                ship.Size = value;
            }
        }
    }
}
