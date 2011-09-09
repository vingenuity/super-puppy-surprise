using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SuperPuppySurprise.GameObjects
{
    public class GameObject
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Direction;
        public float Speed;
        public double Health;
        //Radius is used for physics only Graphics uses Size
        public float Radius;
        public Vector2 Size;

        public GameObject(Vector2 Position)
        {
            this.Position = Position;
        }
        public virtual void Load(ContentManager Content, SpriteBatch spriteBatch)
        {
        }
        public virtual void Unload()
        {
        }
        public virtual void Update(GameTime gameTime)
        {
        }
        public virtual void Draw(GameTime gameTime)
        {
        }
        public virtual void OnDamage(double damage)
        {
        }
        //For AI usage.
        public Vector2 getPosition()
        {
            return this.Position;
        }
        public virtual void OnCollision(GameObject gameObject)
        {
        }
        public virtual void OnDamage(double damage)
        {
        }
    }
}
