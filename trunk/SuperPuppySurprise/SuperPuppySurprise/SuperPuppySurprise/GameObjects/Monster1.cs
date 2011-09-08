using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SuperPuppySurprise.AIRoutines;

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
    }
}
