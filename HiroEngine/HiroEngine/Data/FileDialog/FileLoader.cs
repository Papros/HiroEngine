using Assimp;
using HiroEngine.HiroEngine.Graphics.Elements;
using System;
using System.Collections.Generic;
using HiroEngine.HiroEngine.Data.Logger;
using System.Linq;
using System.Text;

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

            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                var pos = mesh.Vertices[i];
                var normal = mesh.Normals[i];
                var textcoord = mesh.TextureCoordinateChannels[0][i];
                //vertex[i] = new Vertex(new float[]{ pos.X, pos.Y, pos.Z, normal.X, normal.Y, normal.Z, textcoord.X, textcoord.Y });
                vertex[i * 8 + 0] = pos.X;
                vertex[i * 8 + 1] = pos.Y;
                vertex[i * 8 + 2] = pos.Z;

                vertex[i * 8 + 3] = normal.X;
                vertex[i * 8 + 4] = normal.Y;
                vertex[i * 8 + 5] = normal.Z;

                vertex[i * 8 + 6] = textcoord.X;
                vertex[i * 8 + 7] = textcoord.Y;

            }
            Console.WriteLine($"Textures: {scene.Materials[mesh.MaterialIndex].TextureDiffuse.FilePath};");
            Texture[] textures = new Texture[] { new Texture(scene.Materials[mesh.MaterialIndex].TextureDiffuse.FilePath) };

            return new HiroEngine.Graphics.Elements.Mesh(vertex, indicies, textures);
        }
    }
}
