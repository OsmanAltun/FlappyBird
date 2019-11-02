using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird.Shared
{
    public sealed class FontManager
    {
        private static readonly Lazy<FontManager> lazy = new Lazy<FontManager>(() => new FontManager());
        public static FontManager Instance { get { return lazy.Value; } }

        private Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        private FontManager()
        {
        }

        public void AddFont(string name, SpriteFont font)
        {
            Instance.fonts.Add(name, font);
        }

        public void RemoveFont(string name)
        {
            Instance.fonts.Remove(name);
        }

        public void DrawFont(GameState state, string value, Vector2 position, float scale = 1f, string fontName = "Default")
        {
            state.SpriteBatch.DrawString(
                spriteFont: Instance.fonts[fontName],
                text: value,
                position: position,
                color: Color.White,
                rotation: 0,
                origin: Vector2.Zero,
                scale: scale,
                effects: SpriteEffects.None,
                layerDepth: 0f);
        }
        public void DrawFont(GameState state, string value, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, string fontName = "Default")
        {
            state.SpriteBatch.DrawString(
                spriteFont: fonts[fontName],
                text: value,
                position: position,
                color: color,
                rotation: rotation,
                origin: origin,
                scale: scale,
                effects: effects,
                layerDepth: 0f);
        }

        public Vector2 MeasureString(string value, string fontName = "Default")
        {
            return Instance.fonts[fontName].MeasureString(value);
        }
    }
}
