using HiroEngine.HiroEngine.Graphics.Window;
using HiroEngine.HiroEngine.Inputs.handlers;
using HiroEngine.HiroEngine.Inputs.Mouse;

namespace HiroEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            AppWindow game = new AppWindow(AppWindow.GetGameWindowSettings(), AppWindow.GetWindowSettings(1280, 720, "Test2"));
            game.Input.MouseHandler = new BasicMouseHandler(game.Camera);
            game.Input.KeyboardHandler = new BasicKeyboardHandler(game.Camera);
            game.Input.GetSubscribtionFromHandler();
            game.Run();
        }
    }
}
