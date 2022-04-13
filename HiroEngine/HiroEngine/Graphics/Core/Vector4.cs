using System;
using System.Collections.Generic;
using System.Text;

namespace HiroEngine.HiroEngine.Graphics.Core
{
    public struct Vector4
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        /// <summary>
        /// Return Vector4 item with default value [0,0]
        /// </summary>
        /// <param name="x">Value of X</param>
        /// <param name="y">Value of Y</param>
        /// <param name="z">Value of Z</param>
        /// <param name="w">Value of W</param>
        public Vector4(float x = 0, float y = 0, float z = 0, float w = 0)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vector4 add(Vector4 other)
        {
            X += other.X;
            Y += other.Y;
            Z += other.Z;
            W += other.W;
            return this;
        }

        public Vector4 substract(Vector4 other)
        {
            X -= other.X;
            Y -= other.Y;
            Z -= other.Z;
            W -= other.W;
            return this;
        }

        public Vector4 multiply(Vector4 other)
        {
            X *= other.X;
            Y *= other.Y;
            Z *= other.Z;
            W *= other.W;
            return this;
        }

        public Vector4 divide(Vector4 other)
        {
            if (other.X == 0 || other.Y == 0 || other.Z == 0 || other.W == 0) return this;
            X /= other.X;
            Y /= other.Y;
            Z /= other.Z;
            W /= other.W;
            return this;
        }

        public static Vector4 operator +(Vector4 a, Vector4 b) => a.add(b);
        public static Vector4 operator -(Vector4 a, Vector4 b) => a.substract(b);
        public static Vector4 operator *(Vector4 a, Vector4 b) => a.multiply(b);
        public static Vector4 operator /(Vector4 a, Vector4 b) => a.divide(b);
    }
}
