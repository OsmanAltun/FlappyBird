using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird.Shared.Entities
{
    public class Pipe : Entity
    {
        public static Texture2D Texture { get; set; }
        public Vector2 Position;
        private SpriteEffects effect = SpriteEffects.None;
        private Vector2 origin = Vector2.Zero;
        private int speed = 250;
        public bool IsPassed = false;
        public override Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)(Position.X - origin.X), (int)(Position.Y - origin.Y), Texture.Width, Texture.Height);
            }
        }

        public Pipe(GameState state, Vector2 pos, bool verticalFlip = false) : base(state)
        {
            Position = pos;
            if(verticalFlip)
            {
                this.effect = SpriteEffects.FlipVertically;
                this.origin = new Vector2(0, Pipe.Texture.Height);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            gameState.SpriteBatch.Draw(
                texture: Texture,
                position: Position,
                sourceRectangle: new Rectangle(0, 0, Texture.Width, Texture.Height),
                color: Color.CornflowerBlue,
                rotation: 0,
                origin: this.origin,
                scale: Vector2.One,
                effects: this.effect,
                layerDepth: 0
            );

        }

        public override void Update(GameTime gameTime)
        {
            Position.X -= (float)Math.Floor(speed * gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}
