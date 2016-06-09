using System;

using Entitas;

namespace LumberJerks {
  public class PhysicsDebugDrawSystem : IExecuteSystem, ISetPool {
    Group _group;

    public void SetPool(Pool pool) {
      _group = pool.GetGroup(Matcher.AllOf(Matcher.PhysicsBody));
    }

    public void Execute() {
      foreach(Entity e in _group.GetEntities()) {


      }
    }
  }
}

