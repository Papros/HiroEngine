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
            float[] vertices =
            {
                // Position         color             texture coordinates
                 position.X+diagonal.X, position.Y, position.Z+diagonal.Z,  0.0f, 0.0f, 1.0f, 1.0f, 1.0f, // top right
                 position.X+diagonal.X, position.Y, position.Z,  0.0f, 0.0f, 1.0f, 1.0f, 0.0f, // bottom right
                 position.X, position.Y, position.Z, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, // bottom left
                 position.X, position.Y, position.Z+diagonal.Y,  0.0f, 0.0f, 1.0f, 0.0f, 1.0f,  // top left
                 position.X+diagonal.X, position.Y+diagonal.Y, position.Z+diagonal.Z,  0.0f, 0.0f, 1.0f, 1.0f, 1.0f, // top top right
                 position.X+diagonal.X, position.Y+diagonal.Y, position.Z,  0.0f, 0.0f, 1.0f, 1.0f, 0.0f, // top bottom right
                 position.X, position.Y+diagonal.Y, position.Z, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, // top bottom left
                 position.X, position.Y+diagonal.Y, position.Z+diagonal.Z,  0.0f, 0.0f, 1.0f, 0.0f, 1.0f  // top top left
            };
            int[] indices = {  // note that we start from 0!
                    0, 1, 3,   // first triangle bottom
                    1, 2, 3,    // second triangle
                    0, 1, 4,   // side 1
                    4, 1, 5,
                    1,2,5, // side 2
                    5,6,2,
                    2,6,3, // side 3
                    6,7,3,
                    3,4,0, // side 4
                    3,7,4,
                    4, 5, 7,   // first triangle top
                    5, 6, 7,    // second triangle
                };
            return new Shape(vertices, indices, new Texture[] { new Texture("container.png") });
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
