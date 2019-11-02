using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird.Shared.Screens
{
    public abstract class Screen
    {
        public GameState State;

        public bool IsDrawing = true;
        public bool IsUpdating = true;

        public Screen(GameState state)
        {
            this.State = state;
        }
        public abstract void LoadContent();
        public abstract void Init();
        public abstract void Draw(GameTime gameTime);
        public abstract void Update(GameTime gameTime);
    }
}
