using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace HiroEngine.HiroEngine.Graphics.Shaders
{
    public struct ShaderProgram
    {
        public int ID;
        public Shader VertexShader { get; private set; }
        public Shader FragmentShader { get; private set; }

        private Dictionary<string, int> _uniformLocations;

        public struct FragmentShadersType
        {
            //public static string BASIC = @"./HiroEngine/HiroEngine/Graphics/Shaders/Fragments/basic.frag.glsl";
            public static string BASIC = "C:/Users/piotr/Desktop/C#/HiroEngine/HiroEngine/HiroEngine/Graphics/Shaders/Fragments/basic.frag.glsl";
        }

        public struct VertexShaderType
        {
            //public static string BASIC = @"./HiroEngine/HiroEngine/Graphics/Shaders/Vertex/basic.vert.glsl";
            public static string BASIC = "C:/Users/piotr/Desktop/C#/HiroEngine/HiroEngine/HiroEngine/Graphics/Shaders/Vertex/basic.vert.glsl";
        }

        public ShaderProgram LoadShaderProgram( string vertexShaderPath, string fragmentShaderPath )
        {
            ID = GL.CreateProgram();

            VertexShader = Shader.LoadShader(vertexShaderPath, ShaderType.VertexShader);
            FragmentShader = Shader.LoadShader(fragmentShaderPath, ShaderType.FragmentShader);

            GL.AttachShader(ID, VertexShader.ID);
            GL.AttachShader(ID, FragmentShader.ID);

            GL.LinkProgram(ID);

            GL.DetachShader(ID, VertexShader.ID);
            GL.DetachShader(ID, FragmentShader.ID);

            GL.DeleteShader(VertexShader.ID);
            GL.DeleteShader(FragmentShader.ID);

            string infoLog = GL.GetProgramInfoLog(ID);

            GL.GetProgram(ID, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

            _uniformLocations = new Dictionary<string, int>();
            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform(ID, i, out _, out _);
                var location = GL.GetUniformLocation(ID, key);
                _uniformLocations.Add(key, location);
            }

            if (!string.IsNullOrEmpty(infoLog))
            {
                throw new Exception(infoLog);
            }

            return this;
        }

        public void Use()
        {
            GL.UseProgram(ID);
        }

        public void SetInt(string name, int value)
        {
            GL.UseProgram(ID);
            GL.Uniform1(_uniformLocations[name], value);
        }

        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(ID);
            GL.UniformMatrix4(_uniformLocations[name], true, ref data);
        }

        public void SetVector2(string name, Vector2 data)
        {
            GL.UseProgram(ID);
            GL.Uniform2(_uniformLocations[name], ref data);
        }

        public void SetVector3(string name, Vector3 data)
        {
            GL.UseProgram(ID);
            GL.Uniform3(_uniformLocations[name], ref data);
        }
    }
}
