using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperPuppySurprise.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperPuppySurprise.PowerUps
{
    public class PowerUp : GameObject
    {
        protected Texture2D texture;
        protected double time;
        public PowerUp(Vector2 Position) : base(Position)
        {
            Game1.PhysicsEngine.AddTrigger(this);
        }
        public override void Unload()
        {
            Game1.sceneObjects.Remove(this);
            Game1.PowerUpEngine.Remove(this);
            Game1.PhysicsEngine.Remove(this);
            base.Unload();
        }
        public virtual void Effect(GameObject gameObject)
        {
        }
        public override void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalSeconds;
            if (time > 10)
                Unload();
            base.Update(gameTime);
        }
        public override void OnCollision(GameObject gameObject)
        {
            if (gameObject is Player)
            {
                Effect(gameObject);
                Unload();
            }

            base.OnCollision(gameObject);
        }
        public override void Draw(GameTime gameTime)
        {
            Rectangle r = new Rectangle((int)(Position.X - Size.X / 2), (int)(Position.Y - Size.Y / 2), (int)Size.X, (int)Size.Y);
            Game1.spriteBatch.Draw(texture, r, Color.White);
        }
    }
}
