using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DPSF;
using Microsoft.Xna.Framework;
using DPSF.ParticleSystems;

namespace SuperPuppySurprise.DPSFParticles
{
    public class ParticleManager
    {
        ParticleSystemManager manager;
        Matrix Projection;
        Matrix View;
        RandomParticleSystem paricleSystemTest;
        public ParticleManager()
        {
            float aspectRatio = (float)Game1.game.GraphicsDevice.Viewport.Width / (float)Game1.game.GraphicsDevice.Viewport.Height;
            manager = new ParticleSystemManager();
            manager.Enabled = true;
            manager.AutoInitializeAllParticleSystems(Game1.game.GraphicsDevice, Game1.game.Content, Game1.game.spriteBatch);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1, 10000);
            View = Matrix.CreateLookAt(new Vector3(0,0,-50), new Vector3(0,0,0),Vector3.Up);
            manager.SetWorldViewProjectionMatricesForAllParticleSystems(Matrix.Identity, View, Projection);
            paricleSystemTest = new RandomParticleSystem(Game1.game);
            paricleSystemTest.DrawOrder = 100;
            
           
            manager.AddParticleSystem(paricleSystemTest);
            paricleSystemTest.AutoInitialize(Game1.game.GraphicsDevice, Game1.game.Content, null);
            paricleSystemTest.Enabled = true;
        }
        public void Draw()
        {
            manager.DrawAllParticleSystems();
        }
        public void Update(GameTime gameTime)
        {
            manager.UpdateAllParticleSystems((float)(gameTime.ElapsedGameTime.TotalSeconds));
     
        }
    }
}
