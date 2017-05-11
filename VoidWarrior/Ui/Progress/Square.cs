
using Microsoft.Xna.Framework;

namespace VoidWarrior.Ui.Progress
{
    class Square
    {
        private Color color;
        private Rectangle top, left, bottom1, bottom2, right;

        public Square(int X, int Y, int W, int H, int Thickness, Color color)
        {
            this.color = color;
            top = new Rectangle(X, Y, W, Thickness);
            bottom1 = new Rectangle(X, Y + H - Thickness, W / 2, Thickness);

        }
    }
}
