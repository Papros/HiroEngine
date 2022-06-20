using HiroEngine.HiroEngine.Graphics.Elements;
using HiroEngine.HiroEngine.Graphics.Shaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiroEngine.HiroEngine.Engine.Elements
{
    public class Scene : IDrawable
    {
        public List<WorldObject> worldObjects { get; private set; }
        public List<WorldObject> QueueToAdd { get; private set; }
        public int Size { get { return worldObjects.Count; } }
        public Scene()
        {
            worldObjects = new List<WorldObject>();
            QueueToAdd = new List<WorldObject>();
        }

        public void Draw(ShaderProgram shader)
        {
            worldObjects.ForEach(model => {
                    model.Draw(shader);
                });
        }

        public void DrawCollider(ShaderProgram shader)
        {
            worldObjects.ForEach(model =>
            {
                model.DrawCollider(shader);
            });
        }

        public void Update()
        {
            worldObjects.AddRange(QueueToAdd);
            QueueToAdd.Clear();
        }
    }
}
