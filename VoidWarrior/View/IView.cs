using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace VoidWarrior.View
{
    public struct ViewEvent
    {
        public enum EventType
        {
            None,
            Back,
            ChangeView,
        }
        private EventType eventType;
        private IView view;
        public static ViewEvent None { get { return new ViewEvent(); } }
        public static ViewEvent Back { get { return new ViewEvent(EventType.Back); } }
        public static ViewEvent ChangeView(IView view) { return new ViewEvent(EventType.ChangeView, view); }

        public EventType Event { get { return eventType; } }
        public IView View { get { return view; } }

        private ViewEvent(EventType eventType = EventType.None, IView view = null)
        {
            this.eventType = eventType;
            this.view = view;
        }
    }

    public interface IView
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        ViewEvent Event { get; }
        void Reset();
    }
}
