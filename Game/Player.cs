using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace monogame_test.Game;

public class Player : Actor
{
    private Texture2D texture;
    private World World;

    public Player(World World)
    {
        this.World = World;
    }

    public void LoadContent(GraphicsDevice graphicsDevice)
    {
        texture = new(graphicsDevice, 1, 1);
        texture.SetData(new[] { Color.Red });
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Position, Color.Red);
    }

    private bool Colliding(Vector2 position)
    {
        return World.RectangleColliding(position, texture.Width, texture.Height);
    }

    public override void Update(float deltaTime)
    {
        float moveSpeed = 5f;
        Vector2 newPosition = Position;

        KeyboardState keyboard = Keyboard.GetState();
        if (keyboard.IsKeyDown(Keys.Right))
        {
            newPosition.X += moveSpeed * deltaTime;
            while (Colliding(newPosition))
            {
                newPosition.X -= 0.01f;
            }
        }
        if (keyboard.IsKeyDown(Keys.Left))
        {
            newPosition.X -= moveSpeed * deltaTime;
            while (Colliding(newPosition))
            {
                newPosition.X += 0.01f;
            }
        }
        if (keyboard.IsKeyDown(Keys.Up))
        {
            newPosition.Y -= moveSpeed * deltaTime;
            while (Colliding(newPosition))
            {
                newPosition.Y += 0.01f;
            }
        }
        if (keyboard.IsKeyDown(Keys.Down))
        {
            newPosition.Y += moveSpeed * deltaTime;
            while (Colliding(newPosition))
            {
                newPosition.Y -= 0.01f;
            }
        }

        Position = newPosition;
    }
}
