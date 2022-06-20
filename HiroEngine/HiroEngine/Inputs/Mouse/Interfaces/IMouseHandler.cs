using HiroEngine.HiroEngine.Inputs.Enums;

namespace HiroEngine.HiroEngine.Inputs.Mouse
{
    public interface IMouseHandler
    {
        public void OnMouseWheel(float offset);
        public void OnMouseMove(float mouseX, float mouseY);
        public void OnMouseDown(MouseKeys key, InputType action, CorespondingKeyEvent modifier, bool isPressed, float mouseX, float mouseY);
    }
}
