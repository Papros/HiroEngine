using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HiroEngine.HiroEngine.Graphics.Shaders
{
    public struct Shader
    {
        public int ID { get; set; }

        public static Shader LoadShader(string path, ShaderType type)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new Exception("Shader path cannot be empty!");
            }

            if(!File.Exists(path))
            {
                throw new Exception($"Path for shader: [{path}] does not exist");
            }

            int shaderId = GL.CreateShader(type);
            GL.ShaderSource(shaderId, File.ReadAllText(path));
            GL.CompileShader(shaderId);
            
            string infoLog = GL.GetShaderInfoLog(shaderId);
            if(!string.IsNullOrEmpty(infoLog))
            {
                throw new Exception(infoLog);
            }

            return new Shader() { ID = shaderId };
        }
    }
}
