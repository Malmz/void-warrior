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
        private List<ViewEvent> viewEvents;
        private float inputDelay;

        public Menu()
        {
            statics = new List<IStatic>();
            interactives = new List<IInteractive>();
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
                viewEvents.Add(interactives.First(b => b.IsActive).Event);
            }

            inputDelay -= gameTime.ElapsedGameTime.Milliseconds / 1000f;
            viewEvents.Add(ViewEvent.None);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            statics.ForEach(x => x.Draw(spriteBatch));
            interactives.ForEach(x => x.Draw(spriteBatch));
        }

        public List<ViewEvent> Events
        {
            get
            {
                var tmp = new List<ViewEvent>(viewEvents.Count);
                viewEvents.ForEach(x => tmp.Add(x));
                viewEvents.Clear();
                return tmp;
            }
        }
    }
}
