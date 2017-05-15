using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace VoidWarrior.View
{
    struct ViewEvent
    {
        enum EventType
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
        private ViewEvent(EventType eventType = EventType.None, IView view = null)
        {
            this.eventType = eventType;
            this.view = view;
        }
    }

    interface IView
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        List<ViewEvent> Events { get; }
    }
}
