using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using monogame_test.Game;

namespace monogame_test;

public class Tile
{
    public TileType Type { get; set; }
    public (int, int) Position { get; set; }

    public Tile(TileType Type, (int, int) Position)
    {
        this.Type = Type;
        this.Position = Position;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Type.Texture,
            new Rectangle(Position.Item1, Position.Item2, 1, 1),
            new Rectangle(0, 0, Type.Texture.Width, Type.Texture.Height),
            Color.White
        );
    }
}
