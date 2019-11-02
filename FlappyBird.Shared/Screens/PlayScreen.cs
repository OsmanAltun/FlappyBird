using FlappyBird.Shared.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlappyBird.Shared.Screens
{
    public sealed class PlayScreen : Screen
    {
        private List<GroundTile> groundTiles = new List<GroundTile>();
        private List<Pipe> pipes = new List<Pipe>();
        private List<Background> backgrounds = new List<Background>();
        private Player player;
        private Background background;
        public bool hasStarted = false;

        public PlayScreen(GameState state) : base(state)
        {
            LoadContent();
            Init();
        }

        public override void Init()
        {
            this.player = new Player(State);
            this.player.Data.CurrentScore = 0;
            var position = new Vector2(State.VirtualWidth, State.VirtualHeight - GroundTile.Texture.Height);
            for (int i = 0; i <= State.VirtualWidth + GroundTile.Texture.Width; i += GroundTile.Texture.Width)
            {
                var g = new GroundTile(State, new Vector2(i, position.Y));
                groundTiles.Add(g);
            }

            for (int i = 0; i <= State.VirtualWidth + Background.Texture.Width; i += Background.Texture.Width)
            {
                var b = new Background(State, new Vector2(i, 0));
                backgrounds.Add(b);
            }


        }

        public override void LoadContent()
        {
            Player.Texture = State.Content.Load<Texture2D>("bird");
            Background.Texture = State.Content.Load<Texture2D>("background");
            GroundTile.Texture = State.Content.Load<Texture2D>("ground");
            Pipe.Texture = State.Content.Load<Texture2D>("pipe");
        }

        public override void Draw(GameTime gameTime)
        {
            foreach(Background b in backgrounds)
            {
                b.Draw(gameTime);
            }
            foreach (Pipe pipe in pipes)
            {
                pipe.Draw(gameTime);
            }
            if(!player.IsLocked)
            {
                FontManager.Instance.DrawFont(State, player.Data.CurrentScore.ToString(), new Vector2(50, 50), scale: 1.5f);
                FontManager.Instance.DrawFont(State, player.Data.BestScore.ToString(),
                    new Vector2(50, 75 + FontManager.Instance.MeasureString(player.Data.CurrentScore.ToString()).Y), scale:1f);
            }
            player.Draw(gameTime);
            foreach (GroundTile groundTile in groundTiles)
            {
                groundTile.Draw(gameTime);
            }

        }

        public void PlayerDied()
        {
            player.IsAlive = false;
            XmlManager.Save("./SaveFile.xml", new SaveData { BestScore = player.Data.BestScore, CurrentScore = player.Data.CurrentScore});
        }

        public override void Update(GameTime gameTime)
        {


            player.Update(gameTime);

            if(!player.IsLocked && !player.IsAlive)
            {
                foreach (GroundTile ground in groundTiles)
                {
                    if (player.IsColliding(ground))
                    {
                        player.IsLocked = true;
                        this.IsUpdating = false;
                        ScreenManager.Instance.AddScreen(new MenuScreen(State));
                    }
                }
            }

            else if (this.player.IsAlive)
            {

                // update entities
                var dt = gameTime.ElapsedGameTime.TotalSeconds;

                foreach(Background b in backgrounds)
                {
                    b.Update(gameTime);
                }
                foreach(Entity entity in pipes.Concat<Entity>(groundTiles))
                {
                    entity.Update(gameTime);
                    if (player.IsColliding(entity))
                    {
                        PlayerDied();
                    }
                }

                // check if pipe has been passed and increment score
                var passedPipes = pipes.Where<Pipe>(p => !p.IsPassed && p.Position.X+p.Rectangle.Width/2 < player.Position.X);
                if(passedPipes.Count() > 0)
                {
                    foreach(var pipe in passedPipes)
                    {
                        pipe.IsPassed = true;
                    }
                    player.Data.CurrentScore += 1;

                    if (player.Data.CurrentScore > player.Data.BestScore)
                        player.Data.BestScore = player.Data.CurrentScore;
                }

                // remove old and add new moving entities
                pipes.RemoveAll(p => p.Position.X < -Pipe.Texture.Width);
                groundTiles.RemoveAll(g => g.Position.X < -GroundTile.Texture.Width);
                backgrounds.RemoveAll(b => b.Position.X < -Background.Texture.Width);
                if (pipes.Count == 0)
                {
                    int rand = new Random().Next(500, 1250);
                    pipes.Add(new Pipe(State, new Vector2(State.VirtualWidth, rand)));
                    pipes.Add(new Pipe(State, new Vector2(State.VirtualWidth, rand - 300), true));
                }
                else if (pipes.Count <= 5 && pipes.Max(p => p.Position.X) < 384)
                {
                    int rand = new Random().Next(500, 1250);
                    pipes.Add(new Pipe(State, new Vector2(State.VirtualWidth, rand)));
                    pipes.Add(new Pipe(State, new Vector2(State.VirtualWidth, rand - 300), true));
                }
                while(groundTiles.Max<GroundTile>(g => g.Position.X) < State.VirtualWidth+GroundTile.Texture.Width)
                {
                    groundTiles.Add(new GroundTile(State, new Vector2(groundTiles.Max<GroundTile>(g => g.Position.X) + GroundTile.Texture.Width, State.VirtualHeight - GroundTile.Texture.Height)));
                }
                while (backgrounds.Max<Background>(b => b.Position.X) < State.VirtualWidth + Background.Texture.Width)
                {
                    backgrounds.Add(new Background(State, new Vector2(backgrounds.Max<Background>(b => b.Position.X) + Background.Texture.Width, 0)));
                }



            }
        }

    }
}
