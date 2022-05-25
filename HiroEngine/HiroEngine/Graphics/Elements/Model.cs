using HiroEngine.HiroEngine.Data.FileDialog;
using HiroEngine.HiroEngine.Graphics.Shaders;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HiroEngine.HiroEngine.Graphics.Elements
{
    public class Model
    {
        public List<Mesh> Components { get; private set; }
        public Vector3 position = new Vector3(0,0,0);
        public Matrix4 modelMatrix = Matrix4.Identity;
        public Matrix4 rotationMatrix = Matrix4.Identity;
        public Matrix4 transformMatrix = Matrix4.Identity;

        public Model(string path)
        {
            if(path != "")
            {
                string combinedPath = Path.Combine(Environment.CurrentDirectory, @"Sample/assets", path);
                Components = FileLoader.LoadFile(combinedPath);
            } else
            {
                Components = new List<Mesh>();
            }
        }

        public Model(Mesh mesh)
        {
            Components = new List<Mesh>();
            Components.Add(mesh);
        }

        public void AddModel(Mesh model)
        {
            Components.Add(model);
        }

        public void LoadModel(string path)
        {
            string combinedPath = Path.Combine(Environment.CurrentDirectory, @"Sample/assets", path);
            Components = FileLoader.LoadFile(combinedPath);
        }

        public void AddPosition(Vector3 position)
        {
            modelMatrix = Matrix4.CreateTranslation(position);
        }

        public void MoveBy(Vector3 move)
        {
            modelMatrix *= Matrix4.CreateTranslation(move);
        }

        public void AddTransform(float scale)
        {
            modelMatrix = Matrix4.CreateScale(scale);
        }

        public void AddRotation(float x, float y, float z)
        {
            rotationMatrix = Matrix4.CreateRotationX(x) * Matrix4.CreateRotationY(y) * Matrix4.CreateRotationZ(z);
        }

        public void AddRotationDgr(float x, float y, float z)
        {
            const float RAD = 0.0174533f;
            rotationMatrix = Matrix4.CreateRotationX(x*RAD) * Matrix4.CreateRotationY(y*RAD) * Matrix4.CreateRotationZ(z*RAD);
        }

        public void Draw(ShaderProgram shaderProgram)
        {
            shaderProgram.SetMatrix4(ShaderProgram.Uniforms.MATRIX.MODEL, modelMatrix * rotationMatrix);

            foreach(Mesh mesh in Components)
            {
                mesh.Draw(shaderProgram);
            }
        }
    }
}
