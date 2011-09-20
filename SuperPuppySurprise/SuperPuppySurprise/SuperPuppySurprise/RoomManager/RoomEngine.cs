using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SuperPuppySurprise.RoomManager
{
    public class RoomEngine
    {
        Room room;
        public RoomEngine()
        {
            room = new Level1();
        }
        public void Update(GameTime gameTime)
        {
            room.Update(gameTime);
        }
    }
}
