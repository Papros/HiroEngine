using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace HiroEngine.HiroEngine.Graphics.Elements
{
    public class Texture
    {
        int Handle;

        public Texture(int glHandle)
        {
            Handle = glHandle;
        }

        public Texture(string path, TextureUnit unit = TextureUnit.Texture0)
        {
            Handle = GL.GenTexture();

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Handle);

            string combinedPath = Path.Combine(Environment.CurrentDirectory, @"Sample/assets", path);

            using (var bmp = new Bitmap(combinedPath))
            {
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
                BitmapData bmpData = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, bmp.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Rgba, PixelType.UnsignedByte, bmpData.Scan0);
            }

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        public void Use(TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }
    }
}
