using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using HiroEngine.HiroEngine.Graphics.Shaders;
using System.ComponentModel;
using HiroEngine.HiroEngine.Graphics.Elements;
using OpenTK.Mathematics;
using HiroEngine.HiroEngine.Inputs;
using HiroEngine.HiroEngine.Graphics.World;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Generic;
using HiroEngine.HiroEngine.GUI.Elements;
using HiroEngine.HiroEngine.Inputs.Mouse;

namespace HiroEngine.HiroEngine.Graphics.Window
{
    public class AppWindow : GameWindow
    {

        private ShaderProgram _shaderProgram, _guiShaderProgram;
        private List<UIElement> _guiList;
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
            Camera = new Camera(new Vector3(0,10,5), (float)Size.X / (float)Size.Y );
            AppSettings = AppWindowSettings.GetDefaultSettings();
            ReloadSettings();
            _scene = new List<Model>();
            _guiList = new List<UIElement>();
        }

        public void ReloadSettings()
        {
            CursorVisible = true;// AppSettings.CursorVisible;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            _shaderProgram = new ShaderProgram(ShaderProgram.VertexShaderType.BASIC, ShaderProgram.FragmentShadersType.BASIC);
            _guiShaderProgram = new ShaderProgram(ShaderProgram.VertexShaderType.BASIC_UI, ShaderProgram.FragmentShadersType.BASIC_UI);

            _shaderProgram.Use();

            CursorGrabbed = false;// true;
            Texture towerText = new Texture("wooden_tower/textures/Wood_Tower_Col.jpg");

            Model tower = new Model("wooden_tower/tower2.dae");
            tower.Components[0].AddTexture(new Texture[] { towerText });
            tower.AddPosition(new Vector3(0, 0, 0));
            tower.AddRotation(-1.5708f, 0, 0);

            Model tower2 = new Model("wooden_tower/tower2.dae");
            tower2.Components[0].AddTexture(new Texture[] { towerText });
            tower2.AddPosition(new Vector3(-10, 0, 0));
            tower2.AddRotation(-1.5708f, 0, 0);

            Model tower3 = new Model("wooden_tower/tower2.dae");
            tower3.Components[0].AddTexture(new Texture[] { towerText });
            tower3.AddPosition(new Vector3(10, 0, 0));
            tower3.AddRotation(-1.5708f, 0, 0);

            _scene.Add(tower);
            _scene.Add(tower2);
            _scene.Add(tower3);
            
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f); // Setting "default" background color
            GL.Enable(EnableCap.DepthTest); // Enabling depth test, 
            // GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line); To draw only edges


            Shape floor = Shape.Plane(new Vector3(-15, 0, -5), new Vector2(30, 10));
            floor.AddTexture(new Texture[] { new Texture("container.png") });
            _scene.Add(new Model(floor));

            //_scene.Add(new Model(Mesh.TestData()));

            UIElement minimap = new UIElement(new Vector2(-0.98f, 0.48f), new Vector2(1, 1));
            minimap.Element = Shape2D.Rectangle(new Vector2(0, 0), new Vector2(0.5f, 0.5f));
            minimap.Element.AddTexture(new Texture[] { new Texture("UI/minimap.png") });
            _guiList.Add(minimap);

            UIElement bar = new UIElement(new Vector2(-0.3f, -0.98f), new Vector2(0.6f, 0.2f));
            bar.Element = Shape2D.Rectangle(new Vector2(0, 0), new Vector2(0.6f, 0.2f));
            bar.Element.AddTexture(new Texture[] { new Texture("UI/bar.png") });
            _guiList.Add(bar);

            _shaderProgram.SetMatrix4(ShaderProgram.Uniforms.MATRIX.PROJECTION, Camera.GetProjectionMatrix());
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

            _scene.ForEach((model) =>
            {
                model.Draw(_shaderProgram);
            });

            _guiShaderProgram.Use();
            _guiList.ForEach((guiElement) =>
            {
                guiElement.Draw(_guiShaderProgram);
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
