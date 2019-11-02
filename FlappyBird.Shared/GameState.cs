using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird.Shared
{

    public class GameState
    {
        public GraphicsDeviceManager Graphics { get; }
        public ContentManager Content { get; }
        public SpriteBatch SpriteBatch { get; }
        public int VirtualWidth { get; }
        public int VirtualHeight { get; }

        public GameState(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, ContentManager content, int virtualWidth, int virtualHeight)
        {
            this.Graphics = graphics;
            this.Content = content;
            this.SpriteBatch = spriteBatch;
            this.VirtualWidth = virtualWidth;
            this.VirtualHeight = virtualHeight;
        }


    }
}
