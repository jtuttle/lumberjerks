using System;

using Microsoft.Xna.Framework;

using Entitas;

namespace LumberJerks {
  public class SpriteRenderSystem : IExecuteSystem, ISetPool {
    Group _group;

    public void SetPool(Pool pool) {
      _group = pool.GetGroup(Matcher.AllOf(Matcher.Transform));
    }

    public void Execute() {


      foreach(Entity e in _group.GetEntities()) {
        LumberJerksGame.SpriteBatch.Draw(
          e.sprite.Texture,
          e.transform.Position - e.sprite.AnchorOffset, 
          e.sprite.Frame, 
          Color.White);
          //lightableComponent.LightColor * alpha);
      }
    }
  }
}

