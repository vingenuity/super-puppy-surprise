using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SuperPuppySurprise.AIRoutines;
using SuperPuppySurprise.PhysicsEngines;

namespace SuperPuppySurprise.GameObjects
{
    public class Player : GameObject
    {
        int playerID;
        KeyboardState thisKeyState;
        Texture2D texture;
        SpriteBatch spriteBatch;
        Keys upKey;
        Keys downKey;
        Keys leftKey;
        Keys rightKey;
        Keys fireUp;
        Keys fireDown;
        Keys fireLeft;
        Keys fireRight;

        public Player(int id, Vector2 Position)
            : base(Position)
        {
            Direction = Vector2.UnitY * -1;
            playerID = id;
            Speed = 300;
            Size = new Vector2(32, 32);
            Radius = 16;
            Game1.PhysicsEngine.Add(this);
            GameState.players.Add(this);

            switch (id)
            {
                case 1:
                    upKey = Keys.W;
                    downKey = Keys.S;
                    leftKey = Keys.A;
                    rightKey = Keys.D;
                    fireUp = Keys.Up;
                    fireDown = Keys.Down;
                    fireLeft = Keys.Left;
                    fireRight = Keys.Right;
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    //This is an error, technically.
                    break;
            }
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

            //Update movement
            Direction = Vector2.Zero;
            if (thisKeyState.IsKeyDown(downKey))
                Direction.Y++;
            else if (thisKeyState.IsKeyDown(upKey))
                Direction.Y--;
            if (thisKeyState.IsKeyDown(rightKey))
                Direction.X++;
            else if (thisKeyState.IsKeyDown(leftKey))
                Direction.X--;

            Direction.Normalize();

            if (thisKeyState.IsKeyUp(leftKey) && thisKeyState.IsKeyUp(rightKey) && thisKeyState.IsKeyUp(upKey) && thisKeyState.IsKeyUp(downKey))
                Velocity = Vector2.Zero;
            else
                Velocity = Direction * Speed;

            //Update fire direction
            //bulletDir.clear();
            if (thisKeyState.IsKeyDown(downKey))
                ;//bulletDir.Y++;
            else if (thisKeyState.IsKeyDown(upKey))
                ;//bulletDir.Y--;
            if (thisKeyState.IsKeyDown(rightKey))
                ;//bulletDir.X++;
            else if (thisKeyState.IsKeyDown(leftKey))
                ;//bulletDir.X--;
            //bulletDir.Normalize();

            if (thisKeyState.IsKeyUp(leftKey) && thisKeyState.IsKeyUp(rightKey) && thisKeyState.IsKeyUp(upKey) && thisKeyState.IsKeyUp(downKey))
                ;//bulletVelocity = Vector2.Zero;
            else
                ;//bulletVelocity = Direction * Speed;
        }
        public override void Draw(GameTime gameTime)
        {
            Rectangle r = new Rectangle((int)(Position.X - Size.X / 2), (int)(Position.Y - Size.Y / 2), (int)Size.X, (int)Size.Y);
            spriteBatch.Draw(texture, r, Color.White);
            base.Draw(gameTime);
        }
        public void fireBullet()
        {
            //construct new bullet, giving  position, direction
            Vector2 newPosition = new Vector2(this.Position.X + 8 * Direction.X, this.Position.Y + 8 * Direction.Y);
            Bullet b = new Bullet(newPosition, Direction);
            Game1.game.sceneObjects.Add(b);
            b.Load(Game1.game.Content, spriteBatch);
            Game1.PhysicsEngine.AddTrigger(b);
        }
    }
}