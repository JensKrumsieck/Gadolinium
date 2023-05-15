using System.Numerics;
using System.Runtime.InteropServices;

namespace Gadolinium.Geometry;

public class Mesh
{
    private readonly List<Vertex> _vertices = new();
    private readonly List<ushort> _indices = new();

    public void AddVertex(Vector3 position, Vector3 normal, Vector2 textureCoordinate) =>
        _vertices.Add(new Vertex(position, normal, textureCoordinate));

    public void AddVertex(Vertex vertex) => _vertices.Add(vertex);

    public void AddIndex(int index)
    {
        if (index > ushort.MaxValue) throw new ArgumentOutOfRangeException(nameof(index));
        _indices.Add((ushort) index);
    }

    public ReadOnlySpan<Vertex> Vertices => CollectionsMarshal.AsSpan(_vertices);
    public ReadOnlySpan<ushort> Indices => CollectionsMarshal.AsSpan(_indices);

    public void Add(Mesh other)
    {
        var count = _vertices.Count;
        for (var i = 0; i < other.Vertices.Length; i++) AddVertex(other.Vertices[i]);
        for (var i = 0; i < other.Indices.Length; i++) AddIndex(other.Indices[i] + count);
    }
}
