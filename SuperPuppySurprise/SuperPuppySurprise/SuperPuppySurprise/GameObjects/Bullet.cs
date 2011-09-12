using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SuperPuppySurprise.GameMech;

namespace SuperPuppySurprise.GameObjects
{
    public class Bullet : GameObject
    {
        Texture2D texture;
        SpriteBatch spriteBatch;

        public Bullet(Vector2 Position, Vector2 Dir)
            : base(Position) 
        {
            Direction = Dir;
            Speed = 350;
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

        public override void Unload()
        {
            Game1.PhysicsEngine.Remove(this);
            Game1.game.RemoveGameObject(this);
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            //detect collision, unload if needed
            if (HitsWalls())
                Unload();
        }
        
        public override void Draw(GameTime gameTime)
        {
            Rectangle r = new Rectangle((int)(Position.X - Size.X / 2), (int)(Position.Y - Size.Y / 2), (int)Size.X, (int)Size.Y);
            spriteBatch.Draw(texture, r, Color.White);
            base.Draw(gameTime);
        }
        public override void OnCollision(GameObject gameObject)
        {
            if (gameObject is Monster)
            {
                gameObject.OnDamage(0);
                Unload();
            }

            if (gameObject is Player)
            {
                gameObject.OnDamage(1);
            }

            base.OnCollision(gameObject);
        }
        bool HitsWalls()
        {
            int radius = (int)Radius;
            if (GameMechanics.LeftWallBound > Position.X ) return true;
            if (GameMechanics.RightWallBound < Position.X) return true;
            if (GameMechanics.TopWallBound > Position.Y) return true;
            if (GameMechanics.BottomWallBound < Position.Y) return true;
            return false;
        }
    }
}
