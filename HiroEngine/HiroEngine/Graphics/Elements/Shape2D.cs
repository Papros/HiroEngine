using OpenTK.Mathematics;

namespace HiroEngine.HiroEngine.Graphics.Elements
{
    public class Shape2D : Mesh
    {
        Color4 color;

        private Shape2D(float[] vertices, int[] indices, Texture[] textures, Vector3 min, Vector3 max) : base(vertices, indices, textures, min, max)
        {

        }

        public static Shape2D Rectangle(Vector2 position, Vector2 diagonal, bool defaultText = false)
        {
            var depth = 0.1f;
            float[] vertices =
            {
                // Position         color             texture coordinates
                 position.X+diagonal.X, position.Y+diagonal.Y, depth,  0.0f, 0.0f, 1.0f, 1.0f, 1.0f, // top right
                 position.X+diagonal.X, position.Y, depth,  0.0f, 0.0f, 1.0f, 1.0f, 0.0f, // bottom right
                 position.X, position.Y, depth, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, // bottom left
                 position.X, position.Y+diagonal.Y, depth,  0.0f, 0.0f, 1.0f, 0.0f, 1.0f  // top left
            };

            int[] indices = {  // note that we start from 0!
                    0, 1, 3,   // first triangle
                    1, 2, 3    // second triangle
                };

            return new Shape2D(vertices, indices, defaultText ? new Texture[] { new Texture("container.png") } : null, new Vector3(), new Vector3());
        }

        public void Resize(Vector2 diagonal, Vector2 position)
        {
            var depth = 0.1f;
            float[] vertices =
            {
                // Position         color             texture coordinates
                 position.X+diagonal.X, position.Y+diagonal.Y, depth,  0.0f, 0.0f, 1.0f, 1.0f, 1.0f, // top right
                 position.X+diagonal.X, position.Y, depth,  0.0f, 0.0f, 1.0f, 1.0f, 0.0f, // bottom right
                 position.X, position.Y, depth, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, // bottom left
                 position.X, position.Y+diagonal.Y, depth,  0.0f, 0.0f, 1.0f, 0.0f, 1.0f  // top left
            };

            this.Vertices = vertices;
            this.Setup();
        }
    }
}
