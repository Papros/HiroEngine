using HiroEngine.HiroEngine.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiroEngine.HiroEngine.Graphics.Render
{
    public abstract class Renderer2D
    {
        Stack<Mat4> TransformationStack;

        public void Push(ref Mat4 transformation, bool replace = false)
        {
            if(replace)
            {
                TransformationStack.Push(transformation);
            } else
            {
                TransformationStack.Push(TransformationStack.Peek().Multiply(ref transformation));
            }
        }

        public void Pop()
        {
            TransformationStack.Pop();
        }

        public virtual void Begin() { return; }
        public virtual void Submit(Renderable2D renderable) { return; }
        public virtual void End() { return; }
        public virtual void Flush() { return; }
    }
}
