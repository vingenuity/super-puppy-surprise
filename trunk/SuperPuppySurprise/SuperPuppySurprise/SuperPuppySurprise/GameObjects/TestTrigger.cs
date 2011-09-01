using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SuperPuppySurprise.GameObjects
{
    public class TestTrigger : GameObject
    {

        Texture2D texture;
        SpriteBatch spriteBatch;
        Color c = Color.White;
        public TestTrigger(Vector2 Position)
            : base(Position)
        {
        
            Size = new Vector2(16, 16);
            Radius = 8;
            Game1.PhysicsEngine.AddTrigger(this);
        }

        public override void Load(ContentManager Content, SpriteBatch spriteBatch)
        {
            texture = Content.Load<Texture2D>("TestPicture2");
            this.spriteBatch = spriteBatch;
            base.Load(Content, spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(GameTime gameTime)
        {
            Rectangle r = new Rectangle((int)(Position.X - Size.X / 2), (int)(Position.Y - Size.Y / 2), (int)Size.X, (int)Size.Y);
            spriteBatch.Draw(texture, r, c);
            base.Draw(gameTime);
        }
        public override void OnCollision(GameObject gameObject)
        {
            c = Color.Black;
            base.OnCollision(gameObject);
        }
    }
}
