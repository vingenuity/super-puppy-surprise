using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SuperPuppySurprise;
using SuperPuppySurprise.GameObjects;

namespace SuperPuppySurprise
{
    public class Spawner
    {
        double elapsedTime;
        int lastDoor = 0, lastType = 0;
        Random rand = new Random();

        private Vector2[] doors = new Vector2[4] {new Vector2(50, 250), new Vector2(250, 50), 
                                                        new Vector2(450, 250), new Vector2(250, 450)};

        public Spawner()
        {
        }

        public void randSpawn()
        {
            spawn(rand.Next(0, 4), 0);
        }

        public void spawn(int door, int type)
        {
            switch (type)
            {
                case 0:
                    Runner r = new Runner(doors[door]);
                    Game1.sceneObjects.Add(r);
                    r.Load(Game1.game.Content, Game1.spriteBatch);
                    break;
                case 1:
                    break;
                default:
                    break;
            }
        }
    }
}
