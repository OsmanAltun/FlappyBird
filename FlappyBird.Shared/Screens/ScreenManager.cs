using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird.Shared.Screens
{

    public sealed class ScreenManager
    {
        private List<Screen> screens = new List<Screen>();
        private static readonly Lazy<ScreenManager> lazy = new Lazy<ScreenManager>(() => new ScreenManager());
        public static ScreenManager Instance { get { return lazy.Value; } }

        private ScreenManager()
        {

        }

        public void AddScreen(Screen s)
        {
            screens.Add(s);
        }

        public void PopScreen()
        {
            screens.RemoveAt(screens.Count - 1);
        }

        public void Restart(GameState state)
        {
            screens = new List<Screen>();
            screens.Add(new PlayScreen(state));
        }

        public void Update(GameTime dt)
        {
            for (int i = 0; i < this.screens.Count; i++)
            {
                if(this.screens[i].IsUpdating)
                {
                    this.screens[i].Update(dt);
                }
            }
        }

        public void Draw(GameTime dt)
        {
            for (int i = 0; i < this.screens.Count; i++)
            {
                if (this.screens[i].IsDrawing)
                {
                    this.screens[i].Draw(dt);
                }
            }
        }
    }
}
