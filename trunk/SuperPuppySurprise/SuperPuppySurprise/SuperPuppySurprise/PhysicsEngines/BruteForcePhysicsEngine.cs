using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperPuppySurprise.GameObjects;
using Microsoft.Xna.Framework;

namespace SuperPuppySurprise.PhysicsEngines
{
    public class BruteForcePhysicsEngine
    {
        List<GameObject> PhysicsGameObjects;
        public BruteForcePhysicsEngine()
        {
            Reset();
        }
        public void Reset()
        {
            PhysicsGameObjects = new List<GameObject>();
        }
        public void Update(GameTime gameTime)
        {
            double time = gameTime.ElapsedGameTime.TotalSeconds;
            GameObject gameObject;
            Vector2 newPosition;
            
            for(int i = 0; i< PhysicsGameObjects.Count; i++)
            {
                gameObject = PhysicsGameObjects[i];

                newPosition = gameObject.Position + gameObject.Velocity * (float)time;
                if (!CollidesWithAnObject(newPosition, gameObject))
                    gameObject.Position = newPosition;
            }
        }
        bool CollidesWithAnObject(Vector2 newPosition, GameObject gameObject)
        {
            GameObject gameObject2;
            float radiusSqr;
            float distancedSqr;
            for (int j = 0; j < PhysicsGameObjects.Count; j++)
            {
                gameObject2 = PhysicsGameObjects[j];
                radiusSqr =  (gameObject.Radius + gameObject2.Radius) * (gameObject.Radius + gameObject2.Radius);
                distancedSqr = (newPosition - gameObject2.Position).LengthSquared();
                if (gameObject != gameObject2 && radiusSqr > distancedSqr)
                    return true;
            }
            return false;
        }
        public void Add(GameObject gameObject)
        {
            PhysicsGameObjects.Add(gameObject);
        }
        public void Remove(GameObject gameObject)
        {
            PhysicsGameObjects.Remove(gameObject);
        }
    }
}
