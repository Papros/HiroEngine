using HiroEngine.HiroEngine.Graphics.World;

namespace HiroEngine.HiroEngine.Inputs.Mouse
{
    public interface IClickHandleStrategy
    {
        public void HandleClick(int x, int y, Camera cam);

        public int RegisterClickHandler(IClickable clickable);
        public bool UnregisterClickHandler(int objectid);
        public bool ClearRegistred();
    }
}
