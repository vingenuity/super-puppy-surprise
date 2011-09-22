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
    class Shooter : Monster
    {
        //For drawing
        Texture2D texture;
        SpriteBatch spriteBatch;

        //For shooting
        double fireSpeed = 500;
        double elapsedTime;
        
        public Shooter(Vector2 Position) : base(Position)
        {
            elapsedTime = fireSpeed;
        }

        public override void Load(ContentManager Content, SpriteBatch spriteBatch)
        {
            texture = Content.Load<Texture2D>("TestPicture2");
            this.spriteBatch = spriteBatch;
            base.Load(Content, spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            Vector2 closest = GameState.findClosestPlayerTo(Position);

            Vector2 lastDirection = this.Direction;
            Vector2 newDirection = closest - this.Position;
            Direction = (lastDirection + newDirection) / 2;
            Direction.Normalize();
            Velocity = Direction * Speed;

            Vector2 bulletDir = closest - Position;
            bulletDir.Normalize();
            fireBullet(gameTime, bulletDir);

        }
        public override void Draw(GameTime gameTime)
        {
            Rectangle r = new Rectangle((int)(Position.X - Size.X / 2), (int)(Position.Y - Size.Y / 2), (int)Size.X, (int)Size.Y);
            spriteBatch.Draw(texture, r, Color.White);
            base.Draw(gameTime);
        }

        public void fireBullet(GameTime gameTime, Vector2 bulletDir)
        {
            //construct new bullet, giving position, direction
            //to do: prevent rapid fire
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedTime < fireSpeed)
                return;
            elapsedTime = 0;
            Vector2 newPosition = new Vector2(this.Position.X + 8 * bulletDir.X, this.Position.Y + 8 * bulletDir.Y);
            Bullet b = new Bullet(newPosition, bulletDir);
            Game1.sceneObjects.Add(b);
            b.Load(Game1.game.Content, spriteBatch);
           // Game1.PhysicsEngine.AddTrigger(b);
        }
    }
}
