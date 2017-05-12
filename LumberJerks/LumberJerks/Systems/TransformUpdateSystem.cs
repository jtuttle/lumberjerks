using System;

using Entitas;

using FarseerPhysics;

// This system just acts as an intermediary between the physics engine and the
// renderer by copying the physics body transform over to the component so that 
// it renders in the correct place. Hopefully this is temporary.
namespace LumberJerks {
  public class TransformUpdateSystem : IExecuteSystem, ISetPool {
    Group _group;

    public void SetPool(Pool pool) {
      _group = pool.GetGroup(Matcher.AllOf(Matcher.Transform, Matcher.PhysicsBody));
    }

    public void Execute() {
      foreach(Entity e in _group.GetEntities()) {
        e.transform.X = ConvertUnits.ToDisplayUnits(e.physicsBody.Body.Position.X);
        e.transform.Y = ConvertUnits.ToDisplayUnits(e.physicsBody.Body.Position.Y);
      }
    }
  }
}

