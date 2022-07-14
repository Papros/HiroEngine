using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiroEngine.HiroEngine.Physics.Basic
{
    public struct Movement
    {
        public bool IsActive;
        public bool HasFinished;
        public Vector3 Direction;
        public float Force;

        public Movement(Vector3 direction, float force, bool active = false, bool finished = false)
        {
            Direction = direction;  
            Force = force;
            IsActive = active;
            HasFinished = finished;
        }
    }
}
