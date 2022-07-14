using HiroEngine.HiroEngine.Inputs.Shared.Core;
using HiroEngine.HiroEngine.Inputs.Window.Enums;
using System.Collections.Generic;

namespace HiroEngine.HiroEngine.Inputs.Window.Handlers
{
    public class WindowHandler
    {
        public Dictionary<WindowAction, Behaviour<WindowAction>> WindowBindings { get; private set; }
        public WindowHandler()
        {
            WindowBindings = new Dictionary<WindowAction, Behaviour<WindowAction>>();
        }

        public void OnWindowAction(WindowAction key)
        {
            if(WindowBindings.ContainsKey(key)) WindowBindings[key]?.Run();
        }
    }
}
