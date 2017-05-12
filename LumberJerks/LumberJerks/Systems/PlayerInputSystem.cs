using System;

using Entitas;

namespace LumberJerks {
  public class PlayerInputSystem : IExecuteSystem, ISetPool {
    Group _group;

    public void SetPool(Pool pool) {
      _group = pool.GetGroup(Matcher.AllOf(Matcher.PlayerInput));
    }

    public void Execute() {
      foreach(Entity e in _group.GetEntities()) {

      }
    }
  }
}

