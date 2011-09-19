using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SuperPuppySurprise;
using SuperPuppySurprise.GameObjects;
using System.Threading;

namespace SuperPuppySurprise
{
    public class Spawner
    {
        static bool areSpawning = false;
        static int lastDoor = 0, lastType = 0;
        static Int32 spawn_delay = 300;
        static Random rand = new Random();
        static AutoResetEvent autoEvent = new AutoResetEvent(false);

        static private Vector2[] doors = new Vector2[4] {new Vector2(50, 250), new Vector2(450, 250), 
                                                         new Vector2(250, 50), new Vector2(250, 450)};

        public Spawner() { }

        public void makeEnemy(char eType,int door, int xOffset, int yOffset)
        {
            Vector2 pos = doors[door];
            pos.X += xOffset;
            pos.Y += yOffset;
            
            switch(eType)
            {
                case 'r':
                    Runner r = new Runner(pos);
                    Game1.sceneObjects.Add(r);
                    r.Load();
                    break;
                case 's':
                    Shooter s = new Shooter(pos);
                    Game1.sceneObjects.Add(s);
                    s.Load(Game1.game.Content, Game1.spriteBatch);
                    break;
                default:
                    break;
            }
        }

        public void randSpawn()
        {
            if (!areSpawning)
            {
                setSpawn(rand.Next(0, 2));
            }
        }
        public void setSpawn(int type)
        {
            lastType = type;
            if (!areSpawning)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(spawn), autoEvent);
            }
        }

        public static void spawn(object stateInfo)
        {
            areSpawning = true;
            Thread.Sleep(2000);
            switch (lastType)
            {
                case 0:
                    lastDoor = 2;
                    spawnEnemy('r', 0, 0);
                    Thread.Sleep(spawn_delay);
                    spawnEnemy('r', 20, 0);
                    spawnEnemy('r', -20, 0);
                    Thread.Sleep(spawn_delay);
                    spawnEnemy('r', 0, 0);
                    Thread.Sleep(spawn_delay);
                    spawnEnemy('r', 15, 0);
                    spawnEnemy('r', -15, 0);
                    Thread.Sleep(spawn_delay);
                    spawnEnemy('r', 30, 0);
                    spawnEnemy('r', -30, 0);

                    lastDoor = 3;
                    spawnEnemy('r', 0, 0);
                    Thread.Sleep(spawn_delay);
                    spawnEnemy('r', 20, 0);
                    spawnEnemy('r', -20, 0);
                    Thread.Sleep(spawn_delay);
                    spawnEnemy('r', 0, 0);
                    Thread.Sleep(spawn_delay);
                    spawnEnemy('r', 15, 0);
                    spawnEnemy('r', -15, 0);
                    Thread.Sleep(spawn_delay);
                    spawnEnemy('r', 30, 0);
                    spawnEnemy('r', -30, 0);
                    break;
                case 1:
                    lastDoor = 0;
                    spawnEnemy('r', 0, 0);
                    Thread.Sleep(spawn_delay);
                    spawnEnemy('r', 20, 0);
                    spawnEnemy('r', -20, 0);
                    Thread.Sleep(spawn_delay);
                    spawnEnemy('r', 0, 0);
                    Thread.Sleep(spawn_delay);
                    spawnEnemy('r', 15, 0);
                    spawnEnemy('r', -15, 0);
                    Thread.Sleep(spawn_delay);
                    spawnEnemy('r', 30, 0);
                    spawnEnemy('r', -30, 0);

                    lastDoor = 1;
                    spawnEnemy('r', 0, 0);
                    Thread.Sleep(spawn_delay);
                    spawnEnemy('r', 20, 0);
                    spawnEnemy('r', -20, 0);
                    Thread.Sleep(spawn_delay);
                    spawnEnemy('r', 0, 0);
                    Thread.Sleep(spawn_delay);
                    spawnEnemy('r', 15, 0);
                    spawnEnemy('r', -15, 0);
                    Thread.Sleep(spawn_delay);
                    spawnEnemy('r', 30, 0);
                    spawnEnemy('r', -30, 0);
                    break;
                default:
                    break;
            }
            areSpawning = false;
            ((AutoResetEvent)stateInfo).Set();

        }

        public static void spawnEnemy(char eType, int lrOff, int udOff)
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
                        while (Game1.PhysicsEngine.CollidesWithAnotherObject(r))
                        {
                            Thread.Sleep(100);
                        }
                        r.AddGameObjectToScene();
                        r.Load();
                        break;
                    case 's':
                        Shooter s = new Shooter(pos);
                        while (Game1.PhysicsEngine.CollidesWithAnotherObject(s))
                        {
                            Thread.Sleep(100);
                        }
                        s.AddGameObjectToScene();
                        s.Load(Game1.game.Content, Game1.spriteBatch);
                        break;
                    default:
                        break;
                }
            }
            catch { }
        }
    }
}
