using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Shared.Animation
{
    public class SpriteAnimation
    {
        double FrameSpeed { get; set; }
        double ElapsedTime { get; set; } = 0.0;
        SpriteSheet Sheet { get; set; }

        public Rectangle[] Sprites { get; set; }
        public int CurrentSpriteIndex { get; set; } = 0;
        public Rectangle CurrentSprite { get { return Sprites[CurrentSpriteIndex]; } }

        public Texture2D Texture { get { return Sheet.Sheet; } }
        public int SpriteWidth { get { return Sheet.SpriteWidth; } }
        public int SpriteHeight { get { return Sheet.SpriteHeight; } }
        public Point SpriteSize { get { return new Point(SpriteWidth, SpriteHeight); } }


        public SpriteAnimation(SpriteSheet spritesheet, double speed=0.2)
        {
            Sheet = spritesheet;
            FrameSpeed = speed;
        }

        public void LoadRow(int index)
        {
            Sprites = Sheet.GetRow(index);
            CurrentSpriteIndex = 0;
            ElapsedTime = 0.0;
        }

        public void LoadColumn(int index)
        {
            Sprites = Sheet.GetColumn(index);
            CurrentSpriteIndex = 0;
            ElapsedTime = 0.0;
        }


        public void Update(GameTime gameTime)
        {
            ElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            if(ElapsedTime >= FrameSpeed)
            {
                CurrentSpriteIndex = (CurrentSpriteIndex + 1) % Sprites.Length;
                ElapsedTime = 0.0;
            }
        }
    }
}
