using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiroEngine.HiroEngine.Physics.Basic
{
    public class Ray
    {
        public Vector3 Vector { get; set; }
        public Vector3 Position { get; set; }

        public Ray(Vector3 vec, Vector3 pos)
        {
            Vector = vec;
            Position = pos;
        }
    }
}
