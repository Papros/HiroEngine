using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics;
using OpenTK.Windowing.Desktop;
using HiroEngine.HiroEngine.Graphics.Shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Mathematics;
using HiroEngine.HiroEngine.Graphics.Elements;

namespace HiroEngine.HiroEngine.Graphics.Window
{
    public class Window
    {
        GameWindow gameWindow;

        public Window(int width, int height, string title)
        {
            GameWindowSettings gameSetting = GameWindowSettings.Default;
            NativeWindowSettings nativeSetting = NativeWindowSettings.Default;

            nativeSetting.Size = new OpenTK.Mathematics.Vector2i(width, height);
            nativeSetting.Title = title;
            nativeSetting.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;

            gameSetting.RenderFrequency = 120;

            gameWindow = new GameWindow(gameSetting, nativeSetting);

            ShaderProgram shaderProgram = new ShaderProgram() { ID = 0 };

            int vertexArrayId = 0;
            int verticesBufferId = 0;
            int elementsBufferId = 0;

            Vector2 mousePosition = new Vector2(0, 0);
            Texture texture1 = new Texture(0);

            gameWindow.Load += () =>
            {
                shaderProgram = new ShaderProgram();
                shaderProgram.LoadShaderProgram(ShaderProgram.VertexShaderType.BASIC, ShaderProgram.FragmentShadersType.BASIC).Use();

                vertexArrayId = GL.GenVertexArray();
                GL.BindVertexArray(vertexArrayId);

                verticesBufferId = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, verticesBufferId);

                elementsBufferId = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementsBufferId);

                GL.ActiveTexture(TextureUnit.Texture0);

                texture1 = new Texture("container.png");
                shaderProgram.SetInt("texture1", 0);
                texture1.Use(TextureUnit.Texture0);
            };

            gameWindow.MouseMove += (MouseMoveEventArgs args) =>
            {
                mousePosition.X = (2*args.X) / (width) - 1;
                mousePosition.Y = -((2*args.Y) / (height) - 1);
                shaderProgram.SetVector2("light_position", new Vector2(mousePosition.X, mousePosition.Y));
                //Console.WriteLine($"mousePos: {mousePosition.X} | { mousePosition.Y} => ");
                Console.WriteLine($"{(int)(1/gameWindow.RenderTime)} FPS");
            };

            float[] vertices1 =
            {
              // positions        // colors          //texture cords
              0.5f,  0.5f, 0.0f,  0.0f, 0.0f, 1.0f,  1.0f, 1.0f, // top right
              0.5f, -0.5f, 0.0f,  1.0f, 0.0f, 0.0f,  1.0f, 0.0f, // bottom right
             -0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,  0.0f, 0.0f, // bottom left
              -0.5f,  0.5f, 0.0f,  1.0f, 1.0f, 1.0f,  0.0f, 1.0f, // top left
            };

            float[] vertices2 =
            {
              // positions        // colors         
              0.5f,  0.5f, 0.0f,  0.0f, 0.0f, 1.0f, 
              0.5f, -0.5f, 0.0f,  1.0f, 0.0f, 0.0f, 
             -0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f, 
              -0.5f,  0.5f, 0.0f,  1.0f, 1.0f, 1.0f,
            };

            float[] vertices = vertices1;

            uint[] indices = {  // note that we start from 0!
                0, 1, 3,   // first triangle
                1, 2, 3    // second triangle
            };

            gameWindow.RenderFrame += (FrameEventArgs args) =>
            {
                GL.Clear(ClearBufferMask.ColorBufferBit);

                GL.BindVertexArray(vertexArrayId);

                texture1.Use(TextureUnit.Texture0);

                GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticCopy);

                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

                GL.EnableVertexAttribArray(2);
                GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));

                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticCopy);

                GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
                gameWindow.SwapBuffers();
            };

            gameWindow.Closing += (System.ComponentModel.CancelEventArgs args) =>
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
                GL.BindVertexArray(0);
                GL.DeleteVertexArray(vertexArrayId);
                GL.DeleteBuffer(verticesBufferId);
                GL.DeleteBuffer(elementsBufferId);
            };

            gameWindow.Resize += (ResizeEventArgs args) =>
            {
               GL.Viewport(0, 0, gameWindow.Size.X, gameWindow.Size.Y);
            };
        }

        public void AddMouseMoveEvent(Action<MouseMoveEventArgs> function)
        {
            gameWindow.MouseMove += (MouseMoveEventArgs args) => function(args);
        }

        public void AddMouseEvents(Action<MouseButtonEventArgs> mouseDown, Action<MouseButtonEventArgs> mouseUp)
        {
            gameWindow.MouseDown += (MouseButtonEventArgs args) => mouseDown(args);
            gameWindow.MouseUp += (MouseButtonEventArgs args) => mouseUp(args);
        }

        public void AddWindowLoadEvents(Action function)
        {
            gameWindow.Load += () => function();
        }

        public void Run()
        {
            gameWindow.Run();
        }
    }
}
