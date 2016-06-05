using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Entitas;

namespace LumberJerks {
  public class SpriteComponent : IComponent {
    public Texture2D Texture;
    public Rectangle Frame;
    public Vector2 Anchor;

    // Assumes that all frames are the same size, which is fine for now.
    public Vector2 AnchorOffset {
      get {
        return new Vector2(Anchor.X * Frame.Width, Anchor.Y * Frame.Height);
      }
    }
  }
}

