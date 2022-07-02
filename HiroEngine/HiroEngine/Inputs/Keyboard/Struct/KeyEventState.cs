using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiroEngine.HiroEngine.Inputs.Keyboard.Struct
{
    public struct KeyEventState
    {
        public bool isDown;
        public bool wasDown;
        public float timeDelta;
        public bool justPressed => isDown && !wasDown;
        public bool justReleased => !isDown && wasDown;
    }
}
