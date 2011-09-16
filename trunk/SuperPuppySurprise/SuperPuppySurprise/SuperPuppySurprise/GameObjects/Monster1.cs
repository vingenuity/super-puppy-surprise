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

namespace SuperPuppySurprise.GameObjects
{
    public class Monster : GameObject
    {
        public Monster(Vector2 Position)
            : base(Position)
        {
            Health = 100;
            Direction = Vector2.UnitY * -1;
            Speed = 100;
            Size = new Vector2(16, 16);
            Radius = 8;
            GameState.enemies.Add(this);
            Game1.PhysicsEngine.Add(this);
        }
        public override void OnDamage(double damage)
        {
            GameMechanics.Score += 5;
            DeathParticle.CreateDeathParticle(Position);
            Unload();
            base.OnDamage(damage);
        }

        public override void Unload()
        {
            GameState.enemies.Remove(this);
            Game1.PhysicsEngine.Remove(this);
            Game1.game.RemoveGameObject(this);
            base.Unload();
        }
    }
}
