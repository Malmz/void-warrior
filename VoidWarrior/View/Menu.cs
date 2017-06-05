using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using VoidWarrior.Ui.Menu;

namespace VoidWarrior.View
{
    class Menu : IView
    {
        private List<IStatic> statics;
        private List<IInteractive> interactives;
        private ViewEvent viewEvent;
        public IDynamic Background { get; set; }
        public ViewEvent BackEvent { get; set; }
        private float inputDelay;

        public Menu()
        {
            statics = new List<IStatic>();
            interactives = new List<IInteractive>();
            viewEvent = ViewEvent.None;
            BackEvent = ViewEvent.None;
            inputDelay = 0;
        }

        public void AddStatic(IStatic _static)
        {
            statics.Add(_static);
        }

        public void AddInteractive(IInteractive interactive)
        {
            interactives.Add(interactive);
            if (interactives.Count == 1)
            {
                interactives[0].IsActive = true;
            }
        }

        private void MoveUp()
        {

            for (int i = 0; i < interactives.Count; i++)
            {
                if (interactives[i].IsActive == true)
                {
                    interactives[i].IsActive = false;
                    if (i > 0)
                    {
                        interactives[i - 1].IsActive = true;
                    }
                    else
                    {
                        interactives[interactives.Count - 1].IsActive = true;
                    }
                    break;
                }
            }
        }

        private void MoveDown()
        { 
            for (int i = 0; i < interactives.Count; i++)
            {
                if (interactives[i].IsActive == true)
                {
                    interactives[i].IsActive = false;
                    if (i < interactives.Count - 1)
                    {
                        interactives[i + 1].IsActive = true;
                    }
                    else
                    {
                        interactives[0].IsActive = true;
                    }
                    break;
                }
            }
        }

        public void Update(GameTime gameTime) 
        {
            if (Background != null)
            {
                Background.Update(gameTime);
            }
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
                viewEvent = interactives.First(b => b.IsActive).Event;
            }
            else if (Input.Pause && BackEvent.Event != ViewEvent.None.Event)
            {
                viewEvent = BackEvent;
            }
            else
            {
                viewEvent = ViewEvent.None;
            }

            inputDelay -= gameTime.ElapsedGameTime.Milliseconds / 1000f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Background != null)
            {
                Background.Draw(spriteBatch);
            }
            statics.ForEach(x => x.Draw(spriteBatch));
            interactives.ForEach(x => x.Draw(spriteBatch));
        }

        public ViewEvent Event
        {
            get { return viewEvent; }
        }

        public void Reset()
        {
            interactives.First(x => x.IsActive).IsActive = false;
            interactives[0].IsActive = true;
        }
    }
}
