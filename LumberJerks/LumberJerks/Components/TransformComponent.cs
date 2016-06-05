using System;

using Microsoft.Xna.Framework;

using Entitas;

namespace LumberJerks {
  public class TransformComponent : IComponent {
    public float X;
    public float Y;

    public Vector2 Position {
      get { return new Vector2(X, Y); }
    }
  }
}

