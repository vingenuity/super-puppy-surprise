using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperPuppySurprise.AIRoutines;
using SuperPuppySurprise.Spawning;
using SuperPuppySurprise.GameMech;
using SuperPuppySurprise.GameObjects;
using GameStateManagement;
using Microsoft.Xna.Framework;

namespace SuperPuppySurprise.RoomManager
{
    public class Level5 : Room
    {
        public Level5()
            : base()
        {
            RoomNumber = 4;
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
                        Game1.screenManager.AddScreen(new VictoryDefeatScreen("FREEEEEDOOOOOMMMM!!!!!!!!!!!!!!!"),PlayerIndex.One);
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