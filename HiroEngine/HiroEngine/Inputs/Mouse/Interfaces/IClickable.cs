using HiroEngine.HiroEngine.Graphics.Elements;

namespace HiroEngine.HiroEngine.Inputs.Mouse
{
    public interface IClickable
    {
        public void OnClick();

        public IDrawable GetDrawable();
    }
}
