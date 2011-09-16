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
            if (type == 1)
                texture = Game1.game.Content.Load<Texture2D>("PowerUps/asset_powerup_blue");
            if (type == 2)
                texture = Game1.game.Content.Load<Texture2D>("PowerUps/asset_powerup_green");
            if (type == 3)
                texture = Game1.game.Content.Load<Texture2D>("PowerUps/asset_powerup_orange");
            if (type == 4)
                texture = Game1.game.Content.Load<Texture2D>("PowerUps/asset_powerup_purple");
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
