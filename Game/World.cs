using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using monogame_test.Game;
using System;
using System.Collections.Generic;

namespace monogame_test;

public class World
{
    private Dictionary<(int, int), Tile> Map = new();

    private FastNoiseLite noise = new FastNoiseLite();
    
    public World()
    {
        noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
    }

    public void Draw((Vector2, Vector2) borders, SpriteBatch spriteBatch)
    {
        for (float y = borders.Item1.Y; y < borders.Item2.Y; y++)
        {
            for (float x = borders.Item1.X; x < borders.Item2.X; x++)
            {
                (int, int) position = ((int)x, (int)y);
                Map.TryGetValue(position, out Tile tile);
                if (tile == null)
                {
                    Map.Add(position, Generate(position));
                    Map.TryGetValue(position, out tile);
                }

                tile.Draw(spriteBatch);
            }
        }
    }

    private Tile Generate((int, int) position)
    {
        float zoom = 12f * 4f;
        float x = position.Item1;
        float y = position.Item2;

        float value = noise.GetNoise(x * zoom, y * zoom);
        for(int i = 2; i < 5; i++)
        {
            float pow = MathF.Pow(2, i);
            value += noise.GetNoise(x * zoom * (1 / pow), y * zoom * (1 / pow)) * MathF.Pow(2, i - 1);
        }

        value = (Math.Clamp(value, -1, 1) + 1) / 2;

        TileType tileType = value > 0.99 ? TileTypes.Air : TileTypes.Solid;
        return new Tile(tileType, position);
    }

    public bool Colliding(Vector2 position)
    {
        int x = (int)Math.Floor(position.X);
        int y = (int)Math.Floor(position.Y);
        Map.TryGetValue((x, y), out var tile);

        if (tile == null) return false;
        return tile.Type.Solid;
    }

    public bool RectangleColliding(Vector2 position, float width, float height)
    {
        return Colliding(position + new Vector2(0.01f, 0.01f)) ||
            Colliding(position + new Vector2(width - 0.01f, height - 0.01f)) ||
            Colliding(position + new Vector2(width - 0.01f, 0.01f)) ||
            Colliding(position + new Vector2(0.01f, height - 0.01f));
    }
}
