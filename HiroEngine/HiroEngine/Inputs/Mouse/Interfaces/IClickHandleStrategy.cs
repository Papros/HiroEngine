using HiroEngine.HiroEngine.Graphics.World;
using HiroEngine.HiroEngine.Engine.Elements;

namespace HiroEngine.HiroEngine.Inputs.Mouse
{
    public interface IClickHandleStrategy
    {
        public void HandleClick(int x, int y, GameEngine engine);

        public int RegisterClickHandler(IClickable clickable);
        public bool UnregisterClickHandler(int objectid);
        public bool ClearRegistred();
    }
}
