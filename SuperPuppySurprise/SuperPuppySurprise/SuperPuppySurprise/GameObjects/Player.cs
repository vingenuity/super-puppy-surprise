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
using SuperPuppySurprise.DPSFParticles;

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
        int currentFireSpeed = 0;
        double[] fireSpeeds = {1000, 500, 300, 1};
        bool rotateHelper = true;
        double elapsedTime;
        TestParticle2 testParticle;
        public Player(int id, Vector2 Position)
            : base(Position)
        {
            Direction = Vector2.UnitY * -1;
            playerID = id;
            Speed = 300;
            elapsedTime = fireSpeeds[0];
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
            testParticle = new TestParticle2(this);
            
        }
      
        void EngineParticle()
        {
           
            if(Velocity.LengthSquared() > 0 && !testParticle.started)
                testParticle.Start();
            if(Velocity.LengthSquared() == 0 && testParticle.started)
                testParticle.End();
        }
        public override void Load(ContentManager Content, SpriteBatch spriteBatch)
        {
            texture = Content.Load<Texture2D>("TestPicture2");
            this.spriteBatch = spriteBatch;
            base.Load(Content, spriteBatch);
        }

        public void rotateWeapons()
        {
            currentFireSpeed++;
        }

        public override void Update(GameTime gameTime)
        {
            thisKeyState = Keyboard.GetState();
            Vector2 bulletDir;

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
            bulletDir.X = 0;
            bulletDir.Y = 0;
            if (thisKeyState.IsKeyDown(fireDown))
                bulletDir.Y++;
            else if (thisKeyState.IsKeyDown(fireUp))
                bulletDir.Y--;
            if (thisKeyState.IsKeyDown(fireRight))
                bulletDir.X++;
            else if (thisKeyState.IsKeyDown(fireLeft))
                bulletDir.X--;
            bulletDir.Normalize();

            if (thisKeyState.IsKeyDown(fireUp) || thisKeyState.IsKeyDown(fireDown) || 
                thisKeyState.IsKeyDown(fireRight) || thisKeyState.IsKeyDown(fireLeft))
                fireBullet(gameTime, bulletDir);
            if (thisKeyState.IsKeyUp(fireUp) && thisKeyState.IsKeyUp(fireDown) &&
                thisKeyState.IsKeyUp(fireRight) && thisKeyState.IsKeyUp(fireLeft))
                elapsedTime = fireSpeeds[currentFireSpeed % fireSpeeds.Length];

            if (thisKeyState.IsKeyDown(Keys.Tab) && rotateHelper)
            {
                rotateWeapons();
                rotateHelper = false;
            }
            if (thisKeyState.IsKeyDown(Keys.O))
            {
                Unload();
            }

            if (thisKeyState.IsKeyUp(Keys.Tab))
                rotateHelper = true;

            EngineParticle();
            //if (thisKeyState.IsKeyUp(leftKey) && thisKeyState.IsKeyUp(rightKey) && thisKeyState.IsKeyUp(upKey) && thisKeyState.IsKeyUp(downKey))
                //bulletVelocity = Vector2.Zero;
            //else
                //;bulletVelocity = Direction * Speed;
        }
        public override void Unload()
        {
            
            Game1.ParticleEngine.Remove(testParticle);
            Game1.PhysicsEngine.Remove(this);
            Game1.game.RemoveGameObject(this);
            base.Unload();
        }
        public override void Draw(GameTime gameTime)
        {
            Rectangle r = new Rectangle((int)(Position.X - Size.X / 2), (int)(Position.Y - Size.Y / 2), (int)Size.X, (int)Size.Y);
            spriteBatch.Draw(texture, r, Color.White);
            base.Draw(gameTime);
        }
        public void fireBullet(GameTime gameTime, Vector2 bulletDir)
        {
            //construct new bullet, giving  position, direction
            //to do: prevent rapid fire
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedTime < fireSpeeds[currentFireSpeed % fireSpeeds.Length])
                return;
            elapsedTime = 0;
            Vector2 newPosition = new Vector2(this.Position.X + 8 * bulletDir.X, this.Position.Y + 8 * bulletDir.Y);
            Bullet b = new Bullet(newPosition, bulletDir);
            Game1.game.sceneObjects.Add(b);
            b.Load(Game1.game.Content, spriteBatch);
            Game1.PhysicsEngine.AddTrigger(b);
        }
    }
}