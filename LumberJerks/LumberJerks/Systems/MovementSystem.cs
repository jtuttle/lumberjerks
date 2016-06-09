using System;

using Microsoft.Xna.Framework.Input;

using Entitas;

namespace LumberJerks {
  public class MovementSystem : IExecuteSystem, ISetPool {
    Group _group;

    public void SetPool(Pool pool) {
      _group = pool.GetGroup(Matcher.AllOf(Matcher.Player, Matcher.Transform));
    }

    public void Execute() {
      foreach(Entity e in _group.GetEntities()) {
        GamePadState input = GamePad.GetState(e.player.PlayerIndex);

        float h = input.ThumbSticks.Left.X;
        float v = input.ThumbSticks.Left.Y;

        //Console.WriteLine(h + " " + v);

        e.transform.X += h;
        e.transform.Y += v;


        // consider controller input

        // apply gravity? (this may happen automatically in farseer)


      }
    }
  }
}

