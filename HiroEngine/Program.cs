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

            UIElement inv = new UIElement(new Vector3(-0.6f, -0.8f, 0), new Vector2(1f, 1f), UIPositionBehaviour.TOPLEFT);
            Texture textInv = new Texture("UI/inv.png");
            inv.Element.AddTexture(textInv);

            UIElement item = new UIElement(new Vector3(1,1, 0), new Vector2(1, 1), UIPositionBehaviour.CENTER);
            Texture textItem = new Texture("UI/diamond.jpg");
            item.Element.AddTexture(textItem);
            
            //scene.UIElements.Add(bar);

            //UIElement inv2 = new UIElement(new Vector3(0, 1, 0), new Vector2(0.6f, 0.8f), UIPositionBehaviour.TOPLEFT);
            //inv2.Element.AddTexture(textInv);

            //UIElement inv3 = new UIElement(new Vector3(0, 0, 0), new Vector2(0.4f, 0.4f), UIPositionBehaviour.TOPLEFT);
            //inv3.Element.AddTexture(textInv);

            //UIElement inv4 = new UIElement(new Vector3(-1.0f, -1.0f, 0), new Vector2(0.7f, 0.7f), UIPositionBehaviour.BOTTOMLEFT);
            //inv4.Element.AddTexture(textInv);

            UIElement inv5 = new UIElement(new Vector3(-1.0f, 1, 0), new Vector2(1, 1), UIPositionBehaviour.CENTER);
            inv5.Element.AddTexture(textInv);

            item.AddChild(inv5);
            inv.AddChild(item);
            /*
            inv2.AddChild(new UIElement(item));
            inv3.AddChild(new UIElement(item));
            inv4.AddChild(new UIElement(item));
            */
            
            scene.UIElements.Add(inv);

            /*
            scene.UIElements.Add(inv2);
            scene.UIElements.Add(inv3);
            scene.UIElements.Add(inv4);
            */
            //scene.UIElements.Add(item);
            return scene;
        }
    }
}
