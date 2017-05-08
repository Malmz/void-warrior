using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace VoidWarrior
{
    public struct MenuEvent
    {
        public enum EventType
        {
            None,
            Back,
            ChangeView,
        }
        private EventType _event;
        public enum ViewType
        {
            None,
            MainMenu,
            LevelSelect,
            Level,
        }
        private ViewType view;
        private string levelName;
        public ViewType View { get { return view; } }
        public EventType Event { get { return _event; } }
        public string LevelName { get { return levelName; } }

        private MenuEvent(EventType _event = EventType.None, ViewType view = ViewType.None, string levelName = "")
        {
            this._event = _event;
            this.view = view;
            this.levelName = levelName;
        }

        public static MenuEvent None { get { return new MenuEvent(); } }
        public static MenuEvent Back { get { return new MenuEvent(EventType.Back); } }
        public static MenuEvent ChangeView(ViewType view)
        {
            return new MenuEvent(EventType.ChangeView, view);
        }
        public static MenuEvent ChangeToLevel(string levelName)
        {
            return new MenuEvent(EventType.ChangeView, ViewType.Level, levelName);
        }

    }
    class Menu
    {
        protected List<Text> texts;
        protected List<Button> buttons;
        private float inputDelay;

        public Menu()
        {
            texts = new List<Text>();
            buttons = new List<Button>();
            inputDelay = 0;
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
            if (buttons.Count == 1)
            {
                buttons[0].IsActive = true;
            }
        }

        public void AddButton(string text, SpriteFont font, Vector2 position, Color color, Color activeColor, MenuEvent menuEvent, Align align = Align.Left)
        {
            buttons.Add(new Button(text, font, position, color, activeColor, menuEvent, align));
            if (buttons.Count == 1)
            {
                buttons[0].IsActive = true;
            }
        }

        public void AddButton(Button button)
        {
            buttons.Add(button);
            if (buttons.Count == 1)
            {
                buttons[0].IsActive = true;
            }
        }

        private void MoveUp()
        {

            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].IsActive == true)
                {
                    buttons[i].IsActive = false;
                    if (i > 0)
                    {
                        buttons[i - 1].IsActive = true;
                    }
                    else
                    {
                        buttons[buttons.Count - 1].IsActive = true;
                    }
                    break;
                }
            }
        }

        private void MoveDown()
        { 
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].IsActive == true)
                {
                    buttons[i].IsActive = false;
                    if (i < buttons.Count - 1)
                    {
                        buttons[i + 1].IsActive = true;
                    }
                    else
                    {
                        buttons[0].IsActive = true;
                    }
                    break;
                }
            }
        }

        public virtual MenuEvent Update(GameTime gameTime) 
        {
            /*
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
            */
            var temp = Input.Joystick.Y;
            if (temp < 0 && inputDelay < 0)
            {
                MoveUp();
                inputDelay = 0.15f;
            }
            else if (temp > 0 && inputDelay < 0)
            {
                MoveDown();
                inputDelay = 0.15f;
            }

            if (Input.Fire)
            {
                return buttons.First(b => b.IsActive).Event;
            }

            inputDelay -= gameTime.ElapsedGameTime.Milliseconds / 1000f;
            return MenuEvent.None;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            texts.ForEach(x => x.Draw(spriteBatch));
            buttons.ForEach(x => x.Draw(spriteBatch));
        }
    }
}
