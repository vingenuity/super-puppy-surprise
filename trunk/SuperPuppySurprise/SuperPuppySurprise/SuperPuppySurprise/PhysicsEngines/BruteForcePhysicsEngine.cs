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
            for(int i = 0; i< PhysicsGameObjects.Count; i++)
            {
                gameObject = PhysicsGameObjects[i];
                
                gameObject.Position += gameObject.Velocity * (float) time;
            }
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
