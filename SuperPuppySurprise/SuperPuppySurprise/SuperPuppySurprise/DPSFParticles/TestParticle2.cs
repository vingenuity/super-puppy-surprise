using DPSF.ParticleSystems;
using System;
using Microsoft.Xna.Framework;
using SuperPuppySurprise.GameObjects;

namespace SuperPuppySurprise.DPSFParticles
{
    public class TestParticle2 : Particle
    {
        Random2DParticleSystem mcSphereParticleSystem;
        public int counter = 0;
        public Vector3 Position;
        GameObject gameObject;
       
        public TestParticle2(GameObject gameObject)
            : base()
        {
            this.gameObject = gameObject;
            mcSphereParticleSystem = new Random2DParticleSystem(Game1.game);
            mcSphereParticleSystem.AutoInitialize(Game1.game.GraphicsDevice, Game1.game.Content, Game1.spriteBatch);
            
            ParticleSystem = mcSphereParticleSystem;

            //mcSphereParticleSystem.ChangeSphereRadius(.000000005f);
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
           
            
            mcSphereParticleSystem.Pos = ParticleManager.To3D(gameObject.Position);
           
            //mcSphereParticleSystem.Emitter.PositionData.Position = Position;
            base.Update(gameTime);
        }
    }
}