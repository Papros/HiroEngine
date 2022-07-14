using HiroEngine.HiroEngine.Data.Logger;
using HiroEngine.HiroEngine.Engine.Elements;
using HiroEngine.HiroEngine.Inputs.Enums;
using HiroEngine.HiroEngine.Inputs.handlers;
using HiroEngine.HiroEngine.Inputs.Keyboard.Struct;
using HiroEngine.HiroEngine.Inputs.Mouse;
using HiroEngine.HiroEngine.Inputs.Mouse.Struct;
using HiroEngine.HiroEngine.Inputs.Shared.Core;
using HiroEngine.HiroEngine.Inputs.Window.Enums;
using HiroEngine.HiroEngine.Inputs.Window.Handlers;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Generic;

namespace HiroEngine.HiroEngine.Inputs
{
    public class InputManager
    {
        private MouseHandler MouseHandler { get; set; }
        private KeyboardHandler KeyboardHandler { get; set; }
        private WindowHandler WindowHandler { get; set; }
        public float MouseX => MouseHandler?.MouseX ?? 0;
        public float MouseY => MouseHandler?.MouseY ?? 0;
        public List<KeyboardAction> SubscribedKeys => KeyboardHandler?.BindedKeys;

        private Behaviour<MouseEventState> MouseUIControl;

        public InputManager()
        {
        }

        public void OnMouseWheel(float Offset)
        {
            if (MouseHandler != null)
            {
                MouseHandler.OnMouseWheel(Offset);
            }
        }

        public void ResetMouseState()
        {
            if(MouseHandler != null)
            {
                MouseHandler.ResetMouseState();
            }
        }

        public void OnMouseDown(MouseAction action, InputType inpType, CorespondingKeyEvent keyMod, bool press)
        {
            if (MouseHandler != null)
            {
                Logger.Warn("InputManager", $"Mouse clicked: {action}");
                MouseHandler.OnMouseDown(action, inpType, keyMod, press);

                if(action == MouseAction.Left)
                {
                    MouseUIControl?.Run(new MouseEventState()
                    {
                        mouseX = MouseHandler.MouseX,
                        mouseY = MouseHandler.MouseY,
                        mouseDeltaX = 0,
                        mouseDeltaY = 0,
                        inputType = inpType,
                        corespondingKeyEvent = keyMod,
                        mouseWheelOffset = 0,
                    });
                }
                    
            }
        }

        public void OnMouseMove(float x, float y)
        {
            if (MouseHandler != null)
                MouseHandler.OnMouseMove(x, y);
        }

        public void OnKeyboardAction(KeyboardAction key, bool isDown, bool wasDown, float timeDelta)
        {
            if (KeyboardHandler != null)
            {
                KeyboardHandler.OnKeyAction(key, isDown, wasDown, timeDelta);
            }
        }

        public void BindAction(MouseAction action, Behaviour<MouseEventState> mouseBehaviour)
        {
            if(MouseHandler != null && mouseBehaviour != null)
            {
                MouseHandler.MouseBindings?.Add(action, mouseBehaviour);
            } else if(mouseBehaviour == null)
            {
                MouseHandler?.MouseBindings?.Remove(action);
            }
        }

        public void SetActive(MouseAction action, bool isActive)
        {
            if(MouseHandler != null)
            {
                MouseHandler.MouseBindings[action]?.SetActive(isActive);
            }
        }

        public void BindAction(KeyboardAction action, Behaviour<KeyEventState> keyBehaviour)
        {
            Logger.Info("", $"Binding actions to key: {action}");
            if (KeyboardHandler != null && keyBehaviour != null)
            {
                KeyboardHandler.KeyBindings.Add(action, keyBehaviour);
                if(!KeyboardHandler.BindedKeys.Contains(action)) KeyboardHandler.BindedKeys.Add(action);
            }
            else if (KeyboardHandler != null && keyBehaviour == null)
            {
                KeyboardHandler.KeyBindings.Remove(action);
                if (KeyboardHandler.BindedKeys.Contains(action)) KeyboardHandler.BindedKeys.Remove(action);
            }
        }

        public void SetActive(KeyboardAction action, bool isActive)
        {
            if (KeyboardHandler != null)
            {
                KeyboardHandler.KeyBindings[action]?.SetActive(isActive);
            }
        }

        public void BindAction(WindowAction action, Behaviour<WindowAction> windowBehaviour)
        {
            if (WindowHandler != null && windowBehaviour != null)
            {
                WindowHandler.WindowBindings?.Add(action, windowBehaviour);
            }
            else if (windowBehaviour == null)
            {
                WindowHandler?.WindowBindings?.Remove(action);
            }
        }

        public void SetActive(WindowAction action, bool isActive)
        {
            if (WindowHandler != null)
            {
                WindowHandler.WindowBindings[action]?.SetActive(isActive);
            }
        }

        public void PushHandler(MouseHandler mouseHandler)
        {
            this.MouseHandler = mouseHandler;
        }

        public void PushHandler(KeyboardHandler keyboardHandler)
        {
            this.KeyboardHandler = keyboardHandler;
        }

        public void PushHandler(WindowHandler windowHandler)
        {
            this.WindowHandler = windowHandler;
        }

        public void EnableUIMouseControl(GameEngine engine)
        {
            MouseUIControl = new Behaviour<MouseEventState>((eng, mouseState) =>
            {
                var state = (MouseEventState)mouseState;
                var halfX = eng.Window.Size.X * 0.5f;
                var halfY = eng.Window.Size.Y * 0.5f;
                var mousePosition = new Vector2((state.mouseX - halfX) / halfX, -(state.mouseY - halfY) / halfY);
                Logger.Warn("Ui click", $"Clicked in: {mousePosition}");
                eng.GUIScene.UIElements.ForEach(uiElement =>
                {
                    uiElement.Clicked(mousePosition);
                });

            }, engine);
        }
    }
}
