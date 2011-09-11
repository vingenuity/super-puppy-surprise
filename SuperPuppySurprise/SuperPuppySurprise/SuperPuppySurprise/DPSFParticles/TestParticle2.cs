﻿using DPSF.ParticleSystems;
using System;
using Microsoft.Xna.Framework;
using SuperPuppySurprise.GameObjects;
using Microsoft.Xna.Framework.Input;

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
        int a = 0;
        KeyboardState keystate;
        double timercd;
        //126... 42....-38
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
           timercd -= gameTime.ElapsedGameTime.TotalSeconds;
            keystate = Keyboard.GetState();
            if (keystate.IsKeyDown(Keys.Left) && timercd <= 0)
            {
                a = a +1;
                timercd = .03;

            }
            if (keystate.IsKeyDown(Keys.Right) && timercd <= 0)
            {
                a =  a -1;
                timercd = .03;

            }
            mcSphereParticleSystem.Pos = new Vector3(-126,0,0);//ParticleManager.To3D(gameObject.Position);*/
            mcSphereParticleSystem.Pos = ParticleManager.To3D(gameObject.Position);
            //mcSphereParticleSystem.Emitter.PositionData.Position = Position;
            base.Update(gameTime);
        }
    }
}