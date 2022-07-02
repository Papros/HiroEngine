using HiroEngine.HiroEngine.Graphics.Elements;
using HiroEngine.HiroEngine.Graphics.Shaders;
using HiroEngine.HiroEngine.Physics.Basic;
using HiroEngine.HiroEngine.Physics.Interfaces;
using OpenTK.Mathematics;

namespace HiroEngine.HiroEngine.Physics.Complex
{
    public class RigidBody : IDrawable
    {
        public float Mass { get; set; }
        private ICollider Collider;
        private ICollider AABB;

        public RigidBody(float mass, ICollider collider, ICollider aabb)
        {
            Mass = mass;
            Collider = collider;
            AABB = aabb;
        }

        public void Draw(ShaderProgram shader)
        {
            Collider?.Draw(shader);
            AABB?.Draw(shader);
        }

        public bool IsHit(Ray ray, out float outDistance, out Vector3 intersectPoint)
        {
            if (AABB == null)
            {
                outDistance = 0;
                intersectPoint = ray.Position;
                if(Collider == null)
                {
                    return false;
                }
                return Collider.IsHit(ray, out outDistance, out intersectPoint);
            }
            return AABB.IsHit(ray, out outDistance, out intersectPoint);
        }
    }
}
