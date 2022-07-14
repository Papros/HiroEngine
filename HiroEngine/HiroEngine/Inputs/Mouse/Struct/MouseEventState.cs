using HiroEngine.HiroEngine.Inputs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiroEngine.HiroEngine.Inputs.Mouse.Struct
{
    public struct MouseEventState
    {
        public float mouseX;
        public float mouseY;
        public float mouseDeltaX;
        public float mouseDeltaY;
        public InputType inputType;
        public CorespondingKeyEvent corespondingKeyEvent;
        public float mouseWheelOffset;
        public float time;
    }
}
