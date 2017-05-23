
using VoidWarrior.View;

namespace VoidWarrior.Ui.Menu
{
    interface IInteractive : IStatic
    {
        bool IsActive { get; set; }
        ViewEvent Event { get; }
    }
}
