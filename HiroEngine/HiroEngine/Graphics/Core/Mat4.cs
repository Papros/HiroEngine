using System;
using System.Collections.Generic;
using System.Text;

namespace HiroEngine.HiroEngine.Graphics.Core
{
    public struct Mat4
    {
        float[] values;

        public Mat4(float value = 0f)
        {
            values = new float[16] { value, value, value, value, value, value, value, value, value, value, value, value, value, value, value, value };
        }

        public Mat4(float[] value)
        {
            values = value;
        }

        public static Mat4 Diagonal(float value = 0)
        {
            return new Mat4(new float[16] { value, 0f, 0f, 0f, 0f, value, 0f, 0f, 0f, 0f, value, 0f, 0f, 0f, 0f, value});
        }

        public static Mat4 Identity()
        {
            return new Mat4(new float[16] { 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f } );
        }

        public Mat4 Multiply(ref Mat4 other)
        {
            this.values = new float[16] {
                values[0]* other[0] + values[1]* other[4] + values[2]* other[8] + values[3]* other[12], values[0]* other[1] + values[1]* other[5] + values[2]* other[9] + values[3]* other[13], values[0]* other[2] + values[1]* other[6] + values[2]* other[10] + values[3]* other[14], values[0]* other[3] + values[1]* other[7] + values[2]* other[11] + values[3]* other[15],
                values[4]* other[0] + values[5]* other[4] + values[6]* other[8] + values[7]* other[12], values[4]* other[1] + values[5]* other[5] + values[6]* other[9] + values[7]* other[13], values[4]* other[2] + values[5]* other[6] + values[6]* other[10] + values[7]* other[14], values[4]* other[3] + values[5]* other[7] + values[6]* other[11] + values[7]* other[15],
                values[8]* other[0] + values[9]* other[4] + values[10]* other[8] + values[11]* other[12], values[8]* other[1] + values[9]* other[5] + values[10]* other[9] + values[11]* other[13], values[8]* other[2] + values[9]* other[6] + values[10]* other[10] + values[11]* other[14], values[8]* other[3] + values[9]* other[7] + values[10]* other[11] + values[11]* other[15],
                values[12]* other[0] + values[13]* other[4] + values[14]* other[8] + values[15]* other[12], values[12]* other[1] + values[13]* other[5] + values[14]* other[9] + values[15]* other[13], values[12]* other[2] + values[13]* other[6] + values[14]* other[10] + values[15]* other[14], values[12]* other[3] + values[13]* other[7] + values[14]* other[11] + values[15]* other[15],
            };
            return this;
        }

        public Vector4 Multiply(ref Vector4 other)
        {
            return new Vector4(
                values[0] * other.X + values[1] * other.Y + values[2] * other.Z + values[3] * other.W,
                values[4] * other.X + values[5] * other.Y + values[6] * other.Z + values[7] * other.W,
                values[8] * other.X + values[9] * other.Y + values[10] * other.Z + values[11] * other.W,
                values[12] * other.X + values[13] * other.Y + values[14] * other.Z + values[15] * other.W
                );
        }

        public Vector3 Multiply(ref Vector3 other)
        {
            return new Vector3(
                values[0] * other.X + values[1] * other.Y + values[2] * other.Z + values[3],
                values[4] * other.X + values[5] * other.Y + values[6] * other.Z + values[7],
                values[8] * other.X + values[9] * other.Y + values[10] * other.Z + values[11]
                );
        }

        public static Mat4 Rotation(ref float angle, ref Vector3 axis)
        {
            float r = (angle * 3.141592f / 180f);
            float c = (float)Math.Cos(r);
            float s = (float)Math.Sin(r);
            float omc = 1f - c;

            return new Mat4(new float[16] { axis.X * omc + c, axis.Y*axis.X*omc+axis.Z*s, axis.X*axis.Z*omc-axis.Y*s, 0f,
                axis.X * axis.Y * omc - axis.Z * s, axis.Y * omc + c, axis.Y * axis.Z * omc + axis.X * s, 0f,
                axis.X * axis.Z * omc + axis.Y * s, axis.Y * axis.Z * omc - axis.X * s, axis.Z * omc + c, 0f, 
                0f, 0f, 0f, 1f });
        }

        public static Mat4 Scale(ref Vector3 scale)
        {
            return new Mat4(new float[16] { scale.X, 0f, 0f, 0f, 0f, scale.Y, 0f, 0f, 0f, 0f, scale.Z, 0f, 0f, 0f, 0f, 1f });
        }

        public static Mat4 Translation(ref Vector3 translation)
        {
            return new Mat4(new float[16] { 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, translation.X, translation.Y, translation.Z, 1f });
        }

        public static Mat4 Orthographic(float left, float right, float bottom, float top, float near, float far)
        {
            return new Mat4(new float[16] { 2.0f / (right - left), 0f, 0f, 0f, 0f, 2.0f / (top - bottom), 0f, 0f, 0f, 0f, 2.0f / (near - far), 0f, (left + right) / (left - right), (bottom + top) / (bottom - top), (far + near) / (far - near), 1f });
        }

        public static Mat4 Perspective(float fov, float aspectRatio, float near, float far)
        {
            float q = 1f / (float)Math.Tan(0.5f * fov * 3.141592f / 180);
            return new Mat4(new float[16] { q / aspectRatio, 0f, 0f, 0f, 0f, q, 0f, 0f, 0f, 0f, (near + far) / (near - far), -1f, 0f, 0f, (2.0f * near * far) / (near - far), 1f });
        }


        public float this[int index]
        {
            get => values[index];
            set => values[index] = value;
        }

        public string stringify()
        {
            return String.Join(", ", values);
        }
    }
}
