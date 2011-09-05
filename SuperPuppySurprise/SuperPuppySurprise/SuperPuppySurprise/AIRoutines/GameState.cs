using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SuperPuppySurprise.GameObjects;

namespace SuperPuppySurprise.AIRoutines
{
    public class GameState
    {
        public static List<Player> players;

        public GameState()
        {
            players = new List<Player>();
        }

        public void clearPlayers()
        {
            players.Clear();
        }

        public static Player findClosestPlayerTo(Vector2 position)
        {
            double closestDistance = Math.Sqrt(Math.Pow(players[0].getPosition().X - position.X, 2)
                                          + Math.Pow(players[0].getPosition().Y - position.Y, 2)); ;
            Player closestPlayer = players[0];
            for (int i = 1; i < players.Count(); i++)
            {
                double distance = Math.Sqrt(Math.Pow(players[i].getPosition().X - position.X, 2)
                                          + Math.Pow(players[i].getPosition().Y - position.Y, 2));
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPlayer = players[i];
                }
            }
            return closestPlayer;
        }
    }
}
