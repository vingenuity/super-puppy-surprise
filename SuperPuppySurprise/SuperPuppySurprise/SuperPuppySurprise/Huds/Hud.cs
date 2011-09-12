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
        public Hud()
        {
            spriteFont = Game1.game.Content.Load<SpriteFont>("Menu/menufont");
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
        }
    }
}
