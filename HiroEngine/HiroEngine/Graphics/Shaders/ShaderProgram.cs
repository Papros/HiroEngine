using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace HiroEngine.HiroEngine.Graphics.Shaders
{
    public struct ShaderProgram
    {
        public int ID;

        private Dictionary<string, int> _uniformLocations;

        public struct FragmentShadersType
        {
            public static string BASIC = @"HiroEngine\Graphics\Shaders\Fragments\basic.frag.glsl";
            public static string BASIC_UI = @"HiroEngine\Graphics\Shaders\Fragments\basic.frag.ui.glsl";
        }

        public struct VertexShaderType
        {
            public static string BASIC = @"HiroEngine\Graphics\Shaders\Vertex\basic.vert.glsl";
            public static string BASIC_UI = @"HiroEngine\Graphics\Shaders\Vertex\basic.vert.ui.glsl";
        }

        public struct Uniforms
        {
            public struct CORE
            {
                public static int VERTEX_COORDS = 0; //"vertex_coords";
                public static int TEXTURE_COORDS = 2;//"texture_coords";
                public static int VERTEX_COLOR = 1; //"vertex_color";
            }

            public struct TEXTURES
            {
                public static string TEXTURE_1 = "texture1";
                public static string TEXTURE_2 = "texture2";
            }

            public struct LIGHT {
                public static string POSITION = "light_position";
                public static string COLOR = "light_color";
                public static string SATURATION = "light_saturation";
                public static string LIMIT = "light_limit";
                public static string POWER = "light_power";
            }

            public struct MATRIX
            {
                public static string PROJECTION = "matrix_projection";
                public static string MODEL = "matrix_model";
                public static string UI_MODEL = "matrix_ui_model";
                public static string VIEW = "matrix_view";
            }
        }

        public ShaderProgram(string vertexShaderPath, string fragmentShaderPath)
        {
            ID = GL.CreateProgram();

            var _vertexShader = Shader.LoadShader(vertexShaderPath, ShaderType.VertexShader);
            var _fragmentShader = Shader.LoadShader(fragmentShaderPath, ShaderType.FragmentShader);

            GL.AttachShader(ID, _vertexShader.ID);
            GL.AttachShader(ID, _fragmentShader.ID);

            GL.LinkProgram(ID);

            GL.DetachShader(ID, _vertexShader.ID);
            GL.DetachShader(ID, _fragmentShader.ID);

            GL.DeleteShader(_vertexShader.ID);
            GL.DeleteShader(_fragmentShader.ID);

            string infoLog = GL.GetProgramInfoLog(ID);

            _uniformLocations = new Dictionary<string, int>();
            loadUniformsNames();

            if (!string.IsNullOrEmpty(infoLog))
            {
                throw new Exception(infoLog);
            }
        }

        public ShaderProgram LoadShaderProgram( string vertexShaderPath, string fragmentShaderPath )
        {
            ID = GL.CreateProgram();

            var _vertexShader = Shader.LoadShader(vertexShaderPath, ShaderType.VertexShader);
            var _fragmentShader = Shader.LoadShader(fragmentShaderPath, ShaderType.FragmentShader);

            GL.AttachShader(ID, _vertexShader.ID);
            GL.AttachShader(ID, _fragmentShader.ID);

            GL.LinkProgram(ID);

            GL.DetachShader(ID, _vertexShader.ID);
            GL.DetachShader(ID, _fragmentShader.ID);

            GL.DeleteShader(_vertexShader.ID);
            GL.DeleteShader(_fragmentShader.ID);

            string infoLog = GL.GetProgramInfoLog(ID);

           

            _uniformLocations = new Dictionary<string, int>();
            loadUniformsNames();

            if (!string.IsNullOrEmpty(infoLog))
            {
                throw new Exception(infoLog);
            }

            return this;
        }

        private void loadUniformsNames()
        {
            GL.GetProgram(ID, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);
            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform(ID, i, out _, out _);
                var location = GL.GetUniformLocation(ID, key);
                _uniformLocations.Add(key, location);
            }
        }

        public void Use()
        {
            GL.UseProgram(ID);
        }

        public void SetInt(string name, int value)
        {
            GL.UseProgram(ID);
            GL.Uniform1(GetUniformLocation(name), value);
        }

        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(ID);
            GL.UniformMatrix4(_uniformLocations[name], true, ref data);
        }

        public void SetVector2(string name, Vector2 data)
        {
            GL.UseProgram(ID);
            GL.Uniform2(GetUniformLocation(name), ref data);
        }

        public void SetVector3(string name, Vector3 data)
        {
            GL.UseProgram(ID);
            GL.Uniform3(GetUniformLocation(name), ref data);
        }

        public int GetUniformLocation(string name)
        {
            if(!_uniformLocations.ContainsKey(name))
            {
                throw new Exception($"Uniform not defined: [{name}]");
            }

            return _uniformLocations[name];
        }
    }
}
