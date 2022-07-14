using HiroEngine.HiroEngine.Graphics.Elements;
using HiroEngine.HiroEngine.Graphics.Shaders;
using HiroEngine.HiroEngine.Physics.Basic;
using HiroEngine.HiroEngine.Physics.Interfaces;
using OpenTK.Mathematics;
using System;

namespace HiroEngine.HiroEngine.Physics.Structures.Colliders
{
    public class PlaneCollider : ICollider
    {
        private Vector3 boundA, boundB, boundC, boundD;
        private float distanceFromStart;
        private Vector3 planeNormal;
        private readonly string LoggerID = "PlaneCollider";

        public PlaneCollider(Vector3 bA, Vector3 bB, Vector3 bC, Vector3 bD)
        {
            boundA = bA;
            boundB = bB;
            boundC = bC;
            boundD = bD;
            planeNormal = Vector3.Normalize(Vector3.Cross(bB - bA, bC - bA));
            distanceFromStart = Distance(0, 0, 0);
        }

        public bool CheckCoplanar()
        {
            return CheckCoplanar(boundA, boundB, boundC, boundD);
        }

        private bool CheckCoplanar(Vector3 bA, Vector3 bB, Vector3 bC, Vector3 bD)
        {
            float a1 = bB.X - bA.X;
            float b1 = bB.Y - bA.Y;
            float c1 = bB.Z - bA.Z;

            float a2 = bC.X - bA.X;
            float b2 = bC.Y - bA.Y;
            float c2 = bC.Z - bA.Z;

            float a = b1 * c2 - b2 * c1;
            float b = a2 * c1 - a1 * c2;
            float c = a1 * b2 - b1 * a2;
            float d = (-a * bA.X - b * bA.Y - c * bA.Z);

            return a * bD.X + b * bD.Y + c * bD.Z + d == 0;
        }

        public float Distance(float x, float y, float z)
        {
            float a1 = boundB.X - boundA.X;
            float b1 = boundB.Y - boundA.Y;
            float c1 = boundB.Z - boundA.Z;

            float a2 = boundC.X - boundA.X;
            float b2 = boundC.Y - boundA.Y;
            float c2 = boundC.Z - boundA.Z;

            float a = b1 * c2 - b2 * c1;
            float b = a2 * c1 - a1 * c2;
            float c = a1 * b2 - b1 * a2;
            float d = (-a * boundA.X - b * boundA.Y - c * boundA.Z);

            return a * x + b * y + c * z + d;
        }

        private float Distance(Vector3 point)
        {
            return Distance(point.X, point.Y, point.Z);
        }

        public void Draw(ShaderProgram shader)
        {
            throw new NotImplementedException();
        }

        public bool IsHit(ICollider collider, out float outDistance, out Vector3 intersectPoint)
        {
            throw new NotImplementedException();
        }

        public bool IsHit(Ray ray, out float outDistance, out Vector3 intersectPoint)
        {
            float denom = Vector3.Dot(planeNormal, ray.Vector);
            if(Math.Abs(denom) > 1e-4f)
            {
                var helper = planeNormal*distanceFromStart - ray.Position;
                outDistance = -(Vector3.Dot(planeNormal, ray.Position) + distanceFromStart) / denom;
                intersectPoint = ray.Position + ray.Vector * outDistance;
                return Vector3.Dot(helper, planeNormal) / denom >= 0;
            } else
            {
                intersectPoint = ray.Position;
                outDistance = 0;
            }
           
            return false;
        }

        public ICollider Setup(Model model)
        {
            return this;
        }
    }
}
