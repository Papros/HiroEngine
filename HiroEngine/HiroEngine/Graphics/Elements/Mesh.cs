using System;
using System.Collections.Generic;
using System.Text;
using HiroEngine.HiroEngine.Graphics.Shaders;
using OpenTK.Graphics.OpenGL4;

namespace HiroEngine.HiroEngine.Graphics.Elements
{
    public class Mesh
    {
        private Vertex[] _vertices;
        private int[] _indices;
        private Texture[] _textures;

        private int VAO, VBO, EBO;

        public int Size {  get { return _indices.Length;  } }

        public Mesh(Vertex[] vertices, int[] indices, Texture[] textures)
        {
            _vertices = vertices;
            _indices = indices;
            _textures = textures;

            Setup();
        }

        public void Draw(int ID)
        {
            for(int i = 0; i < _textures.Length; i++)
            {
                GL.BindTextureUnit(i, _textures[i].Handle);
            }

            GL.BindVertexArray(VAO);
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);

            GL.ActiveTexture(TextureUnit.Texture0);
        }

        private void Setup()
        {
            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();
            EBO = GL.GenBuffer();

            GL.BindVertexArray(VAO);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float) * 8, _vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(ShaderProgram.Uniforms.CORE.VERTEX_COORDS);
            GL.VertexAttribPointer(ShaderProgram.Uniforms.CORE.VERTEX_COORDS, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0 * sizeof(float));

            GL.EnableVertexAttribArray(ShaderProgram.Uniforms.CORE.VERTEX_COLOR);
            GL.VertexAttribPointer(ShaderProgram.Uniforms.CORE.VERTEX_COLOR, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

            GL.EnableVertexAttribArray(ShaderProgram.Uniforms.CORE.TEXTURE_COORDS);
            GL.VertexAttribPointer(ShaderProgram.Uniforms.CORE.TEXTURE_COORDS, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));

            GL.BindVertexArray(0);
        }

        public static Mesh TestData()
        {
            Vertex[] vertices =
            {
                // Position         color             texture coordinates
                new Vertex( new float[]{0.5f,  0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f }), // top right
                new Vertex( new float[]{0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 0.0f }), // bottom right
                new Vertex( new float[]{-0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f }), // bottom left
                new Vertex( new float[]{-0.5f,  0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f })  // top left
            };

            int[] indices = {  // note that we start from 0!
                    0, 1, 3,   // first triangle
                    1, 2, 3    // second triangle
                };

            return new Mesh(vertices, indices, new Texture[]{ new Texture("container.png") });
        }
    }
}
