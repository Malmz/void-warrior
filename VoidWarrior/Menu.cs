using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace VoidWarrior
{
    public struct MenuEvent
    {
        public enum EventType {
            None,
            Quit,
            ChangeView,
        }
        private EventType _event;
        private int view;
        public int View { get { return view; } }
        public EventType Event { get { return _event; } }

        private MenuEvent(EventType _event, int view)
        {
            this._event = _event;
            this.view = view;
        }

        public static MenuEvent None { get { return new MenuEvent(EventType.None, -1); } }
        public static MenuEvent Quit { get { return new MenuEvent(EventType.Quit, -1); } }
        public static MenuEvent ChangeView(int view)
        {
            return new MenuEvent(EventType.ChangeView, view);
        }

    }
    class Menu
    {
        protected List<Text> texts;
        protected List<Button> buttons;

        public Menu()
        {
            texts = new List<Text>();
            buttons = new List<Button>();
        }

        public void AddText(string text, SpriteFont font, float X, float Y, Color color, Align align = Align.Left)
        {
            texts.Add(new Text(text, font, X, Y, color, align));
        }

        public void AddText(string text, SpriteFont font, Vector2 position, Color color, Align align = Align.Left)
        {
            texts.Add(new Text(text, font, position, color, align));
        }

        public void AddText(Text text)
        {
            texts.Add(text);
        }

        public void AddButton(string text, SpriteFont font, float X, float Y, Color color, Color activeColor, MenuEvent menuEvent, Align align = Align.Left)
        {
            buttons.Add(new Button(text, font, X, Y, color, activeColor, menuEvent, align));
        }

        public void AddButton(string text, SpriteFont font, Vector2 position, Color color, Color activeColor, MenuEvent menuEvent, Align align = Align.Left)
        {
            buttons.Add(new Button(text, font, position, color, activeColor, menuEvent, align));
        }

        public void AddButton(Button button)
        {
            buttons.Add(button);
        }


        public virtual MenuEvent Update() 
        {
            buttons.ForEach(x => x.Update(Events.MousePosition));
            if (Events.MouseReleased(MouseButtons.Left))
            {
                foreach (var button in buttons)
                {
                    if (button.IsActive)
                    {
                        return button.Event;
                    }
                }
            }
            return MenuEvent.None;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            texts.ForEach(x => x.Draw(spriteBatch));
            buttons.ForEach(x => x.Draw(spriteBatch));
        }
    }
}
