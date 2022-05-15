using HiroEngine.HiroEngine.Data.FileDialog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HiroEngine.HiroEngine.Graphics.Elements
{
    public class Model
    {
        public List<Mesh> Components { get; private set; }

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

        public void Draw(int ShaderID)
        {
            int i = 0;
            foreach(Mesh mesh in Components)
            {
                mesh.Draw(ShaderID);
                i += mesh.Size;
            }

            Console.WriteLine($"Drawed size: { i }");
        }
    }
}
