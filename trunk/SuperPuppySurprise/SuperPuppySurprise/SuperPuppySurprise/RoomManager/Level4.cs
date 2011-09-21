using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperPuppySurprise.AIRoutines;
using SuperPuppySurprise.Spawning;
using SuperPuppySurprise.GameMech;

namespace SuperPuppySurprise.RoomManager
{
    public class Level4 : Room
    {
        public Level4()
            : base()
        {
            RoomNumber = 3;
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
           // if (GameMechanics.Score > 600)
           // {
           //     Game1.RoomManager.ChangeRoom(new Level2());
           // }
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            CheckIfProcede();
            SetSpawn();
            base.Update(gameTime);
        }

    }
}