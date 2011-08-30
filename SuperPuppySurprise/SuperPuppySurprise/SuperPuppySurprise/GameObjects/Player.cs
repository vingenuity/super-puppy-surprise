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
    public class Player : GameObject
    {
        KeyboardState thisKeyState;
        Texture2D texture;
        SpriteBatch spriteBatch;
     

        public Player(Vector2 Position)
            : base(Position)
        {
            Direction = Vector2.UnitY * -1;
            Speed = 300;
            Size = new Vector2(32, 32);
            Radius = 16;
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
            thisKeyState = Keyboard.GetState();

            Direction = Vector2.Zero;
            if (thisKeyState.IsKeyDown(Keys.Down))
                Direction.Y++;
            else if (thisKeyState.IsKeyDown(Keys.Up))
                Direction.Y--;
            if (thisKeyState.IsKeyDown(Keys.Right))
                Direction.X++;
            else if (thisKeyState.IsKeyDown(Keys.Left))
                Direction.X--;
            Direction.Normalize();

            if (thisKeyState.IsKeyUp(Keys.Left) && thisKeyState.IsKeyUp(Keys.Right) && thisKeyState.IsKeyUp(Keys.Up) && thisKeyState.IsKeyUp(Keys.Down))
                Velocity = Vector2.Zero;
            else
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