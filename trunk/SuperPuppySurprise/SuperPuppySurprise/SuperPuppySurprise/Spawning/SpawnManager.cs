using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SuperPuppySurprise.AIRoutines;

namespace SuperPuppySurprise.Spawning
{
    public class SpawnManager
    {
        public List<Spawn> SpawnList;
        public SpawnManager()
        {
            SpawnList = new List<Spawn>();
        }
        static int lastDoor = 0, lastType = 0;
        static Int32 spawn_delay = 300;
        static Random rand = new Random();

        static private Vector2[] doors = new Vector2[4] {new Vector2(50, 250), new Vector2(450, 250), 
                                                         new Vector2(250, 50), new Vector2(250, 450)};
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < SpawnList.Count; i++)
            {
                SpawnList[i].Update(gameTime);
            }
           
        }
       
        public void Add(Spawn s)
        {
            SpawnList.Add(s);
        }
        public void Remove(Spawn s)
        {
            SpawnList.Remove(s);
        }
    }
}
