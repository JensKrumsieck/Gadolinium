using Gadolinium.Geometry;
using Microsoft.Xna.Framework.Graphics;

namespace Gadolinium.Monogame.Geometry;

public class VisualMesh
{
    public readonly Mesh Mesh;
    
    public readonly GraphicsDevice Device;
    public VertexBuffer VertexBuffer { get; }
    public IndexBuffer IndexBuffer { get; }

    public VisualMesh(Mesh mesh, GraphicsDevice device)
    {
        Device = device;
        Mesh = mesh;
        VertexBuffer =
            new VertexBuffer(Device, VertexPositionNormalTexture.VertexDeclaration,
                mesh.Vertices.Length, BufferUsage.WriteOnly);


        var verts = new List<VertexPositionNormalTexture>();
        foreach (var vert in mesh.Vertices)
            verts.Add(new VertexPositionNormalTexture(vert.Position, vert.Normal, vert.TextureCoordinate));
        VertexBuffer.SetData(verts.ToArray());
        IndexBuffer =
            new IndexBuffer(Device, typeof(ushort), mesh.Indices.Length, BufferUsage.WriteOnly);
        IndexBuffer.SetData(mesh.Indices.ToArray());
    }
}
