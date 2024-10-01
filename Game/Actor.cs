using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogame_test.Game;
   
public class Actor
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public virtual void Update(float deltaTime) { }
    public virtual void Draw(SpriteBatch spriteBatch) { }
}
