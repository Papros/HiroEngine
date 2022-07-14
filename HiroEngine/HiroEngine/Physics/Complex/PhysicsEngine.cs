using HiroEngine.HiroEngine.Engine.Elements;
using HiroEngine.HiroEngine.Graphics.Elements;
using HiroEngine.HiroEngine.Physics.Basic;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace HiroEngine.HiroEngine.Physics.Complex
{
    public class PhysicsEngine
    {
        private GameEngine engine;
        public PhysicsEngine(GameEngine engine)
        {
            this.engine = engine;
        }

        public RayIntersection Raycast(Ray ray)
        {
           foreach(WorldObject worldObject in new List<WorldObject>(engine?.Scene?.worldObjects))
           {
               
               Vector3 intersetion;
               float distance;
               bool hit = worldObject.PhysicalBody.IsHit(ray, out distance, out intersetion);
               if(hit)
               {
                   return new RayIntersection(intersetion, worldObject, distance);
               }
           };

           return null;
        }
    }
}
