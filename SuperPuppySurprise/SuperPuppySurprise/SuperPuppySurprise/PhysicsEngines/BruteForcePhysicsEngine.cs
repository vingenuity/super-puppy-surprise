using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperPuppySurprise.GameObjects;
using Microsoft.Xna.Framework;
using SuperPuppySurprise.GameMech;

namespace SuperPuppySurprise.PhysicsEngines
{
    public class BruteForcePhysicsEngine
    {
        List<GameObject> PhysicsGameObjects;
        List<GameObject> PhysicsGameObjectTriggers;
        List<GameObject> gameObjectAddList;


        public BruteForcePhysicsEngine()
        {
            Reset();
        }
        public void Reset()
        {
            PhysicsGameObjects = new List<GameObject>();
            PhysicsGameObjectTriggers = new List<GameObject>();
            gameObjectAddList = new List<GameObject>();
        }
        public void Update(GameTime gameTime)
        {
            double time = gameTime.ElapsedGameTime.TotalSeconds;
            UpdateMovement(time);
            UpdateTriggers(time);
        }
       
        /// <summary>
        /// Currently Only tests for TiggerVSRegular game object collisions
        /// </summary>
        /// <param name="time"></param>
        void UpdateTriggers(double time)
        {

            GameObject gameObject;
            GameObject trigger;

            for (int i = 0; i < PhysicsGameObjectTriggers.Count; i++)
            {
                trigger = PhysicsGameObjectTriggers[i];
                for (int j = 0; j < PhysicsGameObjects.Count; j++)
                {
                    gameObject = PhysicsGameObjects[j];
                    if (CollidesSquares(gameObject, trigger))
                        trigger.OnCollision(gameObject);
                }
                trigger.Position += trigger.Velocity * (float)time;
            }
        }
        public bool CollidesWithAnotherObject(GameObject s)
        {

            GameObject gameObject;
            for (int i = 0; i < PhysicsGameObjects.Count; i++)
            {
                gameObject = PhysicsGameObjects[i];
                if (CollidesSquares(gameObject, s) && gameObject != s)
                    return true;
            }
            return false;
        }
        bool CollidesSquares(GameObject gameObject, GameObject gameObject2)
        {
            Rectangle rec1 = new Rectangle((int)(gameObject.Position.X - gameObject.Size.X / 2), (int)(gameObject.Position.Y - gameObject.Size.Y / 2), (int)gameObject.Size.X, (int)gameObject.Size.Y);
            Rectangle rec2 = new Rectangle((int)(gameObject2.Position.X - gameObject2.Size.X / 2), (int)(gameObject2.Position.Y - gameObject2.Size.Y/2), (int)gameObject2.Size.X, (int)gameObject2.Size.Y);
            return rec1.Intersects(rec2); 
        }
        bool CollidesSquares(Vector2 newPosition, GameObject gameObject, GameObject gameObject2)
        {
            Rectangle rec1 = new Rectangle((int)(newPosition.X - gameObject.Size.X / 2), (int)(newPosition.Y - gameObject.Size.Y / 2), (int)gameObject.Size.X, (int)gameObject.Size.Y);
            Rectangle rec2 = new Rectangle((int)(gameObject2.Position.X - gameObject2.Size.X / 2), (int)(gameObject2.Position.Y - gameObject2.Size.Y / 2), (int)gameObject2.Size.X, (int)gameObject2.Size.Y);
            return rec1.Intersects(rec2);
        }
        bool Collides(GameObject gameObject, GameObject gameObject2)
        {
            float radiusSqr =  (gameObject.Radius + gameObject2.Radius) * (gameObject.Radius + gameObject2.Radius);
            float distancedSqr = (gameObject.Position - gameObject2.Position).LengthSquared();
            if (gameObject != gameObject2 && radiusSqr > distancedSqr)
                return true;
            return false;
        }
        void UpdateMovement(double time)
        {
            GameObject gameObject;
            Vector2 newPosition;

            for (int i = 0; i < PhysicsGameObjects.Count; i++)
            {
                gameObject = PhysicsGameObjects[i];

                newPosition = gameObject.Position + gameObject.Velocity * (float)time;
                Vector2 Velocity2;
                CollidesWithAnObjectSquares(newPosition, gameObject, out Velocity2);
                Vector2 v = VelocityAfterHitsWall(newPosition, Velocity2, gameObject);
                gameObject.Position = gameObject.Position + v * (float)time;
                /*
                Vector2 v = VelocityAfterHitsWall(newPosition, Velocity2, gameObject);
                if (!HitsWalls(newPosition, gameObject))
                    gameObject.Position = gameObject.Position + Velocity2 * (float)time;
                   // gameObject.Position = newPosition;
                 * */
            }
        }
        bool CollidesWithAnObjectSquares(Vector2 newPosition, GameObject gameObject, out Vector2 Velocity2)
        {
            Rectangle rec1 = new Rectangle((int)(newPosition.X - gameObject.Radius), (int)(newPosition.Y - gameObject.Radius), (int)gameObject.Radius * 2, (int)gameObject.Radius * 2);
            Rectangle rec2; 
            GameObject gameObject2;
            //float radiusSqr;
            float distancedSqr;
            Velocity2 = gameObject.Velocity;
            bool flag = false;
            for (int j = 0; j < PhysicsGameObjects.Count; j++)
            {
                gameObject2 = PhysicsGameObjects[j];
                rec2 = new Rectangle((int)(gameObject2.Position.X - gameObject2.Radius), (int)(gameObject2.Position.Y - gameObject2.Radius), (int)gameObject2.Radius * 2, (int)gameObject2.Radius * 2);

                if (rec1.Intersects(rec2) && gameObject != gameObject2)
                {
                    Vector2 testPos = gameObject.Position;
                    testPos.X = newPosition.X;
                    if (CollidesSquares(testPos, gameObject, gameObject2))
                        Velocity2.X = 0;
                    testPos.Y = newPosition.Y;
                    if (CollidesSquares(testPos, gameObject, gameObject2))
                        Velocity2.Y = 0;
                    gameObject.OnCollision(gameObject2);
                    flag = true;
                }
            }
            return flag;

            //return (flag || HitsWalls(newPosition, gameObject));
        }
        bool HitsWalls(Vector2 newPosition, GameObject gameObject)
        {
            int radius = (int)gameObject.Radius;
            if (GameMechanics.LeftWallBound > newPosition.X && newPosition.X < gameObject.Position.X) return true;
            if (GameMechanics.RightWallBound < newPosition.X && newPosition.X > gameObject.Position.X) return true;
            if (GameMechanics.TopWallBound > newPosition.Y && newPosition.Y < gameObject.Position.Y) return true;
            if (GameMechanics.BottomWallBound < newPosition.Y && newPosition.Y > gameObject.Position.Y) return true;
            return false;
        }
        public Vector2 VelocityAfterHitsWall(Vector2 newPosition, Vector2 Velocity, GameObject gameObject)
        {
            Vector2 newVelocity = Velocity;
            if (GameMechanics.LeftWallBound > newPosition.X && newPosition.X < gameObject.Position.X) newVelocity.X = 0 ;
            if (GameMechanics.RightWallBound < newPosition.X && newPosition.X > gameObject.Position.X) newVelocity.X = 0;
            if (GameMechanics.TopWallBound > newPosition.Y && newPosition.Y < gameObject.Position.Y) newVelocity.Y = 0;
            if (GameMechanics.BottomWallBound < newPosition.Y && newPosition.Y > gameObject.Position.Y) newVelocity.Y = 0;
            return newVelocity;
        }
        bool CollidesWithAnObject(Vector2 newPosition, GameObject gameObject)
        {
            GameObject gameObject2;
            float radiusSqr;
            float distancedSqr;
            bool flag = false;
            for (int j = 0; j < PhysicsGameObjects.Count; j++)
            {
                gameObject2 = PhysicsGameObjects[j];
                radiusSqr =  (gameObject.Radius + gameObject2.Radius) * (gameObject.Radius + gameObject2.Radius);
                distancedSqr = (newPosition - gameObject2.Position).LengthSquared();
                if (gameObject != gameObject2 && radiusSqr > distancedSqr)
                {
                    gameObject.OnCollision(gameObject2);
                    return true;
                }
            }
            return flag;
        }
        public void Add(GameObject gameObject)
        {
            PhysicsGameObjects.Add(gameObject);
        }
        /// <summary>
        /// Currently Only tests for TiggerVSRegular game object collisions
        /// Subject to change
        /// </summary>
        public void AddTrigger(GameObject gameObject)
        {
            PhysicsGameObjectTriggers.Add(gameObject);
        }
        public void Remove(GameObject gameObject)
        {
            PhysicsGameObjects.Remove(gameObject);
            PhysicsGameObjectTriggers.Remove(gameObject);
        }

    }
}
