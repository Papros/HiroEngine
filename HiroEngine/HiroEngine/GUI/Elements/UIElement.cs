using HiroEngine.HiroEngine.Data.Logger;
using HiroEngine.HiroEngine.Graphics.Elements;
using HiroEngine.HiroEngine.Graphics.Shaders;
using HiroEngine.HiroEngine.Inputs.Mouse;
using HiroEngine.HiroEngine.Inputs.Shared.Core;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace HiroEngine.HiroEngine.GUI.Elements
{
    public class UIElement : IClickable
    {
        public List<UIElement> ChildElements { get; private set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public Vector3 Position { get; set; }
        public Vector2 Size { get; private set; }

        private Vector3 realPosition;
        private Vector2 realSize;

        public Shape2D Element { get; private set; }
        private Vector2 Bounds;
        public UIPositionBehaviour PositionBehaviour { get; set; }
        private Vector3 Offset;
        public Behaviour ClickAction { get; set;}

        public UIElement(Vector2 position, Vector2 size, UIPositionBehaviour behaviour, bool active = true, bool visible = true)
        {
            ChildElements = new List<UIElement>();
            Position = new Vector3(position.X, position.Y, 0);
            Size = size;
            realPosition = Position;
            realSize = size;
            IsActive = active;
            IsVisible = visible;
            Element = Shape2D.Rectangle(new Vector2(0,0), size);
            Bounds = new Vector2(2.0f, 2.0f);
            PositionBehaviour = behaviour;
            CalculateOffset();
        }

        public UIElement(UIElement other)
        {
            this.ChildElements = new List<UIElement>(other.ChildElements);
            IsActive = other.IsActive;
            IsVisible = other.IsVisible;
            Position = new Vector3(other.Position);
            Size = new Vector2(other.Size.X, other.Size.Y);
            Element = Shape2D.Rectangle(new Vector2(0, 0), Size);
            Element.AddTexture(other.Element.Textures);
            Bounds = new Vector2(other.Bounds.X, other.Bounds.Y);
            PositionBehaviour = other.PositionBehaviour;
            realPosition = new Vector3(other.realPosition);
            realSize = new Vector2( other.realSize.X, other.realSize.Y);
        }

        private void Resize()
        {
            Element.Resize(Bounds*Size*0.5f, new Vector2(0,0));
            realSize = Bounds * Size * 0.5f;
        }

        public void AddChild(UIElement child)
        {
            ChildElements.Add(child);
            child.Update(this);
        }

        private void Update(UIElement parent)
        {
            Bounds = parent.Size * parent.Bounds * 0.5f;
            Resize();
            CalculateOffset();
            var _position = Bounds * Position.Xy * 0.5f;
            realPosition = parent.realPosition + new Vector3(_position.X, _position.Y, 0);
            ChildElements.ForEach(child => child.Update(this));
        }

        public void RemoveChild(UIElement child)
        {
            ChildElements.Remove(child);
        }

        public void Draw(ShaderProgram shaderProgram)
        {
            if (IsVisible)
            {
                shaderProgram.SetMatrix4(ShaderProgram.Uniforms.MATRIX.MODEL, Matrix4.CreateTranslation(realPosition + Offset));
                Element.Draw(shaderProgram);
                ChildElements.ForEach(element => element.Draw(shaderProgram, 1));
            }
        }

        public void Draw(ShaderProgram shaderProgram, int layer)
        {
            if (IsVisible)
            {
                var _offset = realPosition + Offset;
                _offset.Z -= layer * 0.01f;
                shaderProgram.SetMatrix4(ShaderProgram.Uniforms.MATRIX.MODEL, Matrix4.CreateTranslation(_offset));
                Element.Draw(shaderProgram);
                ChildElements.ForEach(element => element.Draw(shaderProgram, layer+1));
            }
        }

        public void OnClick()
        {
            if (IsActive)
            {
                ClickAction?.Run();
            }
        }

        public IDrawable GetDrawable()
        {
            return Element;
        }

        private void CalculateOffset()
        {
            switch (PositionBehaviour)
            {
                case UIPositionBehaviour.BOTTOMLEFT: Offset = new Vector3(0,0,0); break;
                case UIPositionBehaviour.TOPLEFT: Offset = new Vector3(0,-Size.Y*Bounds.Y*0.5f, 0); break;
                case UIPositionBehaviour.CENTER: Offset = new Vector3(-Size.X * Bounds.X * 0.25f, -Size.Y * Bounds.Y * 0.25f, 0); break;
                default: Offset = new Vector3(0, 0, 0); break;
            }
        }

        public bool Clicked(Vector2 mouse)
        {
            var leftbottom = realPosition + Offset;
            Logger.Debug("UIElement", $"Testing click for {mouse}, bounds from: {leftbottom} -> {leftbottom.Xy+realSize}");
            if (!IsVisible || !IsActive) return false;

            if( mouse.X > leftbottom.X && mouse.X < leftbottom.X+realSize.X && mouse.Y > leftbottom.Y && mouse.Y < leftbottom.Y + realSize.Y)
            {
                Logger.Debug("", "In bounds!");
                bool childClicked = ChildElements.Exists(child => child.Clicked(mouse));
                if(childClicked)
                {
                    return true;
                } else
                {
                    OnClick();
                    return true;
                }

            } else
            {
                //external children
                return ChildElements.Exists(child => child.Clicked(mouse));
            }
        }
    }
}
