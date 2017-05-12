using System;

using Microsoft.Xna.Framework;
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
        KeyboardState keyboardState = Keyboard.GetState();

        float horizontal = 0f;
        //float vertical = 0f;
        float speed = 2; // TODO: stick this in a component

        /*
        if(keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up)) {
          vertical -= speed;
        }

        if(keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down)) {
          vertical += speed;
        }
        */

        if(keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left)) {
          horizontal -= speed;
        }

        if(keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right)) {
          horizontal += speed;
        }

        float vertical = e.physicsBody.Body.LinearVelocity.Y;
        e.physicsBody.Body.LinearVelocity = new Vector2(horizontal * speed, vertical);

        /*
        GamePadState input = GamePad.GetState(e.player.PlayerIndex);

        float h = input.ThumbSticks.Left.X;
        float v = input.ThumbSticks.Left.Y;

        //Console.WriteLine(h + " " + v);

        e.transform.X += h;
        e.transform.Y += v;
        */

        // consider controller input

        // apply gravity? (this may happen automatically in farseer)


      }
    }
  }
}

