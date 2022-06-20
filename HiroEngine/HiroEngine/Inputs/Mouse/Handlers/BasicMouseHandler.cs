using HiroEngine.HiroEngine.Engine.Elements;
using HiroEngine.HiroEngine.Graphics.World;
using HiroEngine.HiroEngine.Inputs.Enums;

namespace HiroEngine.HiroEngine.Inputs.Mouse
{
    public class BasicMouseHandler : IMouseHandler
    {
        public float Sensitivity { get; set; }

        private GameEngine engine;
        private float _lastPosX, _lastPosY;
        private bool moved = false;

        private MouseClickHandlers clickHandler;

        public BasicMouseHandler(GameEngine engine, float sensitivity = 0.15f)
        {
            this.engine = engine;
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
            clickHandler.HandleClick((int)mouseX, (int)mouseY, engine);
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
                engine.Window.Camera.Yaw += deltaX * Sensitivity;
                engine.Window.Camera.Pitch -= deltaY * Sensitivity;
            }
        }

        public void OnMouseWheel(float offset)
        {
            engine.Window.Camera.Fov -= offset;
        }
    }
}
