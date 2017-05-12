using System;

using Microsoft.Xna.Framework;

using Entitas;

namespace LumberJerks {
  public class SpriteRenderSystem : IExecuteSystem, ISetPool {
    Group _group;

    public void SetPool(Pool pool) {
      _group = pool.GetGroup(Matcher.AllOf(Matcher.Sprite, Matcher.Transform));
    }

    public void Execute() {
      foreach(Entity e in _group.GetEntities()) {

        Vector2 pos = new Vector2();
        pos.X = e.transform.Position.X - (e.sprite.Texture.Width / 2);
        pos.Y = e.transform.Position.Y - (e.sprite.Texture.Height / 2);

        LumberJerksGame.SpriteBatch.Draw(
          e.sprite.Texture,
          pos - e.sprite.AnchorOffset, 
          e.sprite.Frame, 
          Color.White);
      }
    }
  }
}

