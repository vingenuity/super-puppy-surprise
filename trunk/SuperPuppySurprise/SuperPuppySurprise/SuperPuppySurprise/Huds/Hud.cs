using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperPuppySurprise.GameMech;
using SuperPuppySurprise.GameObjects;

namespace SuperPuppySurprise.Huds
{
    public class Hud
    {
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        RoomMap roomMap;
        public Hud()
        {
            spriteFont = Game1.game.Content.Load<SpriteFont>("Menu/menufont");
            roomMap = new RoomMap(new Vector2(550,300));
        }
        public void Load()
        {
            spriteBatch = Game1.spriteBatch;
        }
        public void Update(GameTime gameTime)
        {
            
        }
        public void Draw(GameTime gameTime)
        {
            Player p;
            p = Game1.sceneObjects[0] as Player;
            spriteBatch.DrawString(spriteFont, "Score: " + GameMechanics.Score, new Vector2(560,20), Color.Black);
            spriteBatch.DrawString(spriteFont, "Shotgun: " + p.rounds[1], new Vector2(500, 60), Color.Black);
            spriteBatch.DrawString(spriteFont, "Super Shotty: " + p.rounds[2], new Vector2(500, 85), Color.Black);
            spriteBatch.DrawString(spriteFont, "Burstfire: " + p.rounds[3], new Vector2(500, 110), Color.Black);
            spriteBatch.DrawString(spriteFont, "Automatic: " + p.rounds[4], new Vector2(500, 135), Color.Black);
            spriteBatch.DrawString(spriteFont, "Rooms", new Vector2(600, 250), Color.Black);
            roomMap.Draw(gameTime);
        }
    }
}
