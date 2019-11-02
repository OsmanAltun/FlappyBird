using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Shared.Animation
{
    public class SpriteSheet
    {
        public Texture2D Sheet { get; set; }
        int Rows { get; set; }
        int Columns { get; set; }

        public int SpriteWidth { get { return Sheet.Width / Columns; } }
        public int SpriteHeight { get { return Sheet.Height / Rows; } }
        public Point SpriteSize { get { return new Point(SpriteWidth, SpriteHeight); } }

        public SpriteSheet(Texture2D spritesheet, int rows, int columns)
        {
            Sheet = spritesheet;
            Rows = rows;
            Columns = columns;
        }

        public Rectangle[] GetColumn(int columnIndex)
        {
            List<Rectangle> recs = new List<Rectangle>();

            for (int i = 0; i < Rows; i++)
            {
                recs.Add(new Rectangle(new Point(columnIndex*SpriteWidth, i*SpriteHeight), SpriteSize));
            }

            return recs.ToArray();
        }

        public Rectangle[] GetRow(int rowIndex)
        {
            List<Rectangle> recs = new List<Rectangle>();

            for (int i = 0; i < Columns; i++)
            {
                recs.Add(new Rectangle(new Point(i * SpriteWidth, rowIndex * SpriteHeight), SpriteSize));
            }

            return recs.ToArray();
        }
    }
}
