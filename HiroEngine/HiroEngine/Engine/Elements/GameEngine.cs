using HiroEngine.HiroEngine.Data.Logger;
using HiroEngine.HiroEngine.Graphics.Window;
using HiroEngine.HiroEngine.Inputs;
using HiroEngine.HiroEngine.Inputs.handlers;
using HiroEngine.HiroEngine.Inputs.Mouse;
using HiroEngine.HiroEngine.Physics.Complex;

namespace HiroEngine.HiroEngine.Engine.Elements
{
    public class GameEngine
    {
        readonly string LOGGER_ID = "Engine:Core";
        public Scene Scene { get; set; }
        public GUIScene GUIScene { get; set; }
        public AppWindow Window { get; set; }
        public PhysicsEngine Physics { get; set; }
        public InputManager InputManager => Window?.Input;

        public GameEngine()
        {
            Window = new AppWindow(AppWindow.GetGameWindowSettings(), AppWindow.GetWindowSettings());
            Physics = new PhysicsEngine(this);
            SetupHandlers();
        }

        public void Run(int width, int height, string title = "HiroEngine")
        {
            Window.Size = new OpenTK.Mathematics.Vector2i(width, height);
            Window.Title = title;
            Window.Run();
            Logger.Info(LOGGER_ID, $"Starting windows [{title}] {width} by {height}");
        }

        public void Render(Scene scene)
        {
            Scene = scene;
            Window.Scene = scene;
            Logger.Info(LOGGER_ID, $"Setting up new scene: {scene.Size} objects");
        }

        public void Render(GUIScene scene)
        {
            GUIScene = scene;
            Window.GUIScene = scene;
            Logger.Info(LOGGER_ID, $"Setting up new scene: {scene.Size} objects");
        }

        private void SetupHandlers()
        {
            Window.Input.PushHandler(new MouseHandler());
            Window.Input.PushHandler(new KeyboardHandler());
            Logger.Info(LOGGER_ID, "Handlers settup");
        }
    }
}
