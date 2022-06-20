using HiroEngine.HiroEngine.Data.Logger;
using HiroEngine.HiroEngine.Engine.Elements;
using HiroEngine.HiroEngine.Graphics.Elements;
using HiroEngine.HiroEngine.Physics.Basic;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiroEngine.HiroEngine.Physics.Complex
{
    public class PhysicsEngine
    {
        private GameEngine engine;
        public PhysicsEngine(GameEngine engine)
        {
            this.engine = engine;
        }

        public void Raycast(Ray ray)
        {
           foreach(WorldObject worldObject in new List<WorldObject>(engine?.Scene?.worldObjects))
           {
               Vector3 intersetion;
               bool hit = worldObject.physicsBody.IsHit(ray, out _, out intersetion);
               if(hit)
               {
                   Shape floor = Shape.Cube(intersetion, new Vector3(3,3,3));
                   engine.Scene.QueueToAdd.Add(
                    new WorldObject(new Model(floor),
                    false,
                    false
                    ));
               }
               Logger.Quick($"Object is hit? ({hit})");
           };
           engine.Scene.Update();
        }
    }
}
