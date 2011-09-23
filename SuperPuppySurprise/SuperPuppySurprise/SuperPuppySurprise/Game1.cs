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
using GameStateManagement;
using SuperPuppySurprise.Huds;
using SuperPuppySurprise.Sounds;
using SuperPuppySurprise.PowerUps;
using SuperPuppySurprise.Spawning;
using SuperPuppySurprise.RoomManager;

namespace SuperPuppySurprise
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static Game1 game;
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        Texture2D texture;
        Vector2 PlayerPosition = Vector2.Zero;
        public static List<GameObject> sceneObjects;
        public static GameState state;
        public static BruteForcePhysicsEngine PhysicsEngine;
        public static ParticleManager ParticleEngine;
        public static PowerUpManager PowerUpEngine;
        public static float ScreenWidth;
        public static float ScreenHeight;
        public static ScreenManager screenManager;
        public static Hud hud;
        //public static Spawner spawner;
        public static SoundManager SoundEngine;
        public static SpawnManager SpawnManager;
        public static Texture2D Background;
        public static RoomEngine RoomManager;

        static List<GameObject> gameObjectAddList = new List<GameObject>();

        public Game1()
        {
            game = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";    
        }
        public void Restart()
        {
            graphics.PreferredBackBufferWidth = 750;
            graphics.PreferredBackBufferHeight = 500;
            graphics.ApplyChanges();
            ScreenHeight = graphics.GraphicsDevice.Viewport.Height;
            ScreenWidth = graphics.GraphicsDevice.Viewport.Width;
            
            // Create the screen manager component.
            screenManager = new ScreenManager(this);

            Components.Add(screenManager); 
        }
        public void RestartLoad()
        {
            Game1.SoundEngine = new SoundManager();
            Game1.SoundEngine.Load();

            // Activate the first screens.
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
        }
        public void RemoveGameObject(GameObject g)
        {
            sceneObjects.Remove(g);
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
            base.Initialize();
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            RestartLoad();
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
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DodgerBlue);
            
            
            base.Draw(gameTime);
        }
    }
}
