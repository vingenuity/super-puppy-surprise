using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SuperPuppySurprise.AIRoutines;
using SuperPuppySurprise.PhysicsEngines;

namespace SuperPuppySurprise.GameObjects
{
    public class Runner : Monster
    {

        Texture2D texture;
        SpriteBatch spriteBatch;

        public Runner(Vector2 Position) : base(Position)
        {
            Speed = 200;
        }
      

        public static Texture2D texture2;

        public void Load()
        {
            texture = texture2;
        }
        public override void Update(GameTime gameTime)
        {
            Vector2 closest = GameState.findClosestPlayerTo(Position);

            Direction = Vector2.Zero;
            if (closest.Y > this.Position.Y + 5)
                Direction.Y++;
            else if (closest.Y < this.Position.Y - 5)
                Direction.Y--;
            if (closest.X > this.Position.X + 5)
                Direction.X++;
            else if (closest.X < this.Position.X - 5)
                Direction.X--;
            Direction.Normalize();

            Velocity = Direction * Speed;
        }
        public override void Draw(GameTime gameTime)
        {
            Rectangle r = new Rectangle((int)(Position.X - Size.X / 2), (int)(Position.Y - Size.Y / 2), (int)Size.X, (int)Size.Y);
            Game1.spriteBatch.Draw(texture, r, Color.White);
            base.Draw(gameTime);
        }
        public override void Unload()
        {

            base.Unload();
        }
        public override void OnCollision(GameObject obj)
        {
            base.OnCollision(obj);
        }
        public override void AddGameObjectToScene()
        {

            Game1.sceneObjects.Add(this);
            Game1.PhysicsEngine.Add(this);
            GameState.enemies.Add(this);
            base.AddGameObjectToScene();
        }
    }
}
