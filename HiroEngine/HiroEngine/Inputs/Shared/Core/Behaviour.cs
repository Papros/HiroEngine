using HiroEngine.HiroEngine.Engine.Elements;
using System;

namespace HiroEngine.HiroEngine.Inputs.Shared.Core
{
    public class Behaviour<T>
    {
        public GameEngine Target { get; private set; }
        public Action<GameEngine, T> Action { get; set; }
        private bool Active;  

        public Behaviour(Action<GameEngine, T> callback, GameEngine target = null)
        {
            Action = callback;
            Target = target;
            Active = true;
        }

        public void Run(T additional = default)
        {
            if(Active) Action.Invoke(Target, additional);
        }

        public void SetActive(bool isActive)
        {
            Active = isActive;  
        }
    }
}
