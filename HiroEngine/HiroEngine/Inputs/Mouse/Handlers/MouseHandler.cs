using HiroEngine.HiroEngine.Engine.Elements;
using HiroEngine.HiroEngine.Inputs.Enums;
using HiroEngine.HiroEngine.Inputs.Mouse.Struct;
using HiroEngine.HiroEngine.Inputs.Shared.Core;
using System.Collections.Generic;

namespace HiroEngine.HiroEngine.Inputs.Mouse
{
    public class MouseHandler
    {
        private float _lastPosX, _lastPosY;
        public bool moved = false;

        public float MouseX => _lastPosX;
        public float MouseY => _lastPosY;

        internal Dictionary<MouseAction, Behaviour> MouseBindings;

        public MouseHandler()
        {
            MouseBindings = new Dictionary<MouseAction, Behaviour>();
        }

        public void OnMouseDown(MouseAction key, InputType action, CorespondingKeyEvent modifier, bool isPressed)
        {
            if(MouseBindings.ContainsKey(key)) MouseBindings[key]?.Run(new MouseEventState()
            {
                mouseX = _lastPosX,
                mouseY = _lastPosY,
                mouseDeltaX = 0,
                mouseDeltaY = 0,
                inputType = action,
                corespondingKeyEvent = modifier,
                mouseWheelOffset = 0,
                time = 0
            });
        }

        public void OnMouseMove(float mouseX, float mouseY)
        {
            float deltaX = 0, deltaY = 0;
            if (!moved)
            {
                _lastPosX = mouseX;
                _lastPosY = mouseY;
                moved = true;
            }
            else
            {
                deltaX = mouseX - _lastPosX;
                deltaY = mouseY - _lastPosY;
                _lastPosX = mouseX;
                _lastPosY = mouseY;
            }

            if (MouseBindings.ContainsKey(MouseAction.Move)) MouseBindings[MouseAction.Move]?.Run(new MouseEventState()
                {
                    mouseX = _lastPosX,
                    mouseY = _lastPosY,
                    mouseDeltaX = deltaX,
                    mouseDeltaY = deltaY,
                    inputType = InputType.Press,
                    corespondingKeyEvent = 0,
                    mouseWheelOffset = 0,
                    time = 0
                });
        }

        public void OnMouseWheel(float offset)
        {
            if (MouseBindings.ContainsKey(MouseAction.Wheel)) MouseBindings[MouseAction.Wheel]?.Run(new MouseEventState()
                {
                    mouseX = _lastPosX,
                    mouseY = _lastPosY,
                    mouseDeltaX = 0,
                    mouseDeltaY = 0,
                    inputType = 0,
                    corespondingKeyEvent = 0,
                    mouseWheelOffset = offset,
                    time = 0
            });
        }

        public void ResetMouseState()
        {
            moved = false;
        }
    }
}
