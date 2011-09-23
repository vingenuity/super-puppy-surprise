using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SuperPuppySurprise.GameObjects;
using SuperPuppySurprise.GameMech;

namespace SuperPuppySurprise.Spawning
{
    public class BasicRunnerSpawn : Spawn
    {
        public static int lastDoor, lastType = 0;
        public BasicRunnerSpawn(int type): base()
        {
            lastType = type;
        }
        public delegate void CurrentSpawnPhase(GameTime gameTIme);
        double timer = 0;
        double spawn_delay = .5;
        bool fire1,fire2,fire3,fire4,fire5,fire6,fire7,fire8 = false;

        static private Vector2[] doors = new Vector2[4] {new Vector2(50, 250), new Vector2(450, 250), 
                                                         new Vector2(250, 50), new Vector2(250, 450)};

        public void Spawn1GroupOne(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            switch (lastType)
            {
                case 0:
                    
                    lastDoor = 2;
                    GameMechanics.TopLight = true;
                    GameMechanics.BottomLight = true;
                    if(!fire1)
                    {
                        spawnEnemy('r', 20, 0);
                        spawnEnemy('r', -20, 0);
                        fire1 = true;
                    }
                    if(!fire2 && timer > spawn_delay)
                    {
                        spawnEnemy('r', 0, 0);
                        fire2 = true;
                    }
                    if(!fire3 && timer > spawn_delay * 2)
                    {
                        spawnEnemy('r', 15, 0);
                        spawnEnemy('r', -15, 0);
                        fire3 = true;
                    }
                    if(!fire4 && timer > spawn_delay * 3)
                    {
                        spawnEnemy('r', 30, 0);
                        spawnEnemy('r', -30, 0);
                        lastDoor = 3;
                        fire4 = true;
                    }
                    lastDoor = 3;
                   
                    if(!fire5 && timer > spawn_delay * 4)
                    {
                        spawnEnemy('r', 20, 0);
                        spawnEnemy('r', -20, 0);
                        fire5 = true;
                    }
                    if(!fire6 && timer > spawn_delay  * 5)
                    {
                        spawnEnemy('r', 0, 0);
                        fire6 = true;
                    }
                    if(!fire7 && timer > spawn_delay * 6)
                    {
                        spawnEnemy('r', 15, 0);
                        spawnEnemy('r', -15, 0);
                        fire7 = true;
                    }
                    if(!fire8 && timer > spawn_delay * 7)
                    {
                        spawnEnemy('r', 30, 0);
                        spawnEnemy('r', -30, 0);
                        lastDoor = 3;
                        fire8 = true;
                    }
                    break;
                case 1:
                    lastDoor = 0;
                    GameMechanics.LeftLight = true;
                    GameMechanics.RightLight = true;
                    if(!fire1)
                    {
                        spawnEnemy('r', 20, 0);
                        spawnEnemy('r', -20, 0);
                        fire1 = true;
                    }
                    if(!fire2 && timer > spawn_delay)
                    {
                        spawnEnemy('r', 0, 0);
                        fire2 = true;
                    }
                    if(!fire3 && timer > spawn_delay * 2)
                    {
                        spawnEnemy('r', 15, 0);
                        spawnEnemy('r', -15, 0);
                        fire3 = true;
                    }
                    if(!fire4 && timer > spawn_delay * 3)
                    {
                        spawnEnemy('r', 30, 0);
                        spawnEnemy('r', -30, 0);
                        lastDoor = 3;
                        fire4 = true;
                    }
                    lastDoor = 1;
                    if(!fire5 && timer > spawn_delay * 4)
                    {
                        spawnEnemy('r', 20, 0);
                        spawnEnemy('r', -20, 0);
                        fire5 = true;
                    }
                    if(!fire6 && timer > spawn_delay  * 5)
                    {
                        spawnEnemy('r', 0, 0);
                        fire6 = true;
                    }
                    if(!fire7 && timer > spawn_delay * 6)
                    {
                        spawnEnemy('r', 15, 0);
                        spawnEnemy('r', -15, 0);
                        fire7 = true;
                    }
                    if(!fire8 && timer > spawn_delay * 7)
                    {
                        spawnEnemy('r', 30, 0);
                        spawnEnemy('r', -30, 0);
                        lastDoor = 3;
                        fire8 = true;
                        
                    }
                    break;
            }
        }
        List<Runner> ObjectsToSpawn = new List<Runner>();
        public void SpawnObjectsToSpawn()
        {
            Runner gameObject;
            for (int i = 0; i < ObjectsToSpawn.Count; i++)
            {
                gameObject = ObjectsToSpawn[i];
                if (!Game1.PhysicsEngine.CollidesWithAnotherObject(gameObject))
                {
                    gameObject.AddGameObjectToScene();
                    gameObject.Load();
                    ObjectsToSpawn.Remove(gameObject);
                }
                
            }
               
        }
        public void CheckIfDone()
        {
            if (fire8 == true && ObjectsToSpawn.Count == 0)
            {
                GameMechanics.BottomLight = false;
                GameMechanics.TopLight = false;
                GameMechanics.LeftLight = false;
                GameMechanics.RightLight = false;
                Game1.SpawnManager.Remove(this);
            }
        }
        public void spawnEnemy(char eType, int lrOff, int udOff)
        {
            Vector2 pos = doors[lastDoor];
            if (lastDoor < 2) //We are spawning on left or right doors
            {
                pos.X += udOff;
                pos.Y += lrOff;
            }
            else
            {
                pos.X += lrOff;
                pos.Y += udOff;
            }

            try
            {
                switch (eType)
                {
                    case 'r':
                        Runner r = new Runner(pos);
                        ObjectsToSpawn.Add(r);
                       
                        break;
                    case 's':
                        Shooter s = new Shooter(pos);
                        //ObjectsToSpawn.Add(s);
                        break;
                    default:
                        break;
                }
            }
            catch { }
        }
        public override void Update(GameTime gameTime)
        {
            Spawn1GroupOne(gameTime);
            SpawnObjectsToSpawn();
            CheckIfDone();
            base.Update(gameTime);
        }
    }
}
