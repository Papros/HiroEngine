using Assimp;
using HiroEngine.HiroEngine.Graphics.Elements;
using System;
using System.Collections.Generic;
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
            Vertex[] vertex = new Vertex[mesh.Vertices.Count];

            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                var pos = mesh.Vertices[i];
                var normal = mesh.Normals[i];
                var textcoord = mesh.TextureCoordinateChannels[0][i];
                vertex[i] = new Vertex(new float[]{ pos.X, pos.Y, pos.Z, normal.X, normal.Y, normal.Z, textcoord.X, textcoord.Y });
            }

            Texture[] textures = new Texture[] { new Texture(scene.Materials[mesh.MaterialIndex].TextureDiffuse.FilePath) };

            return new HiroEngine.Graphics.Elements.Mesh(vertex, indicies, textures);
        }
    }
}
