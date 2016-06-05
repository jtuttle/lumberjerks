using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

using Entitas;

namespace LumberJerks {
  public class LumberJerksGame : Game {
    public static readonly int SCREEN_WIDTH = 1920;
    public static readonly int SCREEN_HEIGHT = 1080;

    public static SpriteBatch SpriteBatch;

    GraphicsDeviceManager _graphics;

    Systems systems;


    // TEMP
    Texture2D tex;

    public LumberJerksGame() {
      _graphics = new GraphicsDeviceManager(this) {
        PreferredBackBufferWidth = SCREEN_WIDTH,
        PreferredBackBufferHeight = SCREEN_HEIGHT
      };

      Content.RootDirectory = "Content";
    }

    protected override void Initialize() {
            
      base.Initialize();
    }

    protected override void LoadContent() {
      SpriteBatch = new SpriteBatch(GraphicsDevice);

      tex = this.Content.Load<Texture2D>("Sprites/lumberjack");


      Pool pool = new Pool(10);

      systems = new Systems().
        Add(pool.CreateSystem<SpriteRenderSystem>());
      
      Entity entity = pool.CreateEntity();
      entity.AddTransform(0f, 0f, 0f);
      entity.AddSprite(tex, new Rectangle(0, 0, 100, 100), Vector2.Zero);
    }

    protected override void Update(GameTime gameTime) {
      #if !__IOS__ &&  !__TVOS__
      if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();
      #endif


    

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

      LumberJerksGame.SpriteBatch.Begin();
    
      systems.Execute();

      LumberJerksGame.SpriteBatch.End();
            
      base.Draw(gameTime);
    }

    private void AddPlayer(Pool pool, Vector2 position) {

    }
  }
}

