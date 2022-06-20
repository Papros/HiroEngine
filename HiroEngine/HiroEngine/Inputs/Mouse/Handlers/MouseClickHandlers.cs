using HiroEngine.HiroEngine.Engine.Elements;
using HiroEngine.HiroEngine.Graphics.World;

namespace HiroEngine.HiroEngine.Inputs.Mouse
{
    public class MouseClickHandlers
    {
        
        public enum ClickHandleStrategy
        {
            CHEAT,
        }

        private IClickHandleStrategy clickHandle;

        public MouseClickHandlers(ClickHandleStrategy strategy)
        {
            switch(strategy)
            {
                case ClickHandleStrategy.CHEAT:
                    clickHandle = new CheatClickStrategy();
                    break;
                default:
                    clickHandle = new CheatClickStrategy();
                    break;
            }
        }

        public void HandleClick(int x, int y, GameEngine engine)
        {
            clickHandle.HandleClick(x, y, engine);
        }

        public int Register(IClickable clickable)
        {
            return clickHandle.RegisterClickHandler(clickable);
        }

        public bool Unregister(int id)
        {
            return clickHandle.UnregisterClickHandler(id);
        }

        public bool ClearRegistred()
        {
            return clickHandle.ClearRegistred();
        }

    }
}
