﻿using System;
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
        static int lastDoor = 0, lastType = 0;
        static Int32 spawn_delay = 1000;
        static Int32 delay = 10;
        static Random rand = new Random();
        static AutoResetEvent autoEvent = new AutoResetEvent(false);

        static private Vector2[] doors = new Vector2[4] {new Vector2(50, 250), new Vector2(250, 50), 
                                                  new Vector2(450, 250), new Vector2(250, 450)};

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
                    r.Load(Game1.game.Content, Game1.spriteBatch);
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
            setSpawn(rand.Next(0, 4), 0);
        }

        public void spawn(int door, int type)
        {
            switch (type)
            {
                case 0:
                    Thread.Sleep(5000);
                    makeEnemy('r', door, 0, 0);
                    Thread.Sleep(spawn_delay);
                    makeEnemy('r', door, -10, 0);
                    makeEnemy('r', door, 10, 0);
                    Thread.Sleep(spawn_delay);
                    makeEnemy('r', door, 0, 0);
                    break;
                case 1:
                    break;
                default:
                    break;
            }
        }
        public void setSpawn(int door, int type)
        {
            lastDoor = door;
            lastType = type;

            ThreadPool.QueueUserWorkItem(new WaitCallback(spawn),
            autoEvent);

            //Thread sThread = new Thread(spawn);
            //sThread.Start();
        }

        public static void spawn(object stateInfo)
        {
            switch (lastType)
            {
                case 0:

                    spawnRunner();
                    spawnRunner();
                    spawnRunner();
                    /*
                    Runner r2 = new Runner(doors[lastDoor]);
                    Game1.sceneObjects.Add(r2);
                    r2.Load(Game1.game.Content, Game1.spriteBatch);
                    Thread.Sleep(spawn_delay);
                    Runner r3 = new Runner(doors[lastDoor]);
                    Game1.sceneObjects.Add(r3);
                    r3.Load(Game1.game.Content, Game1.spriteBatch);
                    */
                    break;
                case 1:
                    break;
                default:
                    break;
            }
            ((AutoResetEvent)stateInfo).Set();

        }
        public static void spawnRunner()
        {
            Runner r = new Runner(doors[lastDoor]);
            try
            {

                while (Game1.PhysicsEngine.CollidesWithAnotherObject(r))
                {
                    Thread.Sleep(100);
                }
                r.AddGameObjectToScene();
                r.Load(Game1.game.Content, Game1.spriteBatch);

                Thread.Sleep(spawn_delay);
            }
            catch{}
        }
    }
}
