using System;
using System.Collections.Generic;
using System.Text;

namespace HiroEngine.HiroEngine.Graphics.Core
{
    public struct Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        /// <summary>
        /// Return Vector3 item with default value [0,0]
        /// </summary>
        /// <param name="x">Value of X</param>
        /// <param name="y">Value of Y</param>
        /// <param name="z">Value of Z</param>
        public Vector3(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 add(Vector3 other)
        {
            X += other.X;
            Y += other.Y;
            Z += other.Z;
            return this;
        }

        public Vector3 substract(Vector3 other)
        {
            X -= other.X;
            Y -= other.Y;
            Z -= other.Z;
            return this;
        }

        public Vector3 multiply(Vector3 other)
        {
            X *= other.X;
            Y *= other.Y;
            Z *= other.Z;
            return this;
        }

        public Vector3 divide(Vector3 other)
        {
            if (other.X == 0 || other.Y == 0 || other.Z == 0) return this;
            X /= other.X;
            Y /= other.Y;
            Z /= other.Z;
            return this;
        }

        public static Vector3 operator +(Vector3 a, Vector3 b) => a.add(b);
        public static Vector3 operator -(Vector3 a, Vector3 b) => a.substract(b);
        public static Vector3 operator *(Vector3 a, Vector3 b) => a.multiply(b);
        public static Vector3 operator /(Vector3 a, Vector3 b) => a.divide(b);
    }
}
