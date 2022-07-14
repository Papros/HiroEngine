using HiroEngine.HiroEngine.Data.Logger;
using HiroEngine.HiroEngine.Graphics.World;
using HiroEngine.HiroEngine.Inputs.Enums;
using HiroEngine.HiroEngine.Inputs.Keyboard.Struct;
using HiroEngine.HiroEngine.Inputs.Shared.Core;
using System.Collections.Generic;

namespace HiroEngine.HiroEngine.Inputs.handlers
{
    public class KeyboardHandler
    {
        private readonly string loggerPrefix = "KeyboardHandler";
        public Dictionary<KeyboardAction, Behaviour<KeyEventState>>  KeyBindings {get; private set;}
        public List<KeyboardAction> BindedKeys { get; private set; }
        public KeyboardHandler()
        {
            KeyBindings = new Dictionary<KeyboardAction, Behaviour<KeyEventState>>(); 
            BindedKeys = new List<KeyboardAction>();
        }

        public void OnKeyAction(KeyboardAction key, bool isDown, bool wasDown, float timeDelta)
        {
            if (KeyBindings.ContainsKey(key)) KeyBindings[key]?.Run(new KeyEventState()
            {
                isDown = isDown,
                wasDown = wasDown,
                timeDelta = timeDelta
            });
        }
    }
}
