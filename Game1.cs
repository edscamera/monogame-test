using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using monogame_test.Game;
using System;

namespace monogame_test;

public class Game1 : Microsoft.Xna.Framework.Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private World world = new();
    private Player player;
    private Camera camera;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        player = new Player(world);
        player.Position = new Vector2(2, 2);
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        camera = new Camera(GraphicsDevice);

        player.LoadContent(GraphicsDevice);

        // Load Tile textures
        var tileTypeFields = typeof(TileTypes).GetFields();
        foreach (var tileTypeField in tileTypeFields)
        {
            TileType tileType = (TileType)tileTypeField.GetValue(null);
            if (tileType == null) continue;
            tileType.LoadContent(GraphicsDevice);
        }

        camera.follow = player;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        camera.updateViewMatrix();
        player.Update(deltaTime);
        camera.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        _spriteBatch.Begin(transformMatrix: camera.viewMatrix, samplerState: SamplerState.PointClamp);

        (Vector2, Vector2) cameraBounds = camera.getLocalViewBox();
        cameraBounds.Item1 = new Vector2((int)Math.Floor(cameraBounds.Item1.X) - 1, (int)Math.Floor(cameraBounds.Item1.Y) - 1);
        cameraBounds.Item2 = new Vector2((int)Math.Ceiling(cameraBounds.Item2.X) + 1, (int)Math.Ceiling(cameraBounds.Item2.Y) + 1);
        world.Draw(cameraBounds, _spriteBatch);

        player.Draw(_spriteBatch);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
