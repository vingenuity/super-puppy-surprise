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
        public static List<Monster> enemies;

        public GameState()
        {
            players = new List<Player>();
            enemies = new List<Monster>();
        }

        public void clearEnemies()
        {
            enemies.Clear();
        }
        public void clearPlayers()
        {
            players.Clear();
        }

        public static bool arePlayers()
        {
            if (players.Count == 0)
                return false;
            return true;
        }

        private static double distanceTo(Vector2 position, GameObject gameObj)
        {
            return Math.Sqrt(Math.Pow(gameObj.getPosition().X - position.X, 2)
                           + Math.Pow(gameObj.getPosition().Y - position.Y, 2));
        }

        public static Player findClosestPlayerTo(Vector2 position)
        {
            double closestDistance = distanceTo(position, players[0]);
            Player closestPlayer = players[0];
            for (int i = 0; i < players.Count(); i++)
            {
                double distance = distanceTo(position, players[i]);
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
