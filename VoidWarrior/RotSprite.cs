
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace VoidWarrior
{
    class RotSprite
    {
        private Texture2D texture;
        private Vector2 TL, TR, BL, BR;
        float rotation;
        private Color color;
        public RotSprite(Texture2D texture, Vector2 position, Vector2 size, Color color, float rotation)
        {
            this.texture = texture;
            this.color = color;
            this.rotation = rotation * (float)Math.PI / 180;

            TL = new Vector2(-size.X / 2, -size.Y / 2);
            TR = new Vector2( size.X / 2, -size.Y / 2);
            BL = new Vector2(-size.X / 2,  size.Y / 2);
            BR = new Vector2( size.X / 2,  size.Y / 2);

            TL = new Vector2((float)((-size.X / 2) * Math.Cos(this.rotation) - (-size.Y / 2) * Math.Sin(this.rotation)));

            TL.X = (float)(TL.X * Math.Cos(this.rotation) - TL.Y * Math.Sin(this.rotation));
            TL.Y = (float)(TL.Y * Math.Sin(this.rotation) - TL.X * Math.Cos(this.rotation));

            TR.X = (float)(TR.X * Math.Cos(this.rotation) - TR.Y * Math.Sin(this.rotation));
            TR.Y = (float)(TR.Y * Math.Sin(this.rotation) - TR.X * Math.Cos(this.rotation));

            BL.X = (float)(BL.X * Math.Cos(this.rotation) - BL.Y * Math.Sin(this.rotation));
            BL.Y = (float)(BL.Y * Math.Sin(this.rotation) - BL.X * Math.Cos(this.rotation));

            TL.X = (float)(TL.X * Math.Cos(this.rotation) - TL.Y * Math.Sin(this.rotation));
            TL.Y = (float)(TL.Y * Math.Sin(this.rotation) - TL.X * Math.Cos(this.rotation));

            TL = position;
            TR = new Vector2(position.X + size.X, position.Y);
            BL = new Vector2(position.X, position.Y + size.Y);
            BR = size;
        }
    }
}
