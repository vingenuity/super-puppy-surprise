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
        Texture2D LightOn;
        Texture2D LightOff;
        Texture2D complete;
        RoomMap roomMap;
        Rectangle rectFinish = new Rectangle(100, 100, 300, 300);
        public Hud()
        {
            spriteFont = Game1.game.Content.Load<SpriteFont>("Menu/SideText");
           
            roomMap = new RoomMap(new Vector2(550,300));
        }
        public void Load()
        {
            spriteBatch = Game1.spriteBatch;
            LightOn = Game1.game.Content.Load<Texture2D>("LightOff");
            LightOff = Game1.game.Content.Load<Texture2D>("LightOn");
            complete = Game1.game.Content.Load<Texture2D>("levelcomplete");
        }
        public void Update(GameTime gameTime)
        {
            
        }
        public bool ShowLevelClearedText = false;
        public void Draw(GameTime gameTime)
        {
            Player p;
            p = Game1.sceneObjects[0] as Player;
            spriteBatch.DrawString(spriteFont, "Score: " + GameMechanics.Score, new Vector2(560,20), Color.Black);
            spriteBatch.DrawString(spriteFont, "Shotgun: " + p.rounds[1], new Vector2(500, 60), Color.Black);
            spriteBatch.DrawString(spriteFont, "Super Shotty: " + p.rounds[2], new Vector2(500, 85), Color.Black);
            spriteBatch.DrawString(spriteFont, "Burstfire: " + p.rounds[3], new Vector2(500, 110), Color.Black);
            spriteBatch.DrawString(spriteFont, "Automatic: " + p.rounds[4], new Vector2(500, 135), Color.Black);
            if (ShowLevelClearedText)
                spriteBatch.Draw(complete, rectFinish, Color.White);
                //spriteBatch.DrawString(spriteFont, "Level Cleared", new Vector2(230, 230), Color.Black);
            spriteBatch.DrawString(spriteFont, "Rooms", new Vector2(600, 300), Color.Black);
            if (GameMechanics.BottomLight)
                spriteBatch.Draw(LightOn, new Rectangle(243,484, 20, 20), Color.White);
            else
                spriteBatch.Draw(LightOff, new Rectangle(243, 484, 20, 20), Color.White);
            if (GameMechanics.TopLight)
                spriteBatch.Draw(LightOn, new Rectangle(240, -1, 20, 20), Color.White);
            else
                spriteBatch.Draw(LightOff, new Rectangle(240, -1, 20, 20), Color.White);
            if (GameMechanics.LeftLight)
                spriteBatch.Draw(LightOn, new Rectangle(0, 235, 20, 20), Color.White);
            else
                spriteBatch.Draw(LightOff, new Rectangle(0, 235, 20, 20), Color.White);
            if (GameMechanics.RightLight)
                spriteBatch.Draw(LightOn, new Rectangle(480, 240,20, 20), Color.White);
            else
                spriteBatch.Draw(LightOff, new Rectangle(480, 240, 20, 20), Color.White);
            roomMap.Draw(gameTime);
        }
    }
}
