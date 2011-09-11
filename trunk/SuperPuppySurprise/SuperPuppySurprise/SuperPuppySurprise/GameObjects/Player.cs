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
        TestParticle2 testParticle;
        int currentFireSpeed = 2;
        int currentFireMode = 0;
        double[] fireSpeeds = {1000, 500, 300, 1};
        bool rotateHelper = true;
        double elapsedTime;

        public Player(int id, Vector2 Position)
            : base(Position)
        {
            Direction = Vector2.UnitY * -1;
            playerID = id;
            Speed = 300;
            elapsedTime = fireSpeeds[2];
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
            currentFireMode++;
            if (currentFireMode % 5 == 4)
                currentFireSpeed = 3;
            else
                currentFireSpeed = 1;
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
            {
                elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedTime < fireSpeeds[currentFireSpeed % fireSpeeds.Length])
                    return;
                elapsedTime = 0;
                if (currentFireMode % 5 == 0)
                    fireBullet(this.Position, bulletDir);
                //todo: no semi-auto shotgun!
                else if (currentFireMode % 5 == 1)
                    fireShotGun(this.Position, bulletDir);
                else if (currentFireMode % 5 == 2)
                    fireShotGun2(this.Position, bulletDir);
                else if (currentFireMode % 5 == 3)
                    fireBurst(this.Position, bulletDir);
                else if (currentFireMode % 5 == 4)
                    fireAutomatic(this.Position, bulletDir);
            }
            if (thisKeyState.IsKeyUp(fireUp) && thisKeyState.IsKeyUp(fireDown) &&
                thisKeyState.IsKeyUp(fireRight) && thisKeyState.IsKeyUp(fireLeft))
            {
                elapsedTime = fireSpeeds[currentFireSpeed % fireSpeeds.Length];
                if (currentFireMode % 5 == 0)
                    fireBullet(this.Position, bulletDir);
                //todo: no semi-auto shotgun!
                else if (currentFireMode % 5 == 1)
                    fireShotGun(this.Position, bulletDir);
                else if (currentFireMode % 5 == 2)
                    fireShotGun2(this.Position, bulletDir);
                else if (currentFireMode % 5 == 3)
                    fireBurst(this.Position, bulletDir);
                else if (currentFireMode % 5 == 4)
                    fireAutomatic(this.Position, bulletDir);
            }

            //this function is for testing purposes only, the player will switch weapons in game via power-ups
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
        }
        public override void Unload()
        {
            
            Game1.ParticleEngine.Remove(testParticle);
            GameState.players.Remove(this);
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
        public void fireBullet(Vector2 position, Vector2 bulletDir)
        {
            //construct new bullet, giving  position, direction
            Vector2 newPosition = new Vector2(position.X + 8 * bulletDir.X, position.Y + 8 * bulletDir.Y);
            Bullet b = new Bullet(newPosition, bulletDir);
            Game1.sceneObjects.Add(b);
            b.Load(Game1.game.Content, spriteBatch);
            Game1.PhysicsEngine.AddTrigger(b);
        }
        public override void OnDamage(double damage)
        {
            //Unload();
            base.OnDamage(damage);
        }
        public void fireShotGun(Vector2 position, Vector2 bulletDir)
        {
            currentFireSpeed = 1;
            Vector2 position1 = position;
            Vector2 position2 = position;
            position1.X += 12 * bulletDir.Y;
            position1.Y -= 12 * bulletDir.X;
            position2.X -= 12 * bulletDir.Y;
            position2.Y += 12 * bulletDir.X;
            fireBullet(position, bulletDir);
            fireBullet(position1, bulletDir);
            fireBullet(position2, bulletDir);
        }
        public void fireShotGun2(Vector2 position, Vector2 bulletDir)
        {
            currentFireSpeed = 1;
            Vector2 position1 = position;
            Vector2 position2 = position;
            position1.X += 6 * bulletDir.Y;
            position1.Y -= 6 * bulletDir.X;
            position2.X -= 6 * bulletDir.Y;
            position2.Y += 6 * bulletDir.X;
            Vector2 bulletDir1 = bulletDir;
            Vector2 bulletDir2 = bulletDir;
            bulletDir1.X += .2f * bulletDir.Y;
            bulletDir1.Y -= .2f * bulletDir.X;
            bulletDir2.X -= .2f * bulletDir.Y;
            bulletDir2.Y += .2f * bulletDir.X;
            //fireBullet(position, bulletDir);
            //fireBullet(position1, bulletDir1);
            //fireBullet(position2, bulletDir2);
            fireBurst(position, bulletDir);
            fireBurst(position1, bulletDir1);
            fireBurst(position2, bulletDir2);
        }
        public void fireBurst(Vector2 position, Vector2 bulletDir)
        {
            currentFireSpeed = 1;
            Vector2 position1 = position;
            Vector2 position2 = position;
            position1.X += 12 * bulletDir.X;
            position1.Y += 12 * bulletDir.Y;
            position2.X -= 12 * bulletDir.X;
            position2.Y -= 12 * bulletDir.Y;
            fireBullet(position, bulletDir);
            fireBullet(position1, bulletDir);
            fireBullet(position2, bulletDir);
        }
        public void fireAutomatic(Vector2 position, Vector2 bulletDir)
        {
            fireBullet(position, bulletDir);
        }
    }
}