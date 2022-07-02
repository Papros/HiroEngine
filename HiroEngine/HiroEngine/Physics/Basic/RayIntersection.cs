using HiroEngine.HiroEngine.Engine.Elements;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiroEngine.HiroEngine.Physics.Basic
{
    public class RayIntersection
    {
        public Vector3 ShotPosition;
        public Vector3 HitPosition;
        public WorldObject Target;
        public float HitDistance;

        public RayIntersection(Vector3 point, WorldObject target, float distance)
        {
            HitPosition = point;   
            Target = target;
            HitDistance = distance; 
        }
    }
}
