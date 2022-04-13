using System;
using System.Collections.Generic;
using System.Text;

namespace HiroEngine.HiroEngine.Graphics.Core
{
    public struct Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        /// <summary>
        /// Return Vector2 item with default value [0,0]
        /// </summary>
        /// <param name="x">Value of X</param>
        /// <param name="y">Value of Y</param>
        public Vector2(float x = 0, float y = 0)
        {
            X = x;
            Y = y;
        }

        public Vector2 add(Vector2 other)
        {
            X += other.X;
            Y += other.Y;
            return this;
        }

        public Vector2 substract(Vector2 other)
        {
            X -= other.X;
            Y -= other.Y;
            return this;
        }

        public Vector2 multiply(Vector2 other)
        {
            X *= other.X;
            Y *= other.Y;
            return this;
        }

        public Vector2 divide(Vector2 other)
        {
            if (other.X == 0 || other.Y == 0) return this;
            X /= other.X;
            Y /= other.Y;
            return this;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) => a.add(b);
        public static Vector2 operator -(Vector2 a, Vector2 b) => a.substract(b);
        public static Vector2 operator *(Vector2 a, Vector2 b) => a.multiply(b);
        public static Vector2 operator /(Vector2 a, Vector2 b) => a.divide(b);
    }
}
