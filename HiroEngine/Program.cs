using HiroEngine.HiroEngine.Graphics.Core;
using System;
using System.Diagnostics;
using HiroEngine.HiroEngine.Graphics.Window;

namespace HiroEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Window game = new Window(1280, 720, "Test");
            game.Run();
        }
    }
}
