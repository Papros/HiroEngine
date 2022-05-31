using HiroEngine.HiroEngine.Graphics.Shaders;

namespace HiroEngine.HiroEngine.Graphics.Elements
{
    public interface IDrawable
    {
        public void Draw(ShaderProgram shader);
    }
}
