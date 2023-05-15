using System.Collections.Generic;
using Gadolinium.Geometry;
using Gadolinium.Monogame.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector3 = System.Numerics.Vector3;

namespace Gadolinium.Test.Monogame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private BasicEffect _basicEffect;

    private readonly List<VisualMesh> _meshes = new();
    private readonly Vector3 _cameraPosition = new Vector3(0, -1, -5);

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
        _meshes.Add(new VisualMesh(Primitives.Sphere(Vector3.UnitZ), GraphicsDevice));
        _meshes.Add(new VisualMesh(Primitives.Box(new Vector3(2f, -1, 1)), GraphicsDevice));
        _meshes.Add(new VisualMesh(Primitives.Cone(new Vector3(3, 1, 0), new Vector3(3, 4, 0), 1f, true, 32),
            GraphicsDevice));
        _meshes.Add(new VisualMesh(Primitives.Cylinder(new Vector3(3, -3, 0), new Vector3(2, -4, 1)), GraphicsDevice));
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        base.Draw(gameTime);
        foreach (var mesh in _meshes)
        {
            //GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            GraphicsDevice.SetVertexBuffer(mesh.VertexBuffer);
            GraphicsDevice.Indices = mesh.IndexBuffer;
            _basicEffect = new BasicEffect(GraphicsDevice);
            _basicEffect.EnableDefaultLighting();
            _basicEffect.DiffuseColor = Color.Coral.ToVector3();
            _basicEffect.View =
                Matrix.CreateLookAt(_cameraPosition, Vector3.Zero, Microsoft.Xna.Framework.Vector3.Up);
            _basicEffect.Projection =
                Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(90),
                    GraphicsDevice.Viewport.AspectRatio, 0.1f, 1000f);

            foreach (var pass in _basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, mesh.Mesh.Indices.Length / 3);
            }
        }
    }
}
