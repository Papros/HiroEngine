using HiroEngine.HiroEngine.Graphics.Elements;
using HiroEngine.HiroEngine.Graphics.Shaders;
using HiroEngine.HiroEngine.GUI.Elements;
using System.Collections.Generic;

namespace HiroEngine.HiroEngine.Engine.Elements
{
    public class GUIScene : IDrawable
    {
        public List<UIElement> UIElements { get; private set; }
        public int Size { get { return UIElements.Count; } }

        public GUIScene()
        {
            UIElements = new List<UIElement>();
        }

        public void Draw(ShaderProgram shader)
        {
            UIElements.ForEach(guiElement => {
                guiElement.Draw(shader);
            });
        }
    }
}
