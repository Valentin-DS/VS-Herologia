using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Test3D
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        GraphicsDevice device;

        Camera camera;
        Effect shader;

        SpriteBatch spriteBatch;
        SpriteFont sf;

        Level currentLevel;
        
        MainCharacter mc;
        Texture2D healthBar;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 720,
                PreferredBackBufferWidth = 1280,
            };

            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            ManageKeys.initialize();

            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferMultiSampling = true;
            GraphicsDevice.PresentationParameters.MultiSampleCount = 8;
            graphics.ApplyChanges();

            Window.Title = "Herologia Prototype";

            camera = new Camera(new Vector3(0.5f,3f,8f), new Vector3(0.5f, -0.5f, 0.5f), graphics);

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            device = GraphicsDevice;

            shader = Content.Load<Effect>("Effects//Shader");

            sf = Content.Load<SpriteFont>("File");

            mc = new MainCharacter(Content.Load<Model>("pleine"), Content.Load<Texture2D>("spritesheet_00"), Matrix.CreateScale(0.005f) * Matrix.CreateRotationX((float)Math.PI/2f) * Matrix.CreateWorld(new Vector3(0.5f, -0.5f, 0.5f), Vector3.Forward, Vector3.Up), new Vector2(1, 1), shader);
            healthBar = Content.Load<Texture2D>("StatsBar");

            //Light red = new Light(new Vector3(5,3,5), new Vector4(1,0,0,1), 1, 8);
            //Light green = new Light(new Vector3(5, 3, 15), new Vector4(0, 1, 0, 1), 1, 8);

            currentLevel = new Solbourg();
            currentLevel.Load(Content, shader);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            ManageKeys.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currentLevel.Update(gameTime);

            Movement.Update(mc, camera, currentLevel);

            mc.Update(gameTime);

            camera.Update(gameTime, mc.getPosition());

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            currentLevel.Draw(camera);

            mc.Draw(camera);

            spriteBatch.Begin();

            spriteBatch.Draw(healthBar, new Rectangle(10, 10, 170, 40), Color.White);
            spriteBatch.DrawString(sf, mc.getPositionOnGrid().X + " " + mc.getPositionOnGrid().Y, new Vector2(0, 0), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
