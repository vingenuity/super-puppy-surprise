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
        public static List<Monster> enemies;
        public static List<Player> players;
        public static List<GameObject> gameObjectAddList = new List<GameObject>();

        public GameState()
        {
            enemies = new List<Monster>();
            players = new List<Player>();
        }
        public static void Update(GameTime gameTime)
        {

        }
        public static void clearEnemies()
        {
            enemies.Clear();
        }
        public static void clearPlayers()
        {
            players.Clear();
        }

        public static bool noEnemies()
        {
            if (enemies.Count == 0)
                return true;
            return false;
        }

        public static bool noPlayers()
        {
            if (players.Count == 0)
                return true;
            return false;
        }

        private static double distanceTo(Vector2 position, GameObject gameObj)
        {
            return Math.Sqrt(Math.Pow(gameObj.getPosition().X - position.X, 2)
                           + Math.Pow(gameObj.getPosition().Y - position.Y, 2));
        }

        public static Vector2 findClosestPlayerTo(Vector2 position)
        {
            if (noPlayers())
                return new Vector2(250, 250);
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
            return closestPlayer.getPosition();
        }
    }
}
