using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using monogame_test.Game;

namespace monogame_test;

public class Camera
{
    public Vector2 position = Vector2.Zero;
    public Matrix viewMatrix = Matrix.Identity;
    public float PPU = 48f;
    public Actor follow;

    public GraphicsDevice graphicsDevice;
    
    public Camera(GraphicsDevice graphicsDevice)
    {
        this.graphicsDevice = graphicsDevice;
    }

    public void updateViewMatrix()
    {
        float screenWidth = graphicsDevice.Viewport.Width;
        float screenHeight = graphicsDevice.Viewport.Height;

        Matrix centerScreenMatrix = Matrix.CreateTranslation(screenWidth / 2, screenHeight / 2, 0);
        Matrix scalingMatrix = Matrix.CreateScale(PPU, PPU, 1);
        Matrix cameraPositionMatrix = Matrix.CreateTranslation(-position.X, -position.Y, 0);

        viewMatrix = cameraPositionMatrix * scalingMatrix * centerScreenMatrix;
    }

    public void Update()
    {
        if (follow != null)
            position = follow.Position;
    }

    public (Vector2, Vector2) getLocalViewBox()
    {
        float widthUnits = graphicsDevice.Viewport.Width / PPU;
        float heightUnits = graphicsDevice.Viewport.Height / PPU;

        var topLeft = new Vector2(position.X - widthUnits / 2f, position.Y - heightUnits / 2f);
        var bottomRight = new Vector2(position.X + widthUnits / 2f, position.Y + heightUnits / 2f);

        return (topLeft, bottomRight);
    }
}
