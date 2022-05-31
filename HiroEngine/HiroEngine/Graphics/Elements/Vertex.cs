namespace HiroEngine.HiroEngine.Graphics.Elements
{
    public struct Vertex
    {
        // 0,1,2 position
        // 3,4,5 Normal
        // 6,7 Texture
        public float[] vertex; 

        public Vertex(float[] vert)
        {
            vertex = vert;
        }
    }
}
