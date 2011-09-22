using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SuperPuppySurprise.GameMech
{
    public class GameMechanics
    {
        public static int Score;
        public static int LeftWallBound = 80;
        public static int RightWallBound = 420;
        public static int TopWallBound = 80;
        public static int BottomWallBound = 420;
        public static int RoomDoorPositionX = 250;
        public static int RoomDoorPositionY = 250;
        public static int RoomNumber = 0;
        public static bool LeftLight = false;
        public static bool RightLight = false;
        public static bool TopLight = false;
        public static bool BottomLight = false;       
    }
}
