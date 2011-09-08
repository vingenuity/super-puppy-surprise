using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SuperPuppySurprise.GameObjects;
using SuperPuppySurprise.PhysicsEngines;
using SuperPuppySurprise.AIRoutines;
using SuperPuppySurprise.DPSFParticles;
using DPSF.ParticleSystems;

namespace SuperPuppySurprise
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static Game1 game;
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        Texture2D texture;
        Vector2 PlayerPosition = Vector2.Zero;
        public List<GameObject> sceneObjects;
        public static GameState state;
        public static BruteForcePhysicsEngine PhysicsEngine;
        public static ParticleManager ParticleEngine;
        public static float ScreenWidth;
        public static float ScreenHeight;
        Texture2D Background;
        public Game1()
        {
            game = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            
        }
        public void Restart()
        {
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
            graphics.ApplyChanges();
            ScreenHeight = graphics.GraphicsDevice.Viewport.Height;
            ScreenWidth = graphics.GraphicsDevice.Viewport.Width;
            sceneObjects = new List<GameObject>();
            PhysicsEngine = new BruteForcePhysicsEngine();
            ParticleEngine = new ParticleManager();
            state = new GameState();

            
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Restart();
            // TODO: Add your initialization logic here
            sceneObjects.Add(new Player(1, new Vector2(100, 100)));
            //sceneObjects.Add(new Runner(new Vector2(350, 200)));
            //sceneObjects.Add(new Shooter(new Vector2(200, 100)));
            sceneObjects.Add(new TestTrigger(new Vector2(500, 300)));
            base.Initialize();
        }

        KeyboardState thisKeyState;
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            for (int i = 0; i < sceneObjects.Count; i++)
                sceneObjects[i].Load(Content, spriteBatch);

            Background = Content.Load<Texture2D>("Background");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            for (int i = 0; i < sceneObjects.Count; i++)
                sceneObjects[i].Update(gameTime);

            PhysicsEngine.Update(gameTime);

            thisKeyState = Keyboard.GetState();
            // TODO: Add your update logic here

            ParticleEngine.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DodgerBlue);
            
            spriteBatch.Begin();

            spriteBatch.Draw(Background, new Rectangle(0, 0, 500, 500), Color.White);
            for (int i = 0; i < sceneObjects.Count; i++)
                sceneObjects[i].Draw(gameTime);
            spriteBatch.End();
            // TODO: Add your drawing code here
            
            ParticleEngine.Draw();
            base.Draw(gameTime);
        }
    }
}
