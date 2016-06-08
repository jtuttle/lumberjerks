using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

using Entitas;

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

    Body _body;

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


      _physicsWorld = new World(Vector2.UnitY * 10.0f);



      Texture2D tex = this.Content.Load<Texture2D>("Sprites/chr_manA_idleA");

      Pool pool = new Pool(10);

      _systems = new Systems().
        Add(pool.CreateSystem<SpriteRenderSystem>()).
        Add(pool.CreateSystem<MovementSystem>());

      Vector2 playerPos = new Vector2(300f, 300f);

      Entity entity = pool.CreateEntity();
      entity.AddPlayer(PlayerIndex.One);
      entity.AddTransform(playerPos.X, playerPos.Y);
      entity.AddSprite(tex, new Rectangle(0, 0, 128, 128), Vector2.Zero);


      _body = BodyFactory.CreateRectangle(_physicsWorld, 128, 128, 1, playerPos);
      _body.BodyType = BodyType.Dynamic;
      entity.AddPhysicsBody(_body);

      CircleShape circleShape = new CircleShape(0.5f, 1.0f);

      Fixture fixture = _body.CreateFixture(circleShape);
    }

    protected override void Update(GameTime gameTime) {
      #if !__IOS__ &&  !__TVOS__
      if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();
      #endif


      _physicsWorld.Step(0.033333f);

      Console.WriteLine(_body.Position);

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

      LumberJerksGame.SpriteBatch.Begin();
    
      _systems.Execute();

      LumberJerksGame.SpriteBatch.End();
            
      base.Draw(gameTime);
    }

    private void AddPlayer(Pool pool, Vector2 position) {

    }
  }
}

