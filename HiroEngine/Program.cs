using HiroEngine.HiroEngine.Engine.Elements;
using HiroEngine.HiroEngine.Graphics.Elements;
using HiroEngine.HiroEngine.Graphics.Window;
using HiroEngine.HiroEngine.GUI.Elements;
using HiroEngine.HiroEngine.Inputs.handlers;
using HiroEngine.HiroEngine.Inputs.Mouse;
using HiroEngine.HiroEngine.Physics.Structures.Colliders;
using OpenTK.Mathematics;

namespace HiroEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            GameEngine game = new GameEngine();
            game.Render(SetupLevel());
            game.Render(SetupUI());
            game.SetupHandlers();
            game.Run(1280, 720);   
        }

        private static Scene SetupLevel()
        {
            Texture towerText = new Texture("wooden_tower/textures/Wood_Tower_Col.jpg");
            Model tower = new Model("wooden_tower/tower2.dae");
            tower.Components[0].AddTexture(new Texture[] { towerText });

            WorldObject towerO1 = new WorldObject(tower, true).AddPosition(0, 0, 0).AddRotation(-1.5708f, 0, 0).AddTransform(0.7f);
            WorldObject towerO2 = new WorldObject(tower, true).AddPosition(-10, 0, 0).AddRotation(-1.5708f, -0.9f, 0).AddTransform(1.2f);
            WorldObject towerO3 = new WorldObject(tower, true).AddPosition(10, 0, 0).AddRotation(-1.5708f, 0.5f, 0).AddTransform(1.9f);

            Shape floor = Shape.Plane(new Vector3(-15, 0, -15), new Vector2(30, 30));
            floor.AddTexture(new Texture[] { new Texture("container.png") });

            Scene scene = new Scene();
            scene.worldObjects.Add(towerO1);
            scene.worldObjects.Add(towerO2);
            scene.worldObjects.Add(towerO3);
            scene.worldObjects.Add(
                    new WorldObject(new Model(floor),
                    false,
                    false, 
                    new PlaneCollider(new Vector3(-15,0,-15), new Vector3(-15,0,15), new Vector3(15,0,15), new Vector3(15,0,-15))
                    ));

            return scene;
        }

        private static GUIScene SetupUI()
        {
            GUIScene scene = new GUIScene();

            UIElement bar = new UIElement(new Vector2(-0.1f, -0.1f), new Vector2(0.1f, 0.1f));
            bar.Element = Shape2D.Rectangle(new Vector2(0, 0), new Vector2(0.1f, 0.1f));
            bar.Element.AddTexture(new Texture[] { new Texture("UI/crosshair.png") });

            scene.UIElements.Add(bar);
            return scene;
        }
    }
}
