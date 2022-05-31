using OpenTK.Mathematics;

namespace HiroEngine.HiroEngine.Graphics.Elements
{
    public class Shape : Mesh
    {
        Color4 color;

        private Shape(float[] vertices, int[] indices, Texture[] textures) : base(vertices, indices, textures)
        {
                
        }

        public static Shape Cube(Vector3 position, Vector3 diagonal)
        {
            return new Shape(null, null, null);
        }

        public static Shape Plane(Vector3 position, Vector2 diagonal)
        {
           
            float[] vertices =
            {
                // Position         color             texture coordinates
                 position.X+diagonal.X, position.Y, position.Z+diagonal.Y,  0.0f, 0.0f, 1.0f, 1.0f, 1.0f, // top right
                 position.X+diagonal.X, position.Y, position.Z,  0.0f, 0.0f, 1.0f, 1.0f, 0.0f, // bottom right
                 position.X, position.Y, position.Z, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, // bottom left
                 position.X, position.Y, position.Z+diagonal.Y,  0.0f, 0.0f, 1.0f, 0.0f, 1.0f  // top left
            };

            int[] indices = {  // note that we start from 0!
                    0, 1, 3,   // first triangle
                    1, 2, 3    // second triangle
                };

            return new Shape(vertices, indices, new Texture[] { new Texture("container.png") });
        }

        public void SetColour(Color4 color)
        {

        }

    }
}
