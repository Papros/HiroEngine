using HiroEngine.HiroEngine.Graphics.Elements;
using HiroEngine.HiroEngine.Graphics.Shaders;
using HiroEngine.HiroEngine.Physics.Basic;
using HiroEngine.HiroEngine.Physics.Interfaces;
using OpenTK.Mathematics;
using System;

namespace HiroEngine.HiroEngine.Physics.Structures.Colliders
{
    public class CylinderCollider : ICollider
    {
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
            throw new NotImplementedException();
        }

        public ICollider Setup(Model model)
        {
            throw new NotImplementedException();
        }
    }
}
