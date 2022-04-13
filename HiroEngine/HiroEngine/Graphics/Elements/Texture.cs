using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

            using (var bmp = new Bitmap($"C:/Users/piotr/Desktop/C#/HiroEngine/HiroEngine/Sample/assets/{path}"))
            {
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
                //Console.WriteLine($"File loaded: {bmp.Width},{bmp.Height} -> {bmp.Width * bmp.Height} pixels, stride/n/n: [{ 0 },{bmp.GetPixel(100, 100)},{0}]");
                BitmapData bmpData = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, bmp.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Rgba, PixelType.UnsignedByte, bmpData.Scan0);
            }

            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        public void Use(TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }
    }
}
