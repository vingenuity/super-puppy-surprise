using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DPSF;
using Microsoft.Xna.Framework;

namespace SuperPuppySurprise.DPSFParticles
{
    public class ParticleManager
    {
        ParticleSystemManager manager;
        Matrix Projection;
        Matrix View;
        public ParticleManager()
        {
            float aspectRatio = (float)Game1.game.GraphicsDevice.Viewport.Width / (float)Game1.game.GraphicsDevice.Viewport.Height;
            manager = new ParticleSystemManager();
            manager.AutoInitializeAllParticleSystems(Game1.game.GraphicsDevice, Game1.game.Content, Game1.game.spriteBatch);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1, 10000);
            View = Matrix.CreateTranslation(new Vector3(0, -50, 0)) *
									 Matrix.CreateRotationY(MathHelper.ToRadians(180)) *
									 Matrix.CreateRotationX(MathHelper.ToRadians(0)) *
									 Matrix.CreateLookAt(new Vector3(0, 0, -300),
														 new Vector3(0, 0, 0), Vector3.Up);
            manager.SetWorldViewProjectionMatricesForAllParticleSystems(Matrix.Identity, View, Projection);
            //manager
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
