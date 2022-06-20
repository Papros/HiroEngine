using HiroEngine.HiroEngine.Data.FileDialog;
using HiroEngine.HiroEngine.Graphics.Shaders;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;

namespace HiroEngine.HiroEngine.Graphics.Elements
{
    public class Model : IDrawable
    {
        public List<Mesh> Components { get; private set; }
        public Vector3 position = new Vector3(0,0,0);
        public Vector3 axisMin { get; private set; }
        public Vector3 axisMax { get; private set; }

        public Model(Model model)
        {
            this.Components = model.Components;
            this.position = model.position;
            this.axisMin = model.axisMin;
            this.axisMax = model.axisMax;
        }

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

        public void Draw(ShaderProgram shaderProgram)
        {
            foreach(Mesh mesh in Components)
            {
                mesh.Draw(shaderProgram);
            }
        }
    }
}
