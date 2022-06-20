using HiroEngine.HiroEngine.Graphics.Elements;
using HiroEngine.HiroEngine.Graphics.Shaders;
using HiroEngine.HiroEngine.Physics.Complex;
using HiroEngine.HiroEngine.Physics.Interfaces;
using HiroEngine.HiroEngine.Physics.Structures.Colliders;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiroEngine.HiroEngine.Engine.Elements
{
    public class WorldObject : IDrawable    
    {

        public bool Visible { get; set; }
        public Model ObjectModel { private set; get; }
        public RigidBody physicsBody { get; set; }

        private Matrix4 modelMatrix = Matrix4.Identity;
        private Matrix4 rotationMatrix = Matrix4.Identity;
        private Matrix4 transformMatrix = Matrix4.Identity;

        public WorldObject(Model model, bool createRigidMesh = false, bool createAABB = false, ICollider collider = null, ICollider aabb = null)
        {
            Visible = true;
            ObjectModel = model;
            physicsBody = new RigidBody(
                1.0f, 
                createRigidMesh ? new BoxCollider().Setup(model) : collider,
                createAABB ? new BoxCollider().SetupAABB(model) : aabb
                );
        }

        public WorldObject AddPosition(float x, float y, float z)
        {
            modelMatrix = Matrix4.CreateTranslation(x,y,z);
            return this;
        }

        public WorldObject MoveBy(Vector3 move)
        {
            modelMatrix *= Matrix4.CreateTranslation(move);
            return this;
        }

        public WorldObject AddTransform(float scale)
        {
            modelMatrix *= Matrix4.CreateScale(scale);
            return this;
        }

        public WorldObject AddRotation(float x, float y, float z)
        {
            rotationMatrix = Matrix4.CreateRotationX(x) * Matrix4.CreateRotationY(y) * Matrix4.CreateRotationZ(z);
            return this;
        }

        public WorldObject AddRotationDgr(float x, float y, float z)
        {
            const float RAD = 0.0174533f;
            rotationMatrix = Matrix4.CreateRotationX(x * RAD) * Matrix4.CreateRotationY(y * RAD) * Matrix4.CreateRotationZ(z * RAD);
            return this;
        }

        public void Draw(ShaderProgram shader)
        {
            if (ObjectModel != null)
            {
                shader.SetMatrix4(ShaderProgram.Uniforms.MATRIX.MODEL, rotationMatrix * modelMatrix);
                ObjectModel.Draw(shader);
            }
        }

        public void DrawCollider(ShaderProgram shader)
        {
            if (physicsBody != null)
            {
                shader.SetMatrix4(ShaderProgram.Uniforms.MATRIX.MODEL, rotationMatrix * modelMatrix);
                physicsBody.Draw(shader);
            }
        }
    }
}
