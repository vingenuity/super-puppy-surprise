using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperPuppySurprise.AIRoutines;
using SuperPuppySurprise.Spawning;
using SuperPuppySurprise.GameMech;
using SuperPuppySurprise.GameObjects;

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
            Game1.hud.ShowLevelClearedText = false;
            if (GameMechanics.Score > 500)
            {
                StillSpawning = false;
                if (GameState.enemies.Count == 0 && Game1.SpawnManager.SpawnList.Count == 0)
                {
                    Game1.hud.ShowLevelClearedText = true;
                    if (!NextLevel)
                    {
                        NextLevel = true;
                        new EnterNextRoomTrigger(RoomType.Level2);
                        
                        for (int i = 0; i < GameState.players.Count; i++)
                            GameState.players[i].GoToRoom(400, 250);
                    }
                }
            }
            
        }
        bool StillSpawning = true;
        bool NextLevel = false;
        double preloadingTime = 5;
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (preloadingTime > 0)
            {
                preloadingTime -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                CheckIfProcede();
                if (StillSpawning)
                    SetSpawn();
            }
            base.Update(gameTime);
        }

    }
}