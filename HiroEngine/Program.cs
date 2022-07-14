using HiroEngine.HiroEngine.Data.Logger;
using HiroEngine.HiroEngine.Engine.Elements;
using HiroEngine.HiroEngine.Graphics.Elements;
using HiroEngine.HiroEngine.GUI.Elements;
using HiroEngine.HiroEngine.Inputs.Enums;
using HiroEngine.HiroEngine.Inputs.Keyboard.Struct;
using HiroEngine.HiroEngine.Inputs.Mouse;
using HiroEngine.HiroEngine.Inputs.Mouse.Struct;
using HiroEngine.HiroEngine.Inputs.Shared.Core;
using HiroEngine.HiroEngine.Physics.Structures.Colliders;
using OpenTK.Mathematics;
using System;

namespace HiroEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            GameEngine game = new GameEngine();
            game.Render(SetupLevel(ref game));
            game.Window.AppSettings.CursorVisible = true;
            game.Window.ReloadSettings();
            game.Render(SetupUI(ref game));
            game.Run(1280, 720);   
        }

        private static Scene SetupLevel(ref GameEngine engine)
        {
            Texture towerText = new Texture("wooden_tower/textures/Wood_Tower_Col.jpg");
            Model tower = new Model("wooden_tower/tower2.dae");
            tower.Components[0].AddTexture(new Texture[] { towerText });

            WorldObject towerO1 = new WorldObject(tower, true).AddPosition(0, 0, 0).AddRotation(-1.5708f, 0, 0).AddTransform(0.7f);
            WorldObject towerO2 = new WorldObject(tower, true).AddPosition(-10, 0, 0).AddRotation(-1.5708f, -0.9f, 0).AddTransform(1.2f);
            WorldObject towerO3 = new WorldObject(tower, true).AddPosition(10, 0, 0).AddRotation(-1.5708f, 0.5f, 0).AddTransform(1.9f);

            Shape floor = Shape.Plane(new Vector3(-15, 0, -15), new Vector2(30, 30));
            floor.AddTexture(new Texture[] { new Texture("container.png") });

            Scene scene = new Scene();
            scene.worldObjects.Add(towerO1);
            scene.worldObjects.Add(towerO2);
            scene.worldObjects.Add(towerO3);
            scene.worldObjects.Add(
                    new WorldObject(new Model(floor),
                    false,
                    false, 
                    new PlaneCollider(new Vector3(-15,0,-15), new Vector3(-15,0,15), new Vector3(15,0,15), new Vector3(15,0,-15))
                    ));

            return scene;
        }

        private static GUIScene SetupUI(ref GameEngine engine)
        {
            GUIScene scene = new GUIScene();

            UIElement inv = new UIElement(new Vector2(-0.5f, -0.5f), new Vector2(1f, 1f), UIPositionBehaviour.CENTER);
            Texture textInv = new Texture("UI/inv.png");
            inv.Element.AddTexture(textInv);

            UIElement item = new UIElement(new Vector2(1,1), new Vector2(1, 1), UIPositionBehaviour.CENTER);
            Texture textItem = new Texture("UI/diamond.jpg");
            item.Element.AddTexture(textItem);
            
            UIElement inv5 = new UIElement(new Vector2(-1.0f, 1), new Vector2(1, 1), UIPositionBehaviour.CENTER);
            inv5.Element.AddTexture(textInv);

            Texture crosshair = new Texture("UI/crosshair.png");
            UIElement cross = new UIElement(new Vector2(0, 0), new Vector2(0.2f, 0.2f), UIPositionBehaviour.CENTER);
            cross.Element.AddTexture(crosshair);

            Behaviour<MouseEventState> raycast = new Behaviour<MouseEventState>((eng, _) =>
            {
                var intersection = eng.Physics.Raycast(eng.Window.Camera.GetCameraRay());

                if(intersection != null)
                {
                    Shape missle_shape = Shape.Cube(intersection.ShotPosition, new Vector3(0.1f, 0.1f, 0.1f));
                    WorldObject missle = new WorldObject(
                        new Model(missle_shape),
                        false,
                        true
                        );
                    missle.SetMovementVector(eng.Window.Camera.GetCameraRay().Vector, 5);

                    Shape floor = Shape.Cube(intersection.HitPosition, new Vector3(3, 3, 3));
                    eng.Scene.QueueToAdd.Add(
                     new WorldObject(new Model(floor),
                     false,
                     false
                     ));
                    eng.Scene.Update();
                }
                
                Logger.Debug("Behaviour", $"Raycasting! {intersection}");
            }, engine);

            engine.InputManager.BindAction(MouseAction.Button1, raycast);
            engine.InputManager.BindAction(MouseAction.Right, raycast);

            const float sensitivity = 0.2f;
            Behaviour<MouseEventState> lookingAround = new Behaviour<MouseEventState>((eng, mouseState) =>
            {
                var state = mouseState;
                eng.Window.Camera.Yaw += state.mouseDeltaX * sensitivity;
                eng.Window.Camera.Pitch -= state.mouseDeltaY * sensitivity;
            }, engine);

            engine.InputManager.BindAction(MouseAction.Move, lookingAround);
            engine.InputManager.EnableUIMouseControl(engine);

            const float cameraSpeed = 3.0f;
            Behaviour<KeyEventState> goingUp = new Behaviour<KeyEventState>((eng, keyEvent) =>
            {

                var state = keyEvent;
                if (state.isDown)
                {
                    var camera = eng.Window.Camera;
                    camera.Position += camera.Up * cameraSpeed * state.timeDelta;
                }
            }, engine);

            Behaviour<KeyEventState> goingDown = new Behaviour<KeyEventState>((eng, keyEvent) =>
            {
                var state = keyEvent;
                if (state.isDown)
                {
                    var camera = eng.Window.Camera;
                    camera.Position -= camera.Up * cameraSpeed * state.timeDelta;
                }
            }, engine);

            Behaviour<KeyEventState> goingForward = new Behaviour<KeyEventState>((eng, keyEvent) =>
            {
                var state = keyEvent;
                if (state.isDown)
                {
                    var camera = eng.Window.Camera;
                    camera.Position += camera.Front * cameraSpeed * state.timeDelta;
                }
            }, engine);

            Behaviour<KeyEventState> goingBackward = new Behaviour<KeyEventState>((eng, keyEvent) =>
            {
                var state = keyEvent;
                if (state.isDown)
                {
                    var camera = eng.Window.Camera;
                    camera.Position -= camera.Front * cameraSpeed * state.timeDelta;
                }
            }, engine);

            Behaviour<KeyEventState> goingLeft = new Behaviour<KeyEventState>((eng, keyEvent) =>
            {
                var state = keyEvent;
                if (state.isDown)
                {
                    var camera = eng.Window.Camera;
                    camera.Position -= camera.Right * cameraSpeed * state.timeDelta;
                }
            }, engine);

            Behaviour<KeyEventState> goingRight = new Behaviour<KeyEventState>((eng, keyEvent) =>
            {
                var state = keyEvent;
                if (state.isDown)
                {
                    var camera = eng.Window.Camera;
                    camera.Position += camera.Right * cameraSpeed * state.timeDelta;
                }
            }, engine);

            engine.InputManager.BindAction(KeyboardAction.W, goingForward);
            engine.InputManager.BindAction(KeyboardAction.A, goingLeft);
            engine.InputManager.BindAction(KeyboardAction.S, goingBackward);
            engine.InputManager.BindAction(KeyboardAction.D, goingRight);
            engine.InputManager.BindAction(KeyboardAction.LeftShift, goingDown);
            engine.InputManager.BindAction(KeyboardAction.Space, goingUp);

            bool canMove = true;

            void toggleMovement(bool on)
            {
                goingForward.SetActive(on);
                goingBackward.SetActive(on);
                goingDown.SetActive(on);
                goingUp.SetActive(on);
                goingRight.SetActive(on);
                goingLeft.SetActive(on);
                lookingAround.SetActive(on);
                raycast.SetActive(on);
            }

            Behaviour<KeyEventState> togleInv = new Behaviour<KeyEventState>((eng, keyEvent) =>
            {
               var state = (KeyEventState)keyEvent;

                if(state.justPressed)
                {
                    eng.InputManager.ResetMouseState();
                    eng.Window.AppSettings.CursorVisible = !inv.IsVisible;
                    inv.IsVisible = !inv.IsVisible;
                    eng.Window.ReloadSettings();
                    cross.IsVisible = !cross.IsVisible;
                    toggleMovement(!inv.IsVisible);
                    Logger.Debug("Behaviour", $"Togle inventory!");
                }
            }, engine);

            Behaviour<object> togleInvMouse = new Behaviour<object>((eng, _) =>
            {
                eng.InputManager.ResetMouseState();
                eng.Window.AppSettings.CursorVisible = !inv.IsVisible;
                inv.IsVisible = !inv.IsVisible;
                eng.Window.ReloadSettings();
                cross.IsVisible = !cross.IsVisible;
                toggleMovement(!inv.IsVisible);
                Logger.Debug("Behaviour", $"Togle inventory!");
            }, engine);

            engine.InputManager.BindAction(KeyboardAction.Q, togleInv);

            inv5.ClickAction = togleInvMouse;
            inv5.IsActive = true;

            item.AddChild(inv5);
            inv.AddChild(item);

            inv.IsVisible = false;
            engine.Window.AppSettings.CursorVisible = false;
            engine.Window.ReloadSettings();
            toggleMovement(true);

            scene.UIElements.Add(inv);
            scene.UIElements.Add(cross);

            return scene;
        }
    }
}
