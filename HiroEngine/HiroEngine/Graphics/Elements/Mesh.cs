using System;
using System.Collections.Generic;
using System.Text;
using HiroEngine.HiroEngine.Graphics.Shaders;
using OpenTK.Graphics.OpenGL4;

namespace HiroEngine.HiroEngine.Graphics.Elements
{
    public class Mesh
    {
        private float[] _vertices;
        private int[] _indices;
        private Texture[] _textures;

        private int VAO, VBO, EBO;

        public int Size {  get { return _indices.Length;  } }

        public Mesh(float[] vertices, int[] indices, Texture[] textures)
        {
            _vertices = vertices;
            _indices = indices;
            _textures = textures != null ? textures : new Texture[] { };

            Setup();
        }

        public void Draw(ShaderProgram shader)
        {
            shader.Use();
            GL.BindVertexArray(VAO);

            for (int i = 0; i < _textures.Length; i++)
            {
                _textures[i].UseUnit(i);
            }

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
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(ShaderProgram.Uniforms.CORE.VERTEX_COORDS);
            GL.VertexAttribPointer(ShaderProgram.Uniforms.CORE.VERTEX_COORDS, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0 * sizeof(float));

            GL.EnableVertexAttribArray(ShaderProgram.Uniforms.CORE.VERTEX_COLOR);
            GL.VertexAttribPointer(ShaderProgram.Uniforms.CORE.VERTEX_COLOR, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

            GL.EnableVertexAttribArray(ShaderProgram.Uniforms.CORE.TEXTURE_COORDS);
            GL.VertexAttribPointer(ShaderProgram.Uniforms.CORE.TEXTURE_COORDS, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));

            GL.ActiveTexture(TextureUnit.Texture0);

            GL.BindVertexArray(0);
        }

        public void AddTexture(Texture[] textures)
        {
            _textures = textures;
        }

        public void ReplaceTextures(Texture[] textures)
        {
            _textures = textures;
        }

        public static Mesh TestData()
        {
            float[] vertices =
            {
                // Position         color             texture coordinates
                 0.5f,  0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, // top right
                 0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 0.0f, // bottom right
                -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, // bottom left
                -0.5f,  0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f  // top left
            };

            int[] indices = {  // note that we start from 0!
                    0, 1, 3,   // first triangle
                    1, 2, 3    // second triangle "textures/grass.jpg"
                };

            //return new Mesh(vertices, indices, new Texture[]{ new Texture("container.png") });
            return new Mesh(vertices, indices, new Texture[]{ new Texture("textures/grass.jpg") });
        }
    }
}
