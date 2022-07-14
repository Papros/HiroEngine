using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using HiroEngine.HiroEngine.Graphics.Shaders;
using System.ComponentModel;
using OpenTK.Mathematics;
using HiroEngine.HiroEngine.Inputs;
using HiroEngine.HiroEngine.Graphics.World;
using OpenTK.Windowing.GraphicsLibraryFramework;
using HiroEngine.HiroEngine.Engine.Elements;
using HiroEngine.HiroEngine.Inputs.Mouse;
using HiroEngine.HiroEngine.Inputs.Enums;

namespace HiroEngine.HiroEngine.Graphics.Window
{
    public class AppWindow : GameWindow
    {

        private ShaderProgram _shaderProgram, _guiShaderProgram, _debugShaderProgram;

        internal GUIScene GUIScene;
        internal Scene Scene;

        private double _time;

        public InputManager Input { get; set; }
        public Camera Camera { get; set; }

        public AppWindowSettings AppSettings { get; set; }

        public static NativeWindowSettings GetWindowSettings(int width = 1280, int height = 720, string title = "HiroEngine")
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
            Camera = new Camera(new Vector3(0,10,5), (float)Size.X / (float)Size.Y );
            AppSettings = AppWindowSettings.GetDefaultSettings();
            ReloadSettings();
            Scene = new Scene();
            GUIScene = new GUIScene();
        }

        public void ReloadSettings()
        {
            CursorVisible = AppSettings.CursorVisible;
            CursorGrabbed = !AppSettings.CursorVisible;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            _shaderProgram = new ShaderProgram(ShaderProgram.VertexShaderType.BASIC, ShaderProgram.FragmentShadersType.BASIC);
            _guiShaderProgram = new ShaderProgram(ShaderProgram.VertexShaderType.BASIC_UI, ShaderProgram.FragmentShadersType.BASIC_UI);

            _shaderProgram.Use();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f); // Setting "default" background color
            GL.Enable(EnableCap.DepthTest); // Enabling depth test, 
            GL.DepthFunc(DepthFunction.Less);
            GL.Enable(EnableCap.ClipDistance1);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            _shaderProgram.SetMatrix4(ShaderProgram.Uniforms.MATRIX.PROJECTION, Camera.GetProjectionMatrix());

            if (AppSettings.Debug)
            {
                _debugShaderProgram = new ShaderProgram(ShaderProgram.VertexShaderType.BASIC, ShaderProgram.FragmentShadersType.DEBUG);
                _debugShaderProgram.SetMatrix4(ShaderProgram.Uniforms.MATRIX.PROJECTION, Camera.GetProjectionMatrix());
            }

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            
            _time += e.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _shaderProgram.Use();
            _shaderProgram.SetMatrix4(ShaderProgram.Uniforms.MATRIX.VIEW, Camera.GetViewMatrix());

            Scene.Draw(_shaderProgram);
            if (AppSettings.Debug)
            {
                _shaderProgram.SetFlag(ShaderProgram.Uniforms.SETTINGS.DEBUG, true);
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line); //To draw only edges
                Scene.DrawCollider(_shaderProgram);
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill); // To draw only edges
                _shaderProgram.SetFlag(ShaderProgram.Uniforms.SETTINGS.DEBUG, false);
            }
            _guiShaderProgram.Use();
            GUIScene.Draw(_guiShaderProgram);

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

            Input.SubscribedKeys.ForEach(Key =>
            {
                Input.OnKeyboardAction(Key, input.IsKeyDown((Keys)Key), input.WasKeyDown((Keys)Key), (float)e.Time);
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
            Input.OnMouseDown((MouseAction) e.Button, (InputType) e.Action, (CorespondingKeyEvent)e.Modifiers, e.IsPressed );
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            Input.OnMouseMove(e.X, e.Y);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            Input.OnMouseWheel(e.OffsetY);
        }
    }
}
