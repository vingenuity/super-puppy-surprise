using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SuperPuppySurprise.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace SuperPuppySurprise.PowerUps
{
    public class ChangeWeapon1 : PowerUp
    {
        int type;
        public ChangeWeapon1(Vector2 Position, int type): base(Position)
        {
            texture = Game1.game.Content.Load<Texture2D>("PowerUps/asset_powerup_blue");
            Size = new Vector2(12, 12);
            this.type = type;
        }
        public override void Effect(GameObject gameObject)
        {
            Player p = gameObject as Player;
            p.ChangeWeapon(type);
            base.Effect(gameObject);
        }
    }
}
