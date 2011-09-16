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
        int lastDoor = 0, lastType = 0;
        Int32 spawn_delay = 1000;
        Random rand = new Random();

        private Vector2[] doors = new Vector2[4] {new Vector2(50, 250), new Vector2(250, 50), 
                                                        new Vector2(450, 250), new Vector2(250, 450)};

        public Spawner()
        {
        }
        double time;
        public void delayedRandSpawn()
        {
            if(!needsToSpawn)
                time = 1;
            needsToSpawn = true;
        }
        public void randSpawn()
        {
            int nextSpawnLocation = rand.Next(0, 4);
            spawn(nextSpawnLocation, 0, 0 ,0);
            spawn(nextSpawnLocation, 0,25,25);
            spawn(nextSpawnLocation, 0, -25, -25);
            spawn(nextSpawnLocation, 0, 25, -25);
            spawn(nextSpawnLocation, 0, -25, 25);
            //setSpawn(rand.Next(0, 4), 0);
        }
        public void spawn(int door, int type, int offSetX, int offsetY)
        {
            switch (type)
            {
                case 0:
                    Vector2 pos = doors[door];
                    pos.X += offSetX;
                    pos.Y += offsetY;
                    Runner r = new Runner(pos);
                    Game1.sceneObjects.Add(r);
                    r.Load(Game1.game.Content, Game1.spriteBatch);
                    break;
                case 1:
                    break;
                default:
                    break;
            }
        }
        bool needsToSpawn = false;
        public void Update(GameTime gameTime)
        {
            time -= gameTime.ElapsedGameTime.TotalSeconds;
            if (time < 0 && needsToSpawn)
            {
                needsToSpawn = false;
                randSpawn();
            }
        }
        public void setSpawn(int door, int type)
        {
            lastDoor = door;
            lastType = type;
            Thread sThread = new Thread(spawn);
            sThread.Start();
        }

        public void spawn()
        {
            switch (lastType)
            {
                case 0:
                    Runner r = new Runner(doors[lastDoor]);
                    Game1.sceneObjects.Add(r);
                    r.Load(Game1.game.Content, Game1.spriteBatch);
                    Thread.Sleep(spawn_delay);
                    Runner r2 = new Runner(doors[lastDoor]);
                    Game1.sceneObjects.Add(r2);
                    r2.Load(Game1.game.Content, Game1.spriteBatch);
                    Thread.Sleep(spawn_delay);
                    Runner r3 = new Runner(doors[lastDoor]);
                    Game1.sceneObjects.Add(r3);
                    r3.Load(Game1.game.Content, Game1.spriteBatch);
                    break;
                case 1:
                    break;
                default:
                    break;
            }
        }
    }
}
