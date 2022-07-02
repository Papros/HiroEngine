using OpenTK.Mathematics;

namespace HiroEngine.HiroEngine.Inputs.Mouse
{
    public interface IClickable
    {
        public void OnClick();

        public bool Clicked(Vector2 mouse);

    }
}
