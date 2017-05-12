using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

using Entitas;

using FarseerPhysics;
using FarseerPhysics.DebugViews;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Factories;

namespace LumberJerks {
  public class LumberJerksGame : Game {
    public static readonly int SCREEN_WIDTH = 1920;
    public static readonly int SCREEN_HEIGHT = 1080;

    public static SpriteBatch SpriteBatch;

    GraphicsDeviceManager _graphics;

    Systems _systems;

    World _physicsWorld;
    DebugViewXNA _debugView;

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

      _physicsWorld = new World(Vector2.UnitY * 5.0f);

      _debugView = new DebugViewXNA(_physicsWorld);
      _debugView.LoadContent(GraphicsDevice, Content);

      Texture2D tex = this.Content.Load<Texture2D>("Sprites/chr_manA_idleA");

      Pool pool = new Pool(10);

      _systems = new Systems().
        Add(pool.CreateSystem<PlayerInputSystem>()).
        Add(pool.CreateSystem<MovementSystem>()).
        Add(pool.CreateSystem<TransformUpdateSystem>()).
        Add(pool.CreateSystem<SpriteRenderSystem>());

      Vector2 playerPos = new Vector2(300f, 300f);
      Entity player = pool.CreateEntity();
      player.AddPlayer(PlayerIndex.One);
      player.AddTransform(playerPos.X, playerPos.Y);
      player.AddSprite(tex, new Rectangle(0, 0, 128, 128), Vector2.Zero);
      Body playerBody  = BodyFactory.CreateRectangle(_physicsWorld,
                                                     ConvertUnits.ToSimUnits(128),
                                                     ConvertUnits.ToSimUnits(128),
                                                     1,
                                                     ConvertUnits.ToSimUnits(playerPos));
      playerBody.BodyType = BodyType.Dynamic;
      player.AddPhysicsBody(playerBody);

      float groundHeight = 100.0f;
      Vector2 groundPos = new Vector2(SCREEN_WIDTH / 2, SCREEN_HEIGHT);
      Entity ground = pool.CreateEntity();
      ground.AddTransform(groundPos.X, groundPos.Y);
      Body groundBody = BodyFactory.CreateRectangle(_physicsWorld,
                                                    ConvertUnits.ToSimUnits(SCREEN_WIDTH),
                                                    ConvertUnits.ToSimUnits(groundHeight),
                                                    1,
                                                    ConvertUnits.ToSimUnits(groundPos));
      ground.AddPhysicsBody(groundBody);
    }

    protected override void Update(GameTime gameTime) {
      #if !__IOS__ &&  !__TVOS__
      if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();
      #endif

      _physicsWorld.Step(0.033333f);

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

      LumberJerksGame.SpriteBatch.Begin();
    
      _systems.Execute();

      var projection = 
        Matrix.CreateOrthographicOffCenter(
          0f,
          ConvertUnits.ToSimUnits(_graphics.GraphicsDevice.Viewport.Width),
          ConvertUnits.ToSimUnits(_graphics.GraphicsDevice.Viewport.Height),
          0f, 0f, 1f);
      _debugView.RenderDebugData(ref projection);

      LumberJerksGame.SpriteBatch.End();
            
      base.Draw(gameTime);
    }
  }
}

