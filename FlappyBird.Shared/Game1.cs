#region Using Statements
using System;
using FlappyBird.Shared.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

#endregion

namespace FlappyBird.Shared
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        RenderTarget2D renderTarget;
        int VirtualWidth = 1080;
        int VirtualHeight = 1920;

        public GameState State
        {
            get
            {
                return new GameState(spriteBatch, graphics, Content, VirtualWidth, VirtualHeight);
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
#if LINUX

            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 400;
#endif
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            TouchPanel.DisplayHeight = VirtualHeight;
            TouchPanel.DisplayWidth = VirtualWidth;
            TouchPanel.EnableMouseTouchPoint = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            PresentationParameters pp = graphics.GraphicsDevice.PresentationParameters;
            renderTarget = new RenderTarget2D(graphics.GraphicsDevice, VirtualWidth, VirtualHeight, false,
                SurfaceFormat.Color, DepthFormat.None, pp.MultiSampleCount, RenderTargetUsage.DiscardContents);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ScreenManager.Instance.AddScreen(new PlayScreen(State));
            FontManager.Instance.AddFont("Default", Content.Load<SpriteFont>("FB"));
            //TODO: use this.Content to load your game content here 
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
#endif
            // TODO: Add your update logic here			
            ScreenManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            var outputAspectRatio =  (float)graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;
            var preferredAspectRatio = (float)VirtualWidth / (float)VirtualHeight;
            var position = Vector2.Zero;
            var origin = Vector2.Zero;
            float scale = 1f;

            if (outputAspectRatio > preferredAspectRatio)
            {
                scale = 1f / ((float)VirtualHeight / (float)graphics.GraphicsDevice.Viewport.Height);
                position.X = graphics.GraphicsDevice.Viewport.Width / 2;
                origin.X = VirtualWidth / 2;
            }
            else
            {
                scale = 1f / ((float)VirtualWidth / (float)graphics.GraphicsDevice.Viewport.Width);
                position.Y = graphics.GraphicsDevice.Viewport.Height / 2;
                origin.Y = VirtualHeight / 2;
            }



            graphics.GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            ScreenManager.Instance.Draw(gameTime);
            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin();
            spriteBatch.Draw(renderTarget, position, null, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
