using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperPuppySurprise.AIRoutines;
using SuperPuppySurprise.Spawning;
using SuperPuppySurprise.GameMech;

namespace SuperPuppySurprise.RoomManager
{
    public class Level2 : Room
    {
        public Level2()
            : base()
        {
            RoomNumber = 1;
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
        public void CheckIfProcede()
        {
            if (GameMechanics.Score > 2000)
            {
                Game1.RoomManager.ChangeRoom(new Level3());
            }
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            CheckIfProcede();
            SetSpawn();
            base.Update(gameTime);
        }

    }
}