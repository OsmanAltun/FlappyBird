using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird.Shared.Entities
{
    class Score : Entity
    {
        public static Texture2D Texture;
        public Vector2 Position;
        public Vector2 Origin;
        private SaveData data;

        public override Rectangle Rectangle { get
            {
                return new Rectangle((int)(Position.X - Origin.X), (int)(Position.Y - Origin.Y), Texture.Width, Texture.Height);
            }
        }

        public Score(GameState gameState) : base(gameState)
        {
            Position = new Vector2(base.gameState.VirtualWidth/2, base.gameState.VirtualHeight/2);
            Origin = new Vector2(Texture.Width/2, Texture.Height/2);
            data = XmlManager.Load<SaveData>("SaveFile.xml");
        }

        public override void Draw(GameTime gameTime)
        {
            gameState.SpriteBatch.Draw(
                texture: Texture,
                position: Position,
                sourceRectangle: new Rectangle(0, 0, Texture.Width, Texture.Height),
                color: Color.White,
                rotation: 0,
                origin: Origin,
                scale: Vector2.One,
                effects: SpriteEffects.None,
                layerDepth: 0
            );

            var len = FontManager.Instance.MeasureString(data.CurrentScore.ToString()) * 1.5f;
            FontManager.Instance.DrawFont(gameState, data.CurrentScore.ToString(), new Vector2(Position.X - len.X/2, Position.Y - Texture.Height*(7f/32f)), scale:1.5f);

            len = FontManager.Instance.MeasureString(data.BestScore.ToString()) * 1.5f;
            FontManager.Instance.DrawFont(gameState, data.BestScore.ToString(), new Vector2(Position.X - len.X/2, Position.Y + Texture.Height*(3f/16f)), scale:1.5f);
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
