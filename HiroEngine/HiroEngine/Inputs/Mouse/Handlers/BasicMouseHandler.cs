using HiroEngine.HiroEngine.Graphics.World;
using HiroEngine.HiroEngine.Inputs.Enums;

namespace HiroEngine.HiroEngine.Inputs.Mouse
{
    public class BasicMouseHandler : IMouseHandler
    {
        public float Sensitivity { get; set; }

        private Camera _camera;
        private float _lastPosX, _lastPosY;
        private bool moved = false;

        private MouseClickHandlers clickHandler;

        public BasicMouseHandler(Camera cam, float sensitivity = 0.15f)
        {
            _camera = cam;
            Sensitivity = sensitivity;
        }

        public void RegisterClickHandler()
        {
            clickHandler = new MouseClickHandlers(MouseClickHandlers.ClickHandleStrategy.CHEAT);
        }

        public void OnMouseDown(MouseKeys key, InputType action, CorespondingKeyEvent modifier, bool isPressed, float mouseX, float mouseY)
        {
            //Do nothing
            if (clickHandler == null) RegisterClickHandler();
            clickHandler.HandleClick((int)mouseX, (int)mouseY, _camera);
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
