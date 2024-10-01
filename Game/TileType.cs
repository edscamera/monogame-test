using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace monogame_test.Game
{
    public class TileType
    {
        public bool Solid { get; }  // Property for whether the tile is solid
        private Color[] Colors;     // Array of colors associated with the tile
        public Texture2D Texture { get; protected set; }  // The texture associated with the tile

        // Constructor with optional parameters
        public TileType(bool Solid = false, Color[] Colors = null)
        {
            this.Solid = Solid;

            // If no colors are provided, use the default color array
            this.Colors = Colors ?? [
                Color.DeepPink,
                Color.Black,
                Color.Black,
                Color.DeepPink
            ];
        }

        public Texture2D LoadContent(GraphicsDevice graphicsDevice)
        {
            int size = (int)Math.Sqrt(Colors.Length);
            Texture = new Texture2D(graphicsDevice, size, size);
            Texture.SetData(Colors);
            return Texture;
        }
    }
}
