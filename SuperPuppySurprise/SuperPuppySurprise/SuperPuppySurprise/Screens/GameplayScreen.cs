#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperPuppySurprise;
using SuperPuppySurprise.GameObjects;
using SuperPuppySurprise.PhysicsEngines;
using SuperPuppySurprise.DPSFParticles;
using SuperPuppySurprise.AIRoutines;
using System.Collections.Generic;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        SpriteFont gameFont;

        float pauseAlpha;

        Texture2D Background;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            Restart();
        }

        public void Restart()
        {
            Game1.sceneObjects = new List<GameObject>();
            Game1.PhysicsEngine = new BruteForcePhysicsEngine();
            Game1.ParticleEngine = new ParticleManager();
            Game1.state = new GameState();
            LoadUnits();
        }
        public void LoadUnits()
        {
            // TODO: Add your initialization logic here
            Game1.sceneObjects.Add(new Player(1, new Vector2(100, 100)));
            Game1.sceneObjects.Add(new Runner(new Vector2(350, 200)));
            //Game1.sceneObjects.Add(new Shooter(new Vector2(200, 100)));
            Game1.sceneObjects.Add(new TestTrigger(new Vector2(300, 300)));
        }
        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            
            // Create a new SpriteBatch, which can be used to draw textures.
            Game1.spriteBatch = new SpriteBatch(Game1.game.GraphicsDevice);

            for (int i = 0; i < Game1.sceneObjects.Count; i++)
                Game1.sceneObjects[i].Load(Game1.game.Content, Game1.spriteBatch);

            Background = Game1.game.Content.Load<Texture2D>("Background");

            ScreenManager.Game.ResetElapsedTime();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {

                for (int i = 0; i < Game1.sceneObjects.Count; i++)
                    Game1.sceneObjects[i].Update(gameTime);

                Game1.PhysicsEngine.Update(gameTime);

                Game1.ParticleEngine.Update(gameTime);
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
              
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            Game1.spriteBatch.Begin();

            Game1.spriteBatch.Draw(Background, new Rectangle(0, 0, 500, 500), Color.White);
            for (int i = 0; i < Game1.sceneObjects.Count; i++)
                Game1.sceneObjects[i].Draw(gameTime);
            Game1.spriteBatch.End();
            // TODO: Add your drawing code here

            Game1.ParticleEngine.Draw();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }


        #endregion
    }
}
