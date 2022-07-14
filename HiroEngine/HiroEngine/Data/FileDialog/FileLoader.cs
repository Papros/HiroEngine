using Assimp;
using HiroEngine.HiroEngine.Graphics.Elements;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace HiroEngine.HiroEngine.Data.FileDialog
{
    public class FileLoader
    {

        public static List<Graphics.Elements.Mesh> LoadFile(string path)
        {
            
            List<Graphics.Elements.Mesh> components = new List<Graphics.Elements.Mesh>();
            AssimpContext importer = new AssimpContext();
            Scene scene = importer.ImportFile(path, PostProcessSteps.OptimizeMeshes);
            
            foreach(Assimp.Mesh mesh in scene.Meshes)
            {
                components.Add(BuildMesh(mesh, scene));
            }

            return components;
        }

        private static HiroEngine.Graphics.Elements.Mesh BuildMesh(Assimp.Mesh mesh, Scene scene)
        {
            int[] indicies = mesh.GetIndices();
            float[] vertex = new float[mesh.Vertices.Count * 8];

            Vector3 axisMin = new Vector3(mesh.Vertices[0].X, mesh.Vertices[0].Y, mesh.Vertices[0].Z);
            Vector3 axisMax = new Vector3(axisMin);

            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                var pos = mesh.Vertices[i];
                var normal = mesh.Normals[i];
                var textcoord = mesh.TextureCoordinateChannels[0][i];
                //vertex[i] = new Vertex(new float[]{ pos.X, pos.Y, pos.Z, normal.X, normal.Y, normal.Z, textcoord.X, textcoord.Y });
                vertex[i * 8 + 0] = pos.X;
                vertex[i * 8 + 1] = pos.Y;
                vertex[i * 8 + 2] = pos.Z;

                if (pos.X > axisMax.X) axisMax.X = pos.X; 
                else if (pos.X < axisMin.X) axisMin.X = pos.X;

                if (pos.Y > axisMax.Y) axisMax.Y = pos.Y;
                else if (pos.Y < axisMin.Y) axisMin.Y = pos.Y;

                if (pos.Z > axisMax.Z) axisMax.Z = pos.Z;
                else if (pos.Z < axisMin.Z) axisMin.Z = pos.Z;                
                
                vertex[i * 8 + 3] = normal.X;
                vertex[i * 8 + 4] = normal.Y;
                vertex[i * 8 + 5] = normal.Z;

                vertex[i * 8 + 6] = textcoord.X;
                vertex[i * 8 + 7] = textcoord.Y;
            }

            Texture[] textures = new Texture[] { new Texture(scene.Materials[mesh.MaterialIndex].TextureDiffuse.FilePath) };

            return new HiroEngine.Graphics.Elements.Mesh(vertex, indicies, textures, axisMin, axisMax);
        }
    }
}
