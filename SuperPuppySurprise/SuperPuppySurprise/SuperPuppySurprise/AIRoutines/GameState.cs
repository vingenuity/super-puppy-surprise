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
        public static Player[] players;

        public GameState()
        {
            players = new Player[4];
        }

        public static Vector2 findClosestPlayer()
        {
            return players[0].getPosition();
        }
    }
}
