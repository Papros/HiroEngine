using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using OpenTK.Graphics.OpenGL4;
using HiroEngine.HiroEngine.Graphics.Shaders;
using System.ComponentModel;
using HiroEngine.HiroEngine.Graphics.Elements;
using OpenTK.Mathematics;
using HiroEngine.HiroEngine.Inputs;
using HiroEngine.HiroEngine.Graphics.World;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Generic;

namespace HiroEngine.HiroEngine.Graphics.Window
{
    public class AppWindow : GameWindow
    {

        private const float NEAR_CLIPPING = 0.1f;
        private const float FAR_CLIPPING = 100.0f;
        private const float ANGLE_FOW = 45.0f;

        private int _vertexArrayId = 0;
        private int _vertexBufferId = 0;
        private int _elementsBufferId = 0;

        private float[] _vertices = {
                -0.5f, -0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 0.0f,
                 0.5f,  0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 1.0f,
                 0.5f, -0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 0.0f,
                 0.5f,  0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 1.0f,
                -0.5f,  0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 1.0f,
                -0.5f, -0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 0.0f,

                -0.5f, -0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 0.0f,
                 0.5f, -0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 0.0f,
                 0.5f,  0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 1.0f,
                 0.5f,  0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 1.0f,
                -0.5f,  0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 1.0f,
                -0.5f, -0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 0.0f,

                -0.5f,  0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 0.0f,
                -0.5f,  0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 1.0f,
                -0.5f, -0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 1.0f,
                -0.5f, -0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 1.0f,
                -0.5f, -0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 0.0f,
                -0.5f,  0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 0.0f,

                 0.5f,  0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 0.0f,
                 0.5f,  0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 1.0f,
                 0.5f, -0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 1.0f,
                 0.5f, -0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 1.0f,
                 0.5f, -0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 0.0f,
                 0.5f,  0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 0.0f,

                -0.5f, -0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 1.0f,
                 0.5f, -0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 1.0f,
                 0.5f, -0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 0.0f,
                 0.5f, -0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 0.0f,
                -0.5f, -0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 0.0f,
                -0.5f, -0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 1.0f,

                -0.5f,  0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 1.0f,
                 0.5f,  0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 1.0f,
                 0.5f,  0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 0.0f,
                 0.5f,  0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 1.0f, 0.0f,
                -0.5f,  0.5f,  0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 0.0f,
                -0.5f,  0.5f, -0.5f,  0.0f, 0.0f, 1.0f, 0.0f, 1.0f
            }; //vert                 //color           //text

        private readonly float[] vertices =
        {
            // Position         color             texture coordinates
             0.5f,  0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f  // top left
        };

        uint[] indices = {  // note that we start from 0!
                0, 1, 3,   // first triangle
                1, 2, 3    // second triangle
            };

        private ShaderProgram _shaderProgram;
        private Texture _texture;
        private Matrix4 _model, _view, _projection;

        private List<Model> _scene;

        private double _time;

        public InputManager Input { get; private set; }
        public Camera Camera { get; private set; }

        public AppWindowSettings AppSettings { get; private set; }

        public static NativeWindowSettings GetWindowSettings(int width, int height, string title)
        {
            NativeWindowSettings nativeSetting = NativeWindowSettings.Default;
            nativeSetting.Size = new OpenTK.Mathematics.Vector2i(width, height);
            nativeSetting.Title = title;
            nativeSetting.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            return nativeSetting;
        }

        public static GameWindowSettings GetGameWindowSettings()
        {
            return GameWindowSettings.Default;
        }

        public AppWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            Input = new InputManager();
            Camera = new Camera(Vector3.UnitZ * 3, (float)Size.X / (float)Size.Y );
            AppSettings = AppWindowSettings.GetDefaultSettings();
            ReloadSettings();
            _scene = new List<Model>();
        }

        public void ReloadSettings()
        {
            CursorVisible = AppSettings.CursorVisible;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f); // Setting "default" background color
            GL.Enable(EnableCap.DepthTest); // Enabling depth test, 

            _vertexArrayId = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayId);

            _vertexBufferId = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferId);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            _elementsBufferId = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementsBufferId);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            _shaderProgram = new ShaderProgram(ShaderProgram.VertexShaderType.BASIC, ShaderProgram.FragmentShadersType.BASIC);
            _shaderProgram.Use();

            Console.WriteLine("Test");

            GL.EnableVertexAttribArray(ShaderProgram.Uniforms.CORE.VERTEX_COORDS);
            GL.VertexAttribPointer(ShaderProgram.Uniforms.CORE.VERTEX_COORDS, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0 * sizeof(float));

            GL.EnableVertexAttribArray(ShaderProgram.Uniforms.CORE.VERTEX_COLOR);
            GL.VertexAttribPointer(ShaderProgram.Uniforms.CORE.VERTEX_COLOR, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

            GL.EnableVertexAttribArray(ShaderProgram.Uniforms.CORE.TEXTURE_COORDS);
            GL.VertexAttribPointer(ShaderProgram.Uniforms.CORE.TEXTURE_COORDS, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));

            GL.ActiveTexture(TextureUnit.Texture0);

            Model test = new Model(Mesh.TestData());

            _scene.Add(test);
            //_scene.Add(new Model("wooden_tower/tower2.dae"));

            //_texture = new Texture("wooden_tower/textures/Wood_Tower_Col.jpg");
            _texture = new Texture("container.png");
            _texture.Use(TextureUnit.Texture0);
            _shaderProgram.SetInt(ShaderProgram.Uniforms.TEXTURES.TEXTURE_1, 0);

            _view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(ANGLE_FOW), Size.X / (float)Size.Y, NEAR_CLIPPING, FAR_CLIPPING);
            CursorGrabbed = true;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.DeleteVertexArray(_vertexArrayId);
            GL.DeleteBuffer(_vertexBufferId);
            GL.DeleteBuffer(_elementsBufferId);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            _time += 4.0 * e.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.BindVertexArray(_vertexArrayId);

            _texture.Use(TextureUnit.Texture0);
            _shaderProgram.Use();

            _model = Matrix4.Identity;// * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_time));

            _shaderProgram.SetMatrix4(ShaderProgram.Uniforms.MATRIX.MODEL, _model);
            _shaderProgram.SetMatrix4(ShaderProgram.Uniforms.MATRIX.PROJECTION, Camera.GetProjectionMatrix());
            _shaderProgram.SetMatrix4(ShaderProgram.Uniforms.MATRIX.VIEW, Camera.GetViewMatrix());

             GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
           
            _scene.ForEach((mesh) =>
            {
                mesh.Draw(_shaderProgram.ID);
            });

            SwapBuffers();

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                if(AppSettings.LeaveByEscape)
                {
                    Close();
                }

                if(AppSettings.ShowCursorByEscape)
                {
                    CursorVisible = !CursorVisible;
                }
            }

            Input.SubscribeKeys.ForEach(Key =>
            {
                Input.OnKeyboardAction(Key, input.IsKeyDown(Key), input.WasKeyDown(Key), (float)e.Time);
            });
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Input.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            Input.OnMouseMove(e);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            Input.OnMouseWheel(e);
        }
    }
}
