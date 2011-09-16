using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperPuppySurprise.GameMech;

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
            
            spriteBatch.DrawString(spriteFont, "Score: " + GameMechanics.Score, new Vector2(540,100), Color.Black);
            spriteBatch.DrawString(spriteFont, "Rooms", new Vector2(600, 250), Color.Black);
            roomMap.Draw(gameTime);
        }
    }
}
