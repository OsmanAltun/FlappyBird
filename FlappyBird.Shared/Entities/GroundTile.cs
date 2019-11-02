using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird.Shared.Entities
{
    public class GroundTile : Entity
    {
        public static Texture2D Texture { get; set; }
        public Vector2 Position;
        private int speed = 250;
        public override Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }

        public GroundTile(GameState state, Vector2 pos) : base(state)
        {
            Position = pos;
        }

        public override void Draw(GameTime gameTime)
        {
            gameState.SpriteBatch.Draw(
                texture: Texture,
                position: Position,
                sourceRectangle: new Rectangle(0, 0, Texture.Width, Texture.Height),
                color: Color.White,
                rotation: 0,
                origin: new Vector2(Texture.Width, 0),
                scale: Vector2.One,
                effects: SpriteEffects.None,
                layerDepth: 0
            );

        }

        public override void Update(GameTime gameTime)
        {
            Position.X -= (float)Math.Floor(speed * gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}
