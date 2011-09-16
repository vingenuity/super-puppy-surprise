using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SuperPuppySurprise.GameMech;

namespace SuperPuppySurprise.Huds
{
    public class RoomMap
    {
        Texture2D Cleared;
        Texture2D Locked;
        Texture2D Arrow;
        int size = 30;
        Vector2 Position;
        public RoomMap(Vector2 Position)
        {
            this.Position = Position;
            Cleared = Game1.game.Content.Load<Texture2D>("HudRoom/ClearedDoor");
            Locked = Game1.game.Content.Load<Texture2D>("HudRoom/LockedDoor");
            Arrow = Game1.game.Content.Load<Texture2D>("HudRoom/Arrow");
        }
        public void Draw(GameTime gameTime)
        {
            Texture2D texture;
            for (int i = 0; i < 5; i++)
            {
                if (i <= GameMechanics.RoomNumber)
                    texture = Cleared;
                else
                    texture = Locked;
            
                Rectangle r = new Rectangle((int)(Position.X + i * size), (int)(Position.Y), size, size);
                Game1.spriteBatch.Draw(texture, r, Color.White);
            }
            Rectangle r1 = new Rectangle((int)(Position.X + GameMechanics.RoomNumber * size), (int)(Position.Y+size), size, size);
            Game1.spriteBatch.Draw(Arrow, r1, Color.White);
        }
    }
}
