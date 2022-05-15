using HiroEngine.HiroEngine.Inputs.Enums;
using HiroEngine.HiroEngine.Inputs.interfaces;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiroEngine.HiroEngine.Inputs
{
    public class InputManager
    {
        public IMouseHandler MouseHandler { get; set; }
        public IKeyboardHandler KeyboardHandler { get; set; }
        public IWindowHandler WindowHandler { get; set; }
        public List<Keys> SubscribeKeys { get; private set; }

        public InputManager()
        {
            SubscribeKeys = new List<Keys>();
        }

        public void OnMouseWheel(MouseWheelEventArgs e)
        {
            if(MouseHandler != null)
            {
                MouseHandler.OnMouseWheel(e.OffsetY);
            }
        }

        public void OnMouseDown(MouseButtonEventArgs e)
        {
            if(MouseHandler != null)
            {
                MouseHandler.OnMouseDown((MouseKeys)e.Button, (InputType)e.Action, (CorespondingKeyEvent)e.Modifiers, e.IsPressed);
            }
        }

        public void OnMouseMove(MouseMoveEventArgs e)
        {
            if (MouseHandler != null)
            {
                MouseHandler.OnMouseMove(e.X, e.Y);
            }
        }

        public void SubscribeKey(KeyboardKeys key)
        {
            if(!SubscribeKeys.Contains((Keys)key))
            {
                SubscribeKeys.Add((Keys)key);
            }
        }

        public void SubscribeKey(KeyboardKeys[] keys)
        {
            foreach(KeyboardKeys key in keys)
            {
                SubscribeKey(key);
            }
        }

        public void ClearSubscribtion()
        {
            SubscribeKeys = new List<Keys>();
        }

        public void UnsubscribeKey(KeyboardKeys key)
        {
            SubscribeKeys.Remove((Keys)key);
        }

        public void UnsubscribeKey(KeyboardKeys[] keys)
        {
            foreach (KeyboardKeys key in keys)
            {
                UnsubscribeKey(key);
            }
        }

        public void GetSubscribtionFromHandler()
        {
            if(KeyboardHandler != null)
            {
                SubscribeKey(KeyboardHandler.GetSubscribtionList());
            }
        }

        public void OnKeyboardAction(Keys key, bool isPressed, bool wasPressed, float time)
        {
            if(KeyboardHandler != null)
            {
                KeyboardHandler.OnKeyAction((KeyboardKeys)key, isPressed, wasPressed, time);
            }
        }
    }
}
