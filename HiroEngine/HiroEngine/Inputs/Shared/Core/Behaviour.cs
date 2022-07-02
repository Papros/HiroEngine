using HiroEngine.HiroEngine.Engine.Elements;
using System;

namespace HiroEngine.HiroEngine.Inputs.Shared.Core
{
    public class Behaviour
    {
        public GameEngine Target { get; private set; }
        public Action<GameEngine, object> Action { get; set; }
        private bool Active;  

        public Behaviour(Action<GameEngine, object> callback, GameEngine target = null)
        {
            Action = callback;
            Target = target;
            Active = true;
        }

        public void Run(object additional = null)
        {
            if(Active) Action.Invoke(Target, additional);
        }

        public void SetActive(bool isActive)
        {
            Active = isActive;  
        }
    }
}
