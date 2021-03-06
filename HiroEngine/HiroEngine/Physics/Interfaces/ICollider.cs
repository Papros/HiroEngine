using HiroEngine.HiroEngine.Graphics.Elements;
using HiroEngine.HiroEngine.Physics.Basic;
using OpenTK.Mathematics;

namespace HiroEngine.HiroEngine.Physics.Interfaces
{
    public interface ICollider : IDrawable
    {
        public ICollider Setup(Model model);
        public bool IsHit(ICollider collider, out float outDistance, out Vector3 intersectPoint);
        public bool IsHit(Ray ray, out float outDistance, out Vector3 intersectPoint);

    }
}
