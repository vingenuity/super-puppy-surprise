using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SuperPuppySurprise.RoomManager;

namespace SuperPuppySurprise.GameObjects
{
    public enum RoomType
    {
        Level1,
        Level2,
        Level3,
        Level4,
    };
    public class EnterNextRoomTrigger : GameObject
    {

        Texture2D texture;
        SpriteBatch spriteBatch;
        Color c = Color.White;
        RoomType name;
        public EnterNextRoomTrigger(RoomType name)
            : base(new Vector2(400,300))
        {
            this.name = name;
            Size = new Vector2(30, 200);
            Radius = 8;
            Game1.PhysicsEngine.AddTrigger(this);
            Game1.sceneObjects.Add(this);
            Load(Game1.game.Content, Game1.spriteBatch);
        }

        public override void Load(ContentManager Content, SpriteBatch spriteBatch)
        {
            texture = Content.Load<Texture2D>("TestPicture2");
            this.spriteBatch = spriteBatch;
            base.Load(Content, spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(GameTime gameTime)
        {
            Rectangle r = new Rectangle((int)(Position.X - Size.X / 2), (int)(Position.Y - Size.Y / 2), (int)Size.X, (int)Size.Y);
            spriteBatch.Draw(texture, r, c);
            base.Draw(gameTime);

        }
        void nextRoom()
        {
            if(name == RoomType.Level1)
                Game1.RoomManager.ChangeRoom(new Level2());
            if (name == RoomType.Level2)
                Game1.RoomManager.ChangeRoom(new Level3());
            if (name == RoomType.Level3)
                Game1.RoomManager.ChangeRoom(new Level4());
         // if (name == RoomType.Level4)
         //       Game1.RoomManager.ChangeRoom(new Level2());
        }
        public override void OnCollision(GameObject gameObject)
        {
            if (gameObject is Player)
            {
                ((Player)(gameObject)).Position = new Vector2(250 - 16, 250 - 16);
                ((Player)(gameObject)).goingToRoom = false;
                nextRoom();
            }
            Game1.PhysicsEngine.Remove(this);
            Game1.sceneObjects.Remove(this);
            base.OnCollision(gameObject);
        }
    }
}
