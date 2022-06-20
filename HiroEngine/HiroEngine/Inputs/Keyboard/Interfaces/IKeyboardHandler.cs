using HiroEngine.HiroEngine.Inputs.Enums;

namespace HiroEngine.HiroEngine.Inputs.interfaces
{
    public interface IKeyboardHandler
    {
        public void OnKeyAction(KeyboardKeys key, bool isPressed, bool wasPressed, float time);
        public KeyboardKeys[] GetSubscribtionList();
    }
}
