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
        double timeSinceSpawn;
        Texture2D texture;
        SpriteBatch spriteBatch;

        public Runner(Vector2 Position) : base(Position)
        {
            Speed = 200;
            timeSinceSpawn = 0;
        }
      

        public static Texture2D texture2;

        public void Load()
        {
            texture = Game1.game.Content.Load<Texture2D>("asset_enemy_base");
            eyeTexture = Game1.game.Content.Load<Texture2D>("asset_enemy_eye");
            Orig = new Vector2(eyeTexture.Width / 2, eyeTexture.Height / 2);
            scale = (float)(Size.X / (eyeTexture.Width * 1.0)); 
        }
        public override void Update(GameTime gameTime)
        {
            //Last enemy superpower clause
            if (GameState.enemies.Count() < 2 && timeSinceSpawn > 2) Speed = 325;
            timeSinceSpawn += gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 closest = GameState.findClosestPlayerTo(Position);

            Vector2 lastDirection = this.Direction;
            Vector2 newDirection = closest - this.Position;
            Direction = (lastDirection + newDirection) / 2;
            Direction.Normalize();

            Velocity = Direction * Speed;
            CalculateRotation();
        }
        Texture2D eyeTexture;
        Vector2 Orig;
        float scale = 1;
        float eyeRotation = 0;
        void CalculateRotation()
        {
            if (GameState.players.Count > 0)
            {
                Vector2 EyeDirection = GameState.players[0].Position - Position;
                EyeDirection.Normalize();
                eyeRotation = (float)Math.Atan2(EyeDirection.X, -EyeDirection.Y);
            }
        }
        public override void Draw(GameTime gameTime)
        {
            Rectangle r = new Rectangle((int)(Position.X - Size.X / 2), (int)(Position.Y - Size.Y / 2), (int)Size.X, (int)Size.Y);
            Game1.spriteBatch.Draw(texture, r, Color.White);
            Game1.spriteBatch.Draw(eyeTexture, ((Position)), null, Color.White, eyeRotation, Orig, .1f, SpriteEffects.None, 0);
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
