using HiroEngine.HiroEngine.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiroEngine.HiroEngine.Graphics.Render.Renderable
{
    public class Sprite : Renderable2D
    {
        public Sprite(float x, float y, float width, float height, Vector4 color) : base(new Vector3(x, y, 0), new Vector2(width, height), color)
        {

        }
    }
}
