using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperPuppySurprise.AIRoutines;
using SuperPuppySurprise.Spawning;

namespace SuperPuppySurprise.RoomManager
{
    public class Level1 : Room
    {
        public Level1(): base()
        {
        }
        Random rand = new Random();
        void SetSpawn()
        {
            if (Game1.SpawnManager.SpawnList.Count == 0 && GameState.enemies.Count == 0)
            {
                setSpawn(rand.Next(0, 2));
            }
        }
        void setSpawn(int a)
        {
            Game1.SpawnManager.Add(new BasicRunnerSpawn(a));
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            SetSpawn();
            base.Update(gameTime);
        }

    }
}
