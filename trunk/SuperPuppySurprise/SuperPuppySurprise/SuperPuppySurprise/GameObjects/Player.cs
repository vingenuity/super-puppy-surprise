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
using SuperPuppySurprise.Sounds;
using GameStateManagement;

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
        double[] fireSpeeds = { 800, 500, 300, 1 };
        public int[] rounds = { 0, 20, 20, 20, 20 };
        bool rotateHelper = true;
        double elapsedTime;
        Random random;
        GamePadState gamePadState;

        public Player(int id, Vector2 Position)
            : base(Position)
        {
            random = new Random();
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
                default:
                    //This is an error, technically.
                    break;
            }
            testParticle = new TestParticle2(this);
            testParticle.Start();
            Game1.SoundEngine.StartHover();
        }
        public bool goingToRoom = false;
        Vector2 PositionToGoTo;
        public void GoToRoom(int x, int y)
        {
            goingToRoom = true;
            PositionToGoTo = new Vector2(x, y);
        }
        void EngineParticle()
        {
            /*
            if (Velocity.LengthSquared() > 0)
                testParticle.ChangeStatus(true);
            if(Velocity.LengthSquared() == 0)
                testParticle.ChangeStatus(false);*/
        }
        Texture2D turretTexture;
        public override void Load(ContentManager Content, SpriteBatch spriteBatch)
        {
            //texture = Content.Load<Texture2D>("TestPicture2");
            texture = Content.Load<Texture2D>("asset_char_base");
            turretTexture = Content.Load<Texture2D>("asset_char_turret");
            Orig = new Vector2(turretTexture.Width / 2, turretTexture.Height / 2);
            scale = (float)(Size.X/(turretTexture.Width * 1.0)); 
            this.spriteBatch = spriteBatch;
            base.Load(Content, spriteBatch);
        }
        public void ChangeWeapon(int weapon)
        {
            currentFireMode = weapon;
            if (currentFireMode % 5 == 4)
                currentFireSpeed = 3;
            else
                currentFireSpeed = 2;
        }
        public void rotateWeapons()
        {
            Game1.SoundEngine.PlaySound(SoundEffects.weaponchange);
            currentFireMode++;
            if (currentFireMode % 5 == 1)
                currentFireMode++;
            if (currentFireMode % 5 == 4)
                currentFireSpeed = 3;
            else
                currentFireSpeed = 2;
        }
        public void setFireMode(int mode)
        {
            currentFireMode = mode;
            if (currentFireMode % 5 == 4)
                currentFireSpeed = 3;
            else
                currentFireSpeed = 2;
        }
        public void setVelocityFromKeyBoard()
        {
            Direction = Vector2.Zero;

            thisKeyState = Keyboard.GetState();
            if (thisKeyState.IsKeyDown(downKey))
                Direction.Y++;
            else if (thisKeyState.IsKeyDown(upKey))
                Direction.Y--;
            if (thisKeyState.IsKeyDown(rightKey))
                Direction.X++;
            else if (thisKeyState.IsKeyDown(leftKey))
                Direction.X--;

            Direction.Normalize();

            if (thisKeyState.IsKeyUp(leftKey) && thisKeyState.IsKeyUp(rightKey) &&
                thisKeyState.IsKeyUp(upKey) && thisKeyState.IsKeyUp(downKey))
                Velocity = Vector2.Zero;
            else
                Velocity = Direction * Speed;
        }
        public void setVelocityFromGamePad()
        {
            Direction = Vector2.Zero;

            Direction.X = gamePadState.ThumbSticks.Left.X;
            Direction.Y = -1 * gamePadState.ThumbSticks.Left.Y;

            if (gamePadState.ThumbSticks.Left.X == 0 &&
               gamePadState.ThumbSticks.Left.Y == 0)
                Velocity = Vector2.Zero;
            else
                Velocity = Direction * Speed;

        }
        public Vector2 getBulletDirectionFromKeyBoard()
        {
            Vector2 tempDir;
            tempDir = Vector2.Zero;

            if (thisKeyState.IsKeyDown(fireDown))
                tempDir.Y++;
            else if (thisKeyState.IsKeyDown(fireUp))
                tempDir.Y--;
            if (thisKeyState.IsKeyDown(fireRight))
                tempDir.X++;
            else if (thisKeyState.IsKeyDown(fireLeft))
                tempDir.X--;
            tempDir.Normalize();

            return tempDir;
        }
        public Vector2 getBulletDirectionFromGamePad()
        {
            Vector2 tempDir;
            tempDir = Vector2.Zero;

            tempDir.X = gamePadState.ThumbSticks.Right.X;
            tempDir.Y = -1 * gamePadState.ThumbSticks.Right.Y;

            tempDir.Normalize();

            return tempDir;
        }
        Vector2 bulletDir;
        public override void Update(GameTime gameTime)
        {

            gamePadState = GamePad.GetState(PlayerIndex.One);
            thisKeyState = Keyboard.GetState();
            bulletDir = Vector2.UnitY;

            //Update movement
            Direction = Vector2.Zero;

            if (!gamePadState.IsConnected)
                setVelocityFromKeyBoard();
            else
            {
                setVelocityFromGamePad();
                Game1.SoundEngine.HoverPitch(Velocity.LengthSquared());                
            }


            if (!gamePadState.IsConnected)
                bulletDir = getBulletDirectionFromKeyBoard();
            else
                bulletDir = getBulletDirectionFromGamePad();

            //fire!
            if (thisKeyState.IsKeyDown(fireUp) || thisKeyState.IsKeyDown(fireDown) ||
                thisKeyState.IsKeyDown(fireRight) || thisKeyState.IsKeyDown(fireLeft) ||
                ((gamePadState.Triggers.Right > 0 || gamePadState.Triggers.Left > 0)
                && bulletDir.Length() > 0.0f))
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

            //allows for button mashing
            if (!gamePadState.IsConnected)
                if (thisKeyState.IsKeyUp(fireUp) && thisKeyState.IsKeyUp(fireDown) &&
                    thisKeyState.IsKeyUp(fireRight) && thisKeyState.IsKeyUp(fireLeft))
                    elapsedTime = fireSpeeds[currentFireSpeed % fireSpeeds.Length];
            if (gamePadState.IsConnected)
                if (gamePadState.Triggers.Right == 0 && gamePadState.Triggers.Left == 0)
                    elapsedTime = fireSpeeds[currentFireSpeed % fireSpeeds.Length];

            if (gamePadState.Buttons.A == ButtonState.Pressed || thisKeyState.IsKeyDown(Keys.D4))
                setFireMode(4);
            if (gamePadState.Buttons.B == ButtonState.Pressed || thisKeyState.IsKeyDown(Keys.D3))
                setFireMode(3);
            if (gamePadState.Buttons.X == ButtonState.Pressed || thisKeyState.IsKeyDown(Keys.D2))
                setFireMode(2);
            if (gamePadState.Buttons.Y == ButtonState.Pressed || thisKeyState.IsKeyDown(Keys.D1))
                setFireMode(0);

            //this function is for testing purposes only
            //the player will switch weapons in game via power-ups
            if ((thisKeyState.IsKeyDown(Keys.Tab) ||
                gamePadState.Buttons.RightShoulder == ButtonState.Pressed ||
                gamePadState.Buttons.LeftShoulder == ButtonState.Pressed)
                && rotateHelper)
            {
                rotateWeapons();
                rotateHelper = false;
            }

            if (thisKeyState.IsKeyUp(Keys.Tab) || gamePadState.Buttons.RightShoulder == ButtonState.Pressed)
                rotateHelper = true;

            if (thisKeyState.IsKeyDown(Keys.O) || gamePadState.Buttons.BigButton == ButtonState.Pressed)
            {
                Unload();
            }

            CalculateRotation();

            if (goingToRoom)
            {
                Direction = PositionToGoTo - Position;
                Direction.Normalize();
                Velocity = Speed * Direction;
            }

            Game1.SoundEngine.PlayerVelocity = Velocity;

            EngineParticle();
        }
        public override void Unload()
        {
            Game1.SoundEngine.StopHover();
            Game1.screenManager.AddScreen(new VictoryDefeatScreen("DOOOOOOOOOOOOOOOOOOOOOM!!!!!!!!!!!!!!!!!!", "You have escaped!"), PlayerIndex.One);
            /*
            Game1.ParticleEngine.Remove(testParticle);
            GameState.players.Remove(this);
            Game1.PhysicsEngine.Remove(this);
            Game1.game.RemoveGameObject(this);*/
            base.Unload();
        }
        public static float TurretRotationAmount;
        
        void CalculateRotation()
        {

            if (bulletDir.LengthSquared() > 0)
            {
                TurretRotationAmount = (float)Math.Atan2(bulletDir.X, -bulletDir.Y);
            }

        }

        Vector2 Orig;
        float scale; 
        public override void Draw(GameTime gameTime)
        {
           
           
            Rectangle r = new Rectangle((int)(Position.X - Size.X / 2), (int)(Position.Y - Size.Y / 2), (int)Size.X, (int)Size.Y);
            spriteBatch.Draw(texture, r, null, Color.White, 0, (Size), SpriteEffects.None, 0);
            spriteBatch.Draw(turretTexture, ((Position)), null, Color.White, TurretRotationAmount, Orig,scale, SpriteEffects.None, 0);
            base.Draw(gameTime);
        }
        public void fireBullet(Vector2 position, Vector2 bulletDir)
        {
            //construct new bullet, giving  position, direction
            Vector2 newPosition = new Vector2(position.X + 8 * bulletDir.X, position.Y + 8 * bulletDir.Y);
            Bullet b = new Bullet(newPosition, bulletDir);
            Game1.sceneObjects.Add(b);
            b.Load(Game1.game.Content, spriteBatch);
            //Game1.PhysicsEngine.AddTrigger(b);
            Game1.SoundEngine.PlaySound(SoundEffects.shoot);
        }
        public override void OnDamage(double damage)
        {
            //Unload();
            base.OnDamage(damage);
        }
        private Vector2 position1(Vector2 position, Vector2 bulletDir)
        {
            position.X += 8 * bulletDir.Y;
            position.Y -= 8 * bulletDir.X;
            return position;
        }
        private Vector2 position2(Vector2 position, Vector2 bulletDir)
        {
            position.X -= 8 * bulletDir.Y;
            position.Y += 8 * bulletDir.X;
            return position;
        }
        private void fireShotGun(Vector2 position, Vector2 bulletDir)
        {
            if (rounds[1] == 0)
            {
                currentFireMode = 0;
                Game1.SoundEngine.PlaySound(SoundEffects.weaponchange);
                return;
            }
            currentFireSpeed = 1;
            fireBullet(position, bulletDir);
            fireBullet(position1(position, bulletDir), bulletDir);
            fireBullet(position2(position, bulletDir), bulletDir);
            rounds[1]--;
        }
        private void fireShotGun2(Vector2 position, Vector2 bulletDir)
        {
            if (rounds[2] == 0)
            {
                currentFireMode = 0;
                Game1.SoundEngine.PlaySound(SoundEffects.weaponchange);
                return;
            }
            currentFireSpeed = 1;
            Vector2 bulletDir1 = bulletDir;
            Vector2 bulletDir2 = bulletDir;
            bulletDir1.X += .2f * bulletDir.Y;
            bulletDir1.Y -= .2f * bulletDir.X;
            bulletDir2.X -= .2f * bulletDir.Y;
            bulletDir2.Y += .2f * bulletDir.X;
            Vector2 position0;
            position0.X = position.X + 8 * bulletDir.X;
            position0.Y = position.Y + 8 * bulletDir.Y;
            Vector2 position3;
            position3.X = position.X - 4 * bulletDir.X;
            position3.Y = position.Y - 4 * bulletDir.Y;
            Vector2 position4;
            position4.X = position.X - 4 * bulletDir.X;
            position4.Y = position.Y - 4 * bulletDir.Y;
            Vector2 position5;
            position5.X = position.X - 8 * bulletDir.X;
            position5.Y = position.Y - 8 * bulletDir.Y;
            //can be changed to fireBurst()
            fireBullet(position0, bulletDir);
            fireBullet(position1(position, bulletDir), bulletDir1);
            fireBullet(position2(position, bulletDir), bulletDir2);
            fireBullet(position3, bulletDir1);
            fireBullet(position4, bulletDir2);
            fireBullet(position5, bulletDir);
            rounds[2]--;
        }
        private void fireBurst(Vector2 position, Vector2 bulletDir)
        {
            if (rounds[3] == 0)
            {
                currentFireMode = 0;
                Game1.SoundEngine.PlaySound(SoundEffects.weaponchange);
                return;
            }
            currentFireSpeed = 1;
            Vector2 position1;
            position1.X = position.X + 8 * bulletDir.X;
            position1.Y = position.Y + 8 * bulletDir.Y;
            Vector2 position2;
            position2.X = position.X - 8 * bulletDir.X;
            position2.Y = position.Y - 8 * bulletDir.Y;
            fireBullet(position, bulletDir);
            fireBullet(position1, bulletDir);
            fireBullet(position2, bulletDir); 
            rounds[3]--;
        }
        public float getRandomAdjust()
        {
            int randomNumber = random.Next(0, 10);
            randomNumber -= 5;
            float randomAdjust = (float)randomNumber * 0.01f;
            return randomAdjust;
        }
        private void fireAutomatic(Vector2 position, Vector2 bulletDir)
        {
            if (rounds[4] == 0)
            {
                currentFireMode = 0;
                Game1.SoundEngine.PlaySound(SoundEffects.weaponchange);
                currentFireSpeed = 2;
                return;
            }
            bulletDir.X += getRandomAdjust();
            bulletDir.Y += getRandomAdjust();
            fireBullet(position, bulletDir);
            rounds[4]--;
        }
        public override void OnCollision(GameObject gameObject)
        {
            base.OnCollision(gameObject);
        }
    }
}