using System.Numerics;
using FluentAssertions;
using Gadolinium.Geometry;

namespace Gadolinium.Test;

public class MeshTests
{
    [Fact]
    public void Meshes_Can_Be_Constructed()
    {
        var m = new Mesh();
        m.Should().BeOfType<Mesh>();
    }

    [Fact]
    public void Meshes_Can_Be_Added()
    {
        var m1 = Primitives.Sphere(Vector3.UnitZ * 2, 1f, 32, 64);
        var m2 = Primitives.Sphere(-Vector3.UnitY * 2, 1f, 64, 128);
        var m3 = Primitives.Sphere(-Vector3.UnitX * 2, 1f, 128, 256);
        var m4 = new Mesh();
        m4.Add(m1);
        m4.Add(m2);
        m4.Add(m3);
        m4.Indices.Length.Should().Be(m1.Indices.Length + m2.Indices.Length + m3.Indices.Length);
        m4.Vertices.Length.Should().Be(m1.Vertices.Length + m2.Vertices.Length + m3.Vertices.Length);
    }
}
