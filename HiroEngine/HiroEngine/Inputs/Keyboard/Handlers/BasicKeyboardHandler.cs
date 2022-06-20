using HiroEngine.HiroEngine.Graphics.World;
using HiroEngine.HiroEngine.Inputs.Enums;
using HiroEngine.HiroEngine.Inputs.interfaces;

namespace HiroEngine.HiroEngine.Inputs.handlers
{
    class BasicKeyboardHandler : IKeyboardHandler
    {
        private Camera _camera;
        public float CameraSpeed { get; set; }
        public BasicKeyboardHandler(Camera cam)
        {
            _camera = cam;
            CameraSpeed = 2.0f;
        }

        public void OnKeyAction(KeyboardKeys key, bool isPressed, bool wasPressed, float time)
        {
            //Console.WriteLine($"KeyAction: {key}, {isPressed}:{wasPressed},{time}");    

            if(isPressed)
            switch(key)
            {
                case KeyboardKeys.W:
                    _camera.Position += _camera.Front * CameraSpeed * time;
                    break;
                case KeyboardKeys.D:
                    _camera.Position += _camera.Right * CameraSpeed * time;
                    break;
                case KeyboardKeys.S:
                    _camera.Position -= _camera.Front * CameraSpeed * time;
                    break;
                case KeyboardKeys.A:
                    _camera.Position -= _camera.Right * CameraSpeed * time;
                    break;
                case KeyboardKeys.Space:
                    _camera.Position += _camera.Up * CameraSpeed * time;
                    break;
                case KeyboardKeys.LeftShift:
                    _camera.Position -= _camera.Up * CameraSpeed * time;
                    break;
                default:break;
            }
        }

        public KeyboardKeys[] GetSubscribtionList()
        {
            return new KeyboardKeys[]{
                KeyboardKeys.W,
                KeyboardKeys.A,
                KeyboardKeys.S,
                KeyboardKeys.D,
                KeyboardKeys.Space,
                KeyboardKeys.LeftShift
                };
        }

    }
}
