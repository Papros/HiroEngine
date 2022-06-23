using HiroEngine.HiroEngine.Data.Logger;
using HiroEngine.HiroEngine.Graphics.Elements;
using HiroEngine.HiroEngine.Graphics.Shaders;
using HiroEngine.HiroEngine.Inputs.Mouse;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace HiroEngine.HiroEngine.GUI.Elements
{
    public class UIElement : IClickable
    {
        public List<UIElement> ChildElements { get; private set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public Vector3 Position { get ; set; }
        public Vector2 Size { get; private set; }
        public Shape2D Element { get; private set; }
        private Vector2 Bounds;
        public UIPositionBehaviour PositionBehaviour { get; set; }
        private Vector3 Offset;

        public UIElement(Vector3 position, Vector2 size, UIPositionBehaviour behaviour, bool active = false, bool visible = true)
        {
            ChildElements = new List<UIElement>();
            Position = position;
            Size = size;
            IsActive = active;
            IsVisible = visible;
            Element = Shape2D.Rectangle(new Vector2(0,0), size);
            Bounds = new Vector2(2.0f, 2.0f);
            PositionBehaviour = behaviour;
            CalculateOffset();
            Logger.Info("", $"off: {Offset}");
            Logger.Info("", $"pos: {Position}");
            Logger.Info("", $"Bottomleft in: {Offset + Position}");
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
        }

        private void Resize()
        {
            Element.Resize(Bounds*Size*0.5f, new Vector2(0,0));
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
                var _offset = Offset+Position;
                shaderProgram.SetMatrix4(ShaderProgram.Uniforms.MATRIX.MODEL, Matrix4.CreateTranslation(_offset));
                Element.Draw(shaderProgram);
                _offset.Z -= 0.01f;
                _offset.Xy += Size * 0.5f;
                ChildElements.ForEach(element => element.Draw(shaderProgram, _offset, Size*Bounds * 0.5f));
            }
        }

        public void Draw(ShaderProgram shaderProgram, Vector3 parents_center, Vector2 bounds)
        {
            if (IsVisible)
            {
                var _position = bounds * Position.Xy * 0.5f;
                var _offset = parents_center + Offset + new Vector3(_position.X, _position.Y,0.0f);
                shaderProgram.SetMatrix4(ShaderProgram.Uniforms.MATRIX.MODEL, Matrix4.CreateTranslation(_offset));
                Element.Draw(shaderProgram);
                _offset.Z -= 0.01f;
                _offset.Xy += bounds * Size * 0.25f;
                ChildElements.ForEach(element => element.Draw(shaderProgram, _offset, Size*Bounds * 0.5f));
            }
        }

        public void OnClick()
        {
            if(IsActive)
            {
                Logger.Debug( "Button", "Clicked");
            }
        }

        public IDrawable GetDrawable()
        {
            return Element;
        }

        private void CalculateOffset()
        {

            Logger.Info("", $"Math: Size: {Size}, Bounds: {Bounds}");
            Logger.Info("", $"Math: TOPLEFT: {-Size.Y * Bounds.Y * 0.5f}");
            Logger.Info("", $"Math: CENTER: ({-Size.X * Bounds.X * 0.25f},{-Size.Y * Bounds.Y * 0.25f})");
            switch (PositionBehaviour)
            {
                case UIPositionBehaviour.BOTTOMLEFT: Offset = new Vector3(0,0,0); break;
                case UIPositionBehaviour.TOPLEFT: Offset = new Vector3(0,-Size.Y*Bounds.Y*0.5f, 0); break;
                case UIPositionBehaviour.CENTER: Offset = new Vector3(-Size.X * Bounds.X * 0.25f, -Size.Y * Bounds.Y * 0.25f, 0); break;
                default: Offset = new Vector3(0, 0, 0); break;
            }
        }
    }
}
