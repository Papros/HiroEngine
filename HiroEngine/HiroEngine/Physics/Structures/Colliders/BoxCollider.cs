using HiroEngine.HiroEngine.Graphics.Elements;
using HiroEngine.HiroEngine.Graphics.Shaders;
using HiroEngine.HiroEngine.Physics.Basic;
using HiroEngine.HiroEngine.Physics.Interfaces;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace HiroEngine.HiroEngine.Physics.Structures.Colliders
{
    public class BoxCollider : ICollider
    {
        Vector3 boundsMin = new Vector3(0,0,0);
        Vector3 boundsMax = new Vector3(1,1,1);

        public float[] _vertices =
        {
            // position color
            0.0f,0.0f,0.0f, 1.0f, 0.0f, 0.0f,//0              
            0.0f,1.0f,0.0f, 1.0f, 0.0f, 0.0f,//1                4 ----5
            1.0f,1.0f,0.0f, 1.0f, 0.0f, 0.0f,//2                | \    |\
            1.0f,0.0f,0.0f, 1.0f, 0.0f, 0.0f,//3                |  7----6max
            0.0f,0.0f,1.0f, 1.0f, 0.0f, 0.0f,//4             min0 -|--1 |
            0.0f,1.0f,1.0f, 1.0f, 0.0f, 0.0f,//5                  \|   \|
            1.0f,1.0f,1.0f, 1.0f, 0.0f, 0.0f,//6                   3----2
            1.0f,0.0f,1.0f, 1.0f, 0.0f, 0.0f,//7
        };

        public int[] _indices =
        {
            0,1,2,//down
            2,3,0,
            0,5,4,//back
            0,1,5,
            1,6,5,//right
            1,2,6,
            2,3,7,//front
            2,7,6,
            3,0,4,//left
            3,5,7,
            4,6,5,//up
            4,7,6
        };
        private int VAO, VBO, EBO;

        private void SetupMesh()
        {
            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();
            EBO = GL.GenBuffer();

            GL.BindVertexArray(VAO);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(ShaderProgram.Uniforms.CORE.VERTEX_COORDS);
            GL.VertexAttribPointer(ShaderProgram.Uniforms.CORE.VERTEX_COORDS, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0 * sizeof(float));

            GL.EnableVertexAttribArray(ShaderProgram.Uniforms.CORE.VERTEX_COLOR);
            GL.VertexAttribPointer(ShaderProgram.Uniforms.CORE.VERTEX_COLOR, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));

            GL.BindVertexArray(0);
        }

        public void Draw(ShaderProgram shaderProgram)
        {
            if (VAO == 0) SetupMesh();
            shaderProgram.Use();
            GL.BindVertexArray(VAO);
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
        }

        public bool IsHit(ICollider collider, out float outDistance, out Vector3 intersectPoint)
        {
            outDistance = 0;
            intersectPoint = this.boundsMax;

            if(collider.GetType() == typeof(BoxCollider))
            {
                var coll = (BoxCollider)collider;
                bool alignX = (coll.boundsMax.X < this.boundsMax.X && coll.boundsMax.X > this.boundsMin.X) ||
                              (coll.boundsMin.X < this.boundsMax.X && coll.boundsMin.X > this.boundsMin.X) || 
                              (coll.boundsMax.X > this.boundsMax.X && coll.boundsMin.X < this.boundsMin.X) ||
                              (coll.boundsMax.X < this.boundsMax.X && coll.boundsMin.X < this.boundsMin.X);

                if (!alignX) return false;

                bool alignY = (coll.boundsMax.Y < this.boundsMax.Y && coll.boundsMax.Y > this.boundsMin.Y) ||
                              (coll.boundsMin.Y < this.boundsMax.Y && coll.boundsMin.Y > this.boundsMin.Y) ||
                              (coll.boundsMax.Y > this.boundsMax.Y && coll.boundsMin.Y < this.boundsMin.Y) ||
                              (coll.boundsMax.Y < this.boundsMax.Y && coll.boundsMin.Y < this.boundsMin.Y);

                if (!alignY) return false;

                bool alignZ = (coll.boundsMax.Z < this.boundsMax.Z && coll.boundsMax.Z > this.boundsMin.Z) ||
                              (coll.boundsMin.Z < this.boundsMax.Z && coll.boundsMin.Z > this.boundsMin.Z) ||
                              (coll.boundsMax.Z > this.boundsMax.Z && coll.boundsMin.Z < this.boundsMin.Z) ||
                              (coll.boundsMax.Z < this.boundsMax.Z && coll.boundsMin.Z < this.boundsMin.Z);

                return alignZ;
            }
            else
            {
                return false;
            }
        }

        public bool IsHit(Ray ray, out float outDistance, out Vector3 intersectPoint)
        {
            //throw new NotImplementedException();
            outDistance = 0;
            intersectPoint = ray.Position;
            return false;
        }

        public ICollider Setup(Model model)
        {
            //Console.WriteLine("Starting setting up:..");
            boundsMax = new Vector3(model.Components[0].axisMax);
            boundsMin = new Vector3(model.Components[0].axisMin);

            foreach(Mesh mesh in model.Components)
            {
                if (mesh.axisMax.X > boundsMax.X) boundsMax.X = mesh.axisMax.X;
                if (mesh.axisMax.Y > boundsMax.Y) boundsMax.Y = mesh.axisMax.Y;
                if (mesh.axisMax.Z > boundsMax.Z) boundsMax.Z = mesh.axisMax.Z;

                if (mesh.axisMin.X < boundsMin.X) boundsMin.X = mesh.axisMin.X;
                if (mesh.axisMin.Y < boundsMin.Y) boundsMin.Y = mesh.axisMin.Y;
                if (mesh.axisMin.Z < boundsMin.Z) boundsMin.Z = mesh.axisMin.Z;

            }

            _vertices = new float[]
            {
                // position color
                this.boundsMin.X, this.boundsMin.Y, this.boundsMin.Z, 1.0f, 0.0f, 0.0f,//0              
                this.boundsMin.X, this.boundsMax.Y, this.boundsMin.Z, 1.0f, 0.0f, 0.0f,//1                4 ----5
                this.boundsMax.X, this.boundsMax.Y, this.boundsMin.Z, 1.0f, 0.0f, 0.0f,//2                | \    |\
                this.boundsMax.X, this.boundsMin.Y, this.boundsMin.Z, 1.0f, 0.0f, 0.0f,//3                |  7----6max
                this.boundsMin.X, this.boundsMin.Y, this.boundsMax.Z, 1.0f, 0.0f, 0.0f,//4             min0 -|--1 |
                this.boundsMin.X, this.boundsMax.Y, this.boundsMax.Z, 1.0f, 0.0f, 0.0f,//5                  \|   \|
                this.boundsMax.X, this.boundsMax.Y, this.boundsMax.Z, 1.0f, 0.0f, 0.0f,//6                   3----2
                this.boundsMax.X, this.boundsMin.Y, this.boundsMax.Z, 1.0f, 0.0f, 0.0f,//7
            };

            return this;
        }

        public ICollider SetupAABB(Model model)
        {
            return this;
        }
    }
}
