using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using HiroEngine.HiroEngine.Data.Logger;
using OpenTK.Graphics.OpenGL4;

namespace HiroEngine.HiroEngine.Graphics.Elements
{
    public class Texture
    {
        public int Handle { get; private set; }
        readonly string LOGGER_ID = "Texture";
        public Texture(int glHandle)
        {
            Handle = glHandle;
        }

        public Texture(string path, TextureUnit unit = TextureUnit.Texture0)
        {
            Handle = GL.GenTexture();

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Handle);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            if (path == null) return;

            string combinedPath = Path.Combine(Environment.CurrentDirectory, @"Sample/assets", path);

            if( !File.Exists(combinedPath) )
            {
                Logger.Error(LOGGER_ID, $"File not exist: {combinedPath}");
            } else
            {
                using (var bmp = new Bitmap(combinedPath))
                {
                    bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    BitmapData bmpData = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.CompressedRgba, bmp.Width, bmp.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Rgba, PixelType.UnsignedByte, bmpData.Scan0);
                }

                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                Logger.Info(LOGGER_ID, $"File loaded: {combinedPath}");
            }
        }

        public void Use(TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        public void UseUnit(int unitId)
        {
            GL.ActiveTexture(GetTextureByUnit(unitId));
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        private TextureUnit GetTextureByUnit(int unit)
        {
            switch(unit)
            {
                case 0: return TextureUnit.Texture0;
                case 1: return TextureUnit.Texture1;
                case 2: return TextureUnit.Texture2;
                case 3: return TextureUnit.Texture3;
                case 4: return TextureUnit.Texture4;
                case 5: return TextureUnit.Texture5;
                case 6: return TextureUnit.Texture6;
                case 7: return TextureUnit.Texture7;
                case 8: return TextureUnit.Texture8;
                case 9: return TextureUnit.Texture9;
                default: return TextureUnit.Texture0;
            }
        }
    }
}
