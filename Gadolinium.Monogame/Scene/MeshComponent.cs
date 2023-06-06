using Gadolinium.Monogame.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gadolinium.Monogame.Scene;

public struct MeshComponent
{
    public VisualMesh Mesh;
    public Material<BasicEffect> Material;
    
    public MeshComponent(VisualMesh mesh)
    {
        Mesh = mesh;
        Material = new Material<BasicEffect>(new BasicEffect(mesh.Device));
        Material.Effect.EnableDefaultLighting();
        Material.Effect.DiffuseColor = Color.HotPink.ToVector3();
    }
}
