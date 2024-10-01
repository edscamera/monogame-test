using Microsoft.Xna.Framework;

namespace monogame_test.Game;

public class TileTypes
{
    public static readonly TileType Air = new TileType(Solid: false, Colors: [Color.White]);
    public static readonly TileType Solid = new TileType(Solid: true, Colors: [Color.Black]);
}
