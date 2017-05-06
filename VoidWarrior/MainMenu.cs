using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VoidWarrior
{
    public enum MenuEvent
    {
        None,
        LevelSelect,
        Quit,
        L1,
        L2,
        L3,

    }
    class MainMenu
    {
        Text title;
        Button start;
        Button levelSelect;
        Button quit;

        public MainMenu(ResourcePool res)
        {
            SpriteFont menuFont = res.GetFont("Guardians");
            this.title = new Text("Void Warrior", res.GetFont("Earth Orbiter"), new Vector2(Globals.SCREEN_WIDTH / 2, 100), Color.Yellow, Align.Center);
            this.start = new Button("Start", menuFont, new Vector2(Globals.SCREEN_WIDTH / 2, 400), Color.White, Color.Yellow, Align.Center);
            this.levelSelect = new Button("Level Select", menuFont, new Vector2(Globals.SCREEN_WIDTH / 2, 500), Color.White, Color.Yellow, Align.Center);
            this.quit = new Button("Quit", menuFont, new Vector2(Globals.SCREEN_WIDTH / 2, 600), Color.White, Color.Yellow, Align.Center);

        }

        public MenuEvent Update()
        {
            start.Hover(Events.MousePosition());
            levelSelect.Hover(Events.MousePosition());
            quit.Hover(Events.MousePosition());

            if (Events.MouseReleased(MouseButtons.Left))
            {
                if (start.Contains(Events.MousePosition()))
                {
                    return MenuEvent.L1;
                }
                else if (levelSelect.Contains(Events.MousePosition()))
                {
                    return MenuEvent.LevelSelect;
                }
                else if (quit.Contains(Events.MousePosition()))
                {
                    return MenuEvent.Quit;
                }
            }
            return MenuEvent.None;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            title.Draw(spriteBatch);
            start.Draw(spriteBatch);
            levelSelect.Draw(spriteBatch);
            quit.Draw(spriteBatch);
        }
    }
}
