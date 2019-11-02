using System;
using FlappyBird.Shared.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace FlappyBird.Shared.Entities
{

    public class Player : Entity
    {
        private SpriteAnimation animation;
        private double fallingConstant = 50;
        private double travel = 0;
        private float rotation = 0.0f;
        private Vector2 origin;
        public Vector2 Position;
        public SaveData Data;
        public bool IsAlive = false;
        public bool IsLocked = false;
        public bool IsWobbling = true;
        public static Texture2D Texture;

        public override Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)(Position.X-origin.X), (int)(Position.Y-origin.Y), animation.SpriteWidth, animation.SpriteHeight);
            }
        }

        public Player(GameState state) : base(state)
        {
            animation = new SpriteAnimation(new SpriteSheet(Player.Texture, 1, 3), speed:0.1);
            animation.LoadRow(0);
            Position = new Vector2(gameState.VirtualWidth/2, gameState.VirtualHeight/2);
            origin = new Vector2(animation.SpriteWidth / 2, animation.SpriteHeight / 2);
            Data = XmlManager.Load<SaveData>("./SaveFile.xml");
        }

        public override void Draw(GameTime gameTime)
        {
            gameState.SpriteBatch.Draw(
                texture: animation.Texture,
                position: Position,
                sourceRectangle: animation.CurrentSprite,
                color: Color.White,
                rotation: rotation,
                origin: origin,
                scale: Vector2.One, 
                effects: SpriteEffects.None,
                layerDepth: 0
                );
        }

        public override void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
            double dt = gameTime.ElapsedGameTime.TotalSeconds;

            if(IsWobbling)
            {
                if(travel <= -10)
                {
                    fallingConstant = 50;
                }
                if(travel >= 10)
                {
                    fallingConstant = -50;
                }
                travel += fallingConstant * dt;

                Position.Y += (float)(fallingConstant * dt);

#if LINUX
                if(Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    IsWobbling = false;
                    IsAlive = true;
                    fallingConstant = 50;
                    travel = 0;
                }
#elif ANDROID
                if(TouchPanel.GetState().Count > 0)
                {
                    IsWobbling = false;
                    IsAlive = true;
                    fallingConstant = 50;
                    travel = 0;
                }
#endif
            }
            else if(!IsAlive)
            {
                if(!IsLocked)
                {
                    if (travel < 1000 * dt)
                    {
                        travel += (fallingConstant * dt);
                    }
                    if (MathHelper.ToDegrees(this.rotation) <= 90)
                    {
                        rotation += MathHelper.ToRadians((float)(180 * dt));
                    }
                    Position.Y += (int)Math.Round(travel);
                }
            }

            else if(IsAlive)
            {
#if LINUX
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    
                    travel = -600 * dt;
                    rotation = MathHelper.ToRadians(-45);
                    animation.CurrentSpriteIndex = 0;
                }

#elif ANDROID
                if(TouchPanel.GetState().Count > 0)
                {
                    travel = -600 * dt;
                    rotation = MathHelper.ToRadians(-45);
                    animation.CurrentSpriteIndex = 0;
                    
                }
#endif
                else
                {
                    if(travel < 1000 * dt)
                    {
                        travel += (fallingConstant * dt);
                    }
                    if(MathHelper.ToDegrees(this.rotation) <= 90 )
                    {
                        rotation += MathHelper.ToRadians((float)(180 * dt));
                    }
                }
                Position.Y += (int)Math.Round(travel);
            }

        }
    }
}
