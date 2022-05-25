using HiroEngine.HiroEngine.Data.Logger;
using HiroEngine.HiroEngine.Graphics.Elements;
using HiroEngine.HiroEngine.Graphics.Shaders;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiroEngine.HiroEngine.GUI.Elements
{
    public class UIElement : Clickable
    {
        public List<UIElement> ChildElements { get; private set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public bool AbsolutePosition { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Mesh Element { get; set; }

        public UIElement(Vector2 position, Vector2 size, bool absPosition = false, bool active = false, bool visible = true)
        {
            ChildElements = new List<UIElement>();
            AbsolutePosition = absPosition;
            Position = position;
            Size = size;
            IsActive = active;
            IsVisible = visible;
        }

        public void AddChild(UIElement child)
        {
            ChildElements.Add(child);
        }

        public void RemoveChild(UIElement child)
        {
            ChildElements.Remove(child);
        }

        public void Draw(ShaderProgram shaderProgram)
        {
            if (IsVisible)
            {
                shaderProgram.SetMatrix4(ShaderProgram.Uniforms.MATRIX.MODEL, Matrix4.CreateTranslation(new Vector3(Position.X, Position.Y, 0.0f)));
                Element.Draw(shaderProgram);
                ChildElements.ForEach(element => element.Draw(shaderProgram));
            }
        }

        public void OnClick()
        {
            if(IsActive)
            {
                Logger.Debug( "Button", "Clicked");
            }
        }

    }
}
