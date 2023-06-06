using Gadolinium.Geometry;
using Gadolinium.Monogame.Geometry;
using Gadolinium.Monogame.Scene;
using Gadolinium.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector3 = System.Numerics.Vector3;

namespace Gadolinium.Test.Monogame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private readonly World _world = new();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
    }

    
    protected override void Initialize()
    {
        base.Initialize();
        var camera = _world.CreateEntity();
        _world.AddComponent(camera, new TransformComponent {Position = new Vector3(0, -1, -5)});
        _world.AddComponent(camera, new CameraComponent
        {
            AspectRatio = _graphics.GraphicsDevice.Viewport.AspectRatio
        });
        Window.ClientSizeChanged += (_, _) => _world.GetComponent<CameraComponent>(camera).AspectRatio = _graphics.GraphicsDevice.Viewport.AspectRatio;
        _world.AddComponent(
                            _world.CreateEntity(),
                            new MeshComponent(new VisualMesh(Primitives.Sphere(Vector3.UnitZ), GraphicsDevice)
                                             )
                            {
                                Material =
                                {
                                    Effect =
                                    {
                                        DiffuseColor = Color.Firebrick.ToVector3()
                                    }
                                }
                            });
        _world.AddComponent(
                           _world.CreateEntity(),
                           new MeshComponent(new VisualMesh(Primitives.Box(new Vector3(2f, -1, 1)), GraphicsDevice)
                                            ));
        _world.AddComponent(
                           _world.CreateEntity(),
                           new MeshComponent(new
                                                 VisualMesh(Primitives.Cone(new Vector3(3, 1, 0), new Vector3(3, 4, 0), 1f, true, 32),
                                                            GraphicsDevice)
                                            ));
        _world.AddComponent(
                           _world.CreateEntity(),
                           new MeshComponent(new
                                                 VisualMesh(Primitives.Cylinder(new Vector3(3, -3, 0), new Vector3(2, -4, 1)),
                                                            GraphicsDevice)));
        _world.AddSystem(new MeshRendererSystem(camera));
        _world.InitializeSystems();
    }
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _world.ExecuteSystems((float)gameTime.ElapsedGameTime.TotalSeconds);        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        base.Draw(gameTime);
        _world.DrawSystems();
    }
}
