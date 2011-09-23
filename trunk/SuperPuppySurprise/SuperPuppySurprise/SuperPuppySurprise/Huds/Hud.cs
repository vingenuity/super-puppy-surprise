using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperPuppySurprise.GameMech;
using SuperPuppySurprise.GameObjects;
using SuperPuppySurprise.AIRoutines;

namespace SuperPuppySurprise.Huds
{
    public class Hud
    {
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Texture2D LightOn;
        Texture2D LightOff;
        Texture2D complete;
        Texture2D sidebar;
        Texture2D gun1, gun2, gun3;
        Texture2D gun0_icon, gun1_icon, gun2_icon, gun3_icon;
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
            sidebar = Game1.game.Content.Load<Texture2D>("asset_sideBoard");
            gun1 = Game1.game.Content.Load<Texture2D>("asset_icon_tripleFire");
            gun2 = Game1.game.Content.Load<Texture2D>("asset_icon_spread");
            gun3 = Game1.game.Content.Load<Texture2D>("asset_icon_minigun");
            gun0_icon = Game1.game.Content.Load<Texture2D>("PowerUps/asset_powerup_green");
            gun1_icon = Game1.game.Content.Load<Texture2D>("PowerUps/asset_powerup_blue");
            gun2_icon = Game1.game.Content.Load<Texture2D>("PowerUps/asset_powerup_orange");
            gun3_icon = Game1.game.Content.Load<Texture2D>("PowerUps/asset_powerup_purple");
        }
        public void Update(GameTime gameTime)
        {
            
        }
        public bool ShowLevelClearedText = false;
        public void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(sidebar, new Rectangle(500, 0, 250, 500), Color.White);
            Player p;
            if (GameState.players.Count > 0)
            {
                p = (Player)GameState.players[0];
                spriteBatch.DrawString(spriteFont, "" + GameMechanics.Score, new Vector2(620, 60), Color.White);
                int rounds;
                int offsetY = 25;
                int offsetX = 25;
                int i = 0; int j = 0;
                rounds = p.rounds[3];
                while (rounds > 25 && i < 4)
                {
                    spriteBatch.Draw(gun2, new Rectangle(614 + offsetX * i, 85 + offsetY * j, 15, 15), Color.White);
                    i++;
                    rounds -= 25;
                    // spriteBatch.DrawString(spriteFont, "Burstfire: " + p.rounds[3], new Vector2(600, 110), Color.White);

                }
                
                j++;
                i = 0;
                rounds = p.rounds[2];
                while (rounds > 25 && i < 4)
                {
                    spriteBatch.Draw(gun1, new Rectangle(614 + offsetX * i, 85 + offsetY * j, 15, 15), Color.White);
                    i++;
                    rounds -= 25;
                    //spriteBatch.DrawString(spriteFont, "Super Shotty: " + p.rounds[2], new Vector2(600, 85), Color.White);
                }
                j++;
                i = 0;
                rounds = p.rounds[4];
                while (rounds > 25 && i < 4)
                {
                    spriteBatch.Draw(gun3, new Rectangle(614 + offsetX * i, 85 + offsetY * j, 15, 15), Color.White);
                    i++;
                    rounds -= 25;
                    //spriteBatch.DrawString(spriteFont, "Automatic: " + p.rounds[4], new Vector2(600, 135), Color.White);
              
                }
                j++;
                if (p.getCurrentFireMode() % 5 == 0)
                    spriteBatch.Draw(gun0_icon, new Rectangle(550, 180, 30, 30), Color.White);
                if (p.getCurrentFireMode() % 5 == 2)
                    spriteBatch.Draw(gun1_icon, new Rectangle(550, 180, 30, 30), Color.White);
                if (p.getCurrentFireMode() % 5 == 3)
                    spriteBatch.Draw(gun2_icon, new Rectangle(550, 180, 30, 30), Color.White);
                if (p.getCurrentFireMode() % 5 == 4)
                    spriteBatch.Draw(gun3_icon, new Rectangle(550, 180, 30, 30), Color.White);
               
            }
            if (ShowLevelClearedText)
                spriteBatch.Draw(complete, rectFinish, Color.White);
            spriteBatch.DrawString(spriteFont, "Rooms", new Vector2(600, 300), Color.Black);
            if (GameMechanics.BottomLight)
            {
                spriteBatch.Draw(LightOn, new Rectangle(241, 480, 20, 20), Color.White);
            }
            else
            {
              //  spriteBatch.Draw(LightOff, new Rectangle(243, 484, 20, 20), Color.White);
            }
            if (GameMechanics.TopLight)
            {
                spriteBatch.Draw(LightOn, new Rectangle(240, 0, 20, 20), Color.White);
            }
            else
            {
                //spriteBatch.Draw(LightOff, new Rectangle(240, -1, 20, 20), Color.White);
            }
            if (GameMechanics.LeftLight)
            {
                spriteBatch.Draw(LightOn, new Rectangle(0, 239, 20, 20), Color.White);
            }
            else
            {
                //spriteBatch.Draw(LightOff, new Rectangle(0, 235, 20, 20), Color.White);
            }
            if (GameMechanics.RightLight)
            {
                spriteBatch.Draw(LightOn, new Rectangle(480, 240, 20, 20), Color.White);
            }
            else
            {
                //spriteBatch.Draw(LightOff, new Rectangle(480, 240, 20, 20), Color.White);
            }
            roomMap.Draw(gameTime);
        }
    }
}
