using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SuperPuppySurprise.GameObjects
{
    public class Bullet : GameObject
    {
        KeyboardState thisKeyState;
        Texture2D texture;
        SpriteBatch spriteBatch;

        public Bullet(Vector2 Position, Vector2 Dir)
            : base(Position) 
        {
            Direction = Dir;
            Speed = 500;
            Size = new Vector2(4, 4);
            Radius = 2;
            Game1.PhysicsEngine.AddTrigger(this);
            Velocity = Direction * Speed;
        }

        //at the moment, a bullet looks like a player or monster
        public override void Load(ContentManager Content, SpriteBatch spriteBatch)
        {
            texture = Content.Load<Texture2D>("TestPicture2");
            this.spriteBatch = spriteBatch;
            base.Load(Content, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            //detect collision, unload if needed
        }
        
        public override void Draw(GameTime gameTime)
        {
            Rectangle r = new Rectangle((int)(Position.X - Size.X / 2), (int)(Position.Y - Size.Y / 2), (int)Size.X, (int)Size.Y);
            spriteBatch.Draw(texture, r, Color.White);
            base.Draw(gameTime);
        }
    }
}
