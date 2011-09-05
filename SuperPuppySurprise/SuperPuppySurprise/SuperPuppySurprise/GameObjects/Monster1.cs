using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SuperPuppySurprise.AIRoutines;

namespace SuperPuppySurprise.GameObjects
{
    public class Monster : GameObject
    {
        Texture2D texture;
        SpriteBatch spriteBatch;

        public Monster(Vector2 Position)
            : base(Position)
        {
            Direction = Vector2.UnitY * -1;
            Speed = 100;
            Size = new Vector2(16, 16);
            Radius = 8;
            Game1.PhysicsEngine.Add(this);
        }

        public override void Load(ContentManager Content, SpriteBatch spriteBatch)
        {
            texture = Content.Load<Texture2D>("TestPicture2");
            this.spriteBatch = spriteBatch;
            base.Load(Content, spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            Player closest = GameState.findClosestPlayerTo(Position);

            Direction = Vector2.Zero;
            if (closest.getPosition().Y > this.Position.Y)
                Direction.Y++;
            else if (closest.getPosition().Y < this.Position.Y)
                Direction.Y--;
            if (closest.getPosition().X > this.Position.X)
                Direction.X++;
            else if (closest.getPosition().X < this.Position.X)
                Direction.X--;
            Direction.Normalize();
      
            Velocity = Direction * Speed;
        }
        public override void Draw(GameTime gameTime)
        {
            Rectangle r = new Rectangle((int)(Position.X - Size.X / 2), (int)(Position.Y - Size.Y / 2), (int)Size.X, (int)Size.Y);
            spriteBatch.Draw(texture, r, Color.White);
            base.Draw(gameTime);
        }
    }
}
