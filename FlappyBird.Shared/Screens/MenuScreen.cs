using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlappyBird.Shared.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace FlappyBird.Shared.Screens
{
    public sealed class MenuScreen : Screen
    {
        private Score score;
        private bool hasBeenPressed = false;

        public MenuScreen(GameState state) : base(state)
        {
            LoadContent();
            Init();
        }

        public override void Draw(GameTime gameTime)
        {
            score.Draw(gameTime);

        }

        public override void Init()
        {
            score = new Score(State);
        }

        public override void LoadContent()
        {
            Score.Texture = State.Content.Load<Texture2D>("score");



        }

        public override void Update(GameTime gameTime)
        {
            score.Update(gameTime);
#if ANDROID
            foreach(var touch in TouchPanel.GetState())
            {
                if(touch.State == TouchLocationState.Pressed)
                {
                    hasBeenPressed = true;
                }
                if(touch.State == TouchLocationState.Released && hasBeenPressed)
                {
                    ScreenManager.Instance.Restart(State);
                }
            }
#elif LINUX
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                hasBeenPressed = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space) && hasBeenPressed)
            {
                ScreenManager.Instance.Restart(State);
            }
#endif
        }
    }
}
