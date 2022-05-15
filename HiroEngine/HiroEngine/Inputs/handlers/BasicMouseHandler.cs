using HiroEngine.HiroEngine.Graphics.World;
using HiroEngine.HiroEngine.Inputs.Enums;
using HiroEngine.HiroEngine.Inputs.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiroEngine.HiroEngine.Inputs.handlers
{
    public class BasicMouseHandler : IMouseHandler
    {
        public float Sensitivity { get; set; }

        private Camera _camera;
        private float _lastPosX, _lastPosY;
        private bool moved = false;
        public BasicMouseHandler(Camera cam, float sensitivity = 0.15f)
        {
            _camera = cam;
            Sensitivity = sensitivity;
        }

        public void OnMouseDown(MouseKeys key, InputType action, CorespondingKeyEvent modifier, bool isPressed)
        {
           //Do nothing
        }

        public void OnMouseMove(float mouseX, float mouseY)
        {
            if (!moved)
            {
                _lastPosX = mouseX;
                _lastPosY = mouseY;
                moved = true;
            }
            else
            {
                var deltaX = mouseX - _lastPosX;
                var deltaY = mouseY - _lastPosY;
                _lastPosX = mouseX;
                _lastPosY = mouseY;
                _camera.Yaw += deltaX * Sensitivity;
                _camera.Pitch -= deltaY * Sensitivity;
            }
        }

        public void OnMouseWheel(float offset)
        {
            _camera.Fov -= offset;
        }
    }
}
