using HiroEngine.HiroEngine.Inputs.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiroEngine.HiroEngine.Inputs.interfaces
{
    public interface IMouseHandler
    {
        public void OnMouseWheel(float offset);
        public void OnMouseMove(float mouseX, float mouseY);
        public void OnMouseDown(MouseKeys key, InputType action, CorespondingKeyEvent modifier, bool isPressed);
    }
}
