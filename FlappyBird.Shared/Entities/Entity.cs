using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird.Shared.Entities
{
    public abstract class Entity
    {
        protected GameState gameState { get; }
        public abstract Rectangle Rectangle { get; }
        public Entity(GameState gameState)
        {
            this.gameState = gameState;
        }

        public abstract void Draw(GameTime gameTime);

        public abstract void Update(GameTime gameTime);

        public bool IsColliding(Entity entity)
        {
            if (IsCollidingLeft(entity))
                return true;
            else if (IsCollidingRight(entity))
                return true;
            else if (IsCollidingTop(entity))
                return true;
            else if (IsCollidingBottom(entity))
                return true;
            else
                return false;
        }

        public bool IsCollidingLeft(Entity entity)
        {
            return this.Rectangle.Right > entity.Rectangle.Left &&
              this.Rectangle.Left < entity.Rectangle.Left &&
              this.Rectangle.Bottom > entity.Rectangle.Top &&
              this.Rectangle.Top < entity.Rectangle.Bottom;
        }

        public bool IsCollidingRight(Entity entity)
        {
            return this.Rectangle.Left < entity.Rectangle.Right &&
              this.Rectangle.Right > entity.Rectangle.Right &&
              this.Rectangle.Bottom > entity.Rectangle.Top &&
              this.Rectangle.Top < entity.Rectangle.Bottom;
        }

        public bool IsCollidingTop(Entity entity)
        {
            return this.Rectangle.Bottom > entity.Rectangle.Top &&
              this.Rectangle.Top < entity.Rectangle.Top &&
              this.Rectangle.Right > entity.Rectangle.Left &&
              this.Rectangle.Left < entity.Rectangle.Right;
        }

        public bool IsCollidingBottom(Entity entity)
        {
            return this.Rectangle.Top < entity.Rectangle.Bottom &&
              this.Rectangle.Bottom > entity.Rectangle.Bottom &&
              this.Rectangle.Right > entity.Rectangle.Left &&
              this.Rectangle.Left < entity.Rectangle.Right;
        }
    }
}
