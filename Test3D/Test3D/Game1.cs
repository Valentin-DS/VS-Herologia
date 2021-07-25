using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Test3D
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public static GraphicsDevice device;
        public Camera camera;
        SpriteBatch spriteBatch;
        SpriteFont sf;
        Level currentLevel;  
        MainCharacter mc;
        Texture2D healthBar;
        List<Effect> shaders;

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
            Window.Title = "Herologia - Vertical Slice";
            camera = new Camera(new Vector3(0.5f,3f,58f), new Vector3(0.5f, -0.5f, 0.5f), graphics);
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = GraphicsDevice;

            this.shaders = new List<Effect>()
            {
                { Content.Load<Effect>("Effects//Shader") },
                { Content.Load<Effect>("Effects//Glass") }
            };

            sf = Content.Load<SpriteFont>("File");
            mc = new MainCharacter(Content.Load<Model>("pleine"), Content.Load<Texture2D>("spritesheet_00"), Matrix.CreateScale(0.005f) * Matrix.CreateRotationX((float)Math.PI/2f) * Matrix.CreateWorld(new Vector3(0.5f, -0.5f, 50.5f), Vector3.Forward, Vector3.Up), new Vector2(1, 51), shaders[0]);
            healthBar = Content.Load<Texture2D>("StatsBar");
            //Light red = new Light(new Vector3(5,3,5), new Vector4(1,0,0,1), 1, 8);
            //Light green = new Light(new Vector3(5, 3, 15), new Vector4(0, 1, 0, 1), 1, 8);

            currentLevel = new Solbourg(mc);
            currentLevel.Load(Content, shaders);
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
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            spriteBatch.Draw(healthBar, new Rectangle(10, 10, 170, 40), Color.White);
            spriteBatch.DrawString(sf, mc.getPositionOnGrid().ToString() + " || " + mc.getPosition().ToString(), new Vector2(0, 0), Color.Red);
            //spriteBatch.DrawString(sf, Movement.isSecondFloorReached.ToString(), new Vector2(0, 20), Color.Red);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
