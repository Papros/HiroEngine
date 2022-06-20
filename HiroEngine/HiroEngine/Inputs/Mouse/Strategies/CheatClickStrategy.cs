using HiroEngine.HiroEngine.Graphics.Shaders;
using HiroEngine.HiroEngine.Graphics.World;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using HiroEngine.HiroEngine.Data.Logger;
using HiroEngine.HiroEngine.Engine.Elements;

namespace HiroEngine.HiroEngine.Inputs.Mouse
{
    public class CheatClickStrategy : IClickHandleStrategy
    {
        private Dictionary<int, IClickable> clickableDictionary;
        private List<IClickable> drawQueue;
        private ShaderProgram cheatClickShader;
        private int colorID = 000001;

        public CheatClickStrategy()
        {
            clickableDictionary = new Dictionary<int, IClickable>();
            drawQueue = new List<IClickable>();
            cheatClickShader = new ShaderProgram(ShaderProgram.VertexShaderType.BASIC, ShaderProgram.FragmentShadersType.CLICKCHEAT);
        }

        public bool ClearRegistred()
        {
            clickableDictionary.Clear();
            return true;
        }

        public void HandleClick(int x, int y, GameEngine engine)
        {
            Logger.Warn("Click!?!", $"Clicked on ({x}, {y}): id = unknown");

            engine.Physics.Raycast(engine.Window.Camera.GetCameraRay());

            /*
            cheatClickShader.Use();
            drawQueue.ForEach((clickable) => clickable.GetDrawable().Draw(cheatClickShader));
            GL.ReadBuffer(ReadBufferMode.ColorAttachment1);
            var pixel = new float[1];
            GL.ReadPixels(x, y, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

            Logger.Warn("Click!?!",$"Clicked on ({x}, {y}): id = { pixel[0] }");

            */
        }

        public int RegisterClickHandler(IClickable clickable)
        {
            int clickId = generateNewKey();
            clickableDictionary.Add(clickId, clickable);
            drawQueue.Add(clickable);
            return clickId;
        }

        public bool UnregisterClickHandler(int objectid)
        {
            drawQueue.Remove(clickableDictionary[objectid]);
            return clickableDictionary.Remove(objectid);
        }

        private int generateNewKey()
        {
            colorID++;
            return colorID;
        }
    }
}
