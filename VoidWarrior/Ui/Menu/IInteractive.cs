using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VoidWarrior.Ui.Menu
{
    interface IInteractive : IStatic
    {
        bool IsActive { get; set; }
        ViewEvent Event { get; }
    }
}
