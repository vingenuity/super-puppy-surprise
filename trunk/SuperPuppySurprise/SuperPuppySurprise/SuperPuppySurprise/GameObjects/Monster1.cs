using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SuperPuppySurprise.AIRoutines;
using SuperPuppySurprise.GameMech;
using SuperPuppySurprise.DPSFParticles;
using SuperPuppySurprise.PowerUps;
using GameStateManagement;
using SuperPuppySurprise.Sounds;

namespace SuperPuppySurprise.GameObjects
{
    public class Monster : GameObject
    {
        static Random random = new Random();
        public Monster(Vector2 Position)
            : base(Position)
        {
            Health = 100;
            Direction = Vector2.UnitY * -1;
            Speed = 100;
            Size = new Vector2(16, 16);
            Radius = 8;     
        }
        
        public override void OnDamage(double damage)
        {
            GameMechanics.Score += 5;
            DeathParticle.CreateDeathParticle(Position);
            int type = random.Next(4) + 1;
            int rand = random.Next(6);
            if(rand > 4)
                Game1.PowerUpEngine.Add(new ChangeWeapon1(Position, type));
            Unload();
            base.OnDamage(damage);
        }

        public override void Unload()
        {
            Game1.SoundEngine.PlaySound(SoundEffects.explode);
            GameState.enemies.Remove(this);
            Game1.PhysicsEngine.Remove(this);
            Game1.game.RemoveGameObject(this);
            base.Unload();
        }
        public override void OnCollision(GameObject gameObject)
        {
           // if (gameObject is Player)
            //    Game1.screenManager.AddScreen(new VictoryDefeatScreen("They got you.........."), PlayerIndex.One);
            base.OnCollision(gameObject);
        }
    }
}
