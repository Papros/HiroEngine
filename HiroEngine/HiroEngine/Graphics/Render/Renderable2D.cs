using HiroEngine.HiroEngine.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiroEngine.HiroEngine.Graphics.Render
{
    public abstract class Renderable2D
    {
        struct VertexData
        {
            Vector3 vertex;
            uint color;
        }

        public Renderable2D(Vector3 position, Vector2 size, Vector4 color)
        {
            Position = position;
            Size = size;
            Color = color;
        }

        public Vector3 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector4 Color { get; set; }

        virtual public void Submit(Renderer2D renderer)
        {
            renderer.Submit(this);
        }

    }
}
