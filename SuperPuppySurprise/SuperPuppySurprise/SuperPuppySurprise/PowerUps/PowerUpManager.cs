using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SuperPuppySurprise.PowerUps
{
    public class PowerUpManager
    {
        List<PowerUp> PowerUpList;
        public PowerUpManager()
        {
            PowerUpList = new List<PowerUp>();
        }
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < PowerUpList.Count; i++)
            {
                PowerUpList[i].Update(gameTime);
            }
        }
        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < PowerUpList.Count; i++)
            {
                PowerUpList[i].Draw(gameTime);
            }
        }
        public void Add(PowerUp powerUp)
        {
            PowerUpList.Add(powerUp);
        }
        public void Remove(PowerUp powerUp)
        {
            PowerUpList.Remove(powerUp);
        }
    }
}
