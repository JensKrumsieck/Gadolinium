using System.Numerics;
using FluentAssertions;

namespace Gadolinium.Test;

public class MeshBuilderTests
{
    [Fact]
    public void Cone_Can_Be_Created_WithNoCaps()
    {
        const int thetaDiv = 30;
        const int points = 2;
        const int vertexCount = thetaDiv * 2 * (points - 1);
        
        var m = MeshBuilder.Cone(Vector3.UnitX, Vector3.UnitZ, 1f, 0f, 1f, false, false, thetaDiv);
        m.Indices.Length.Should().Be(vertexCount * 3);
        m.Vertices.Length.Should().Be(vertexCount);
    }
    
    [Fact]
    public void Cone_Can_Be_Created_WithCaps()
    {
        const int thetaDiv = 30;
        const int points = 4;
        const int vertexCount = thetaDiv * 2 * (points - 1);
        
        var m = MeshBuilder.Cone(Vector3.UnitX, Vector3.UnitZ, 1f, 0f, 1f, true, true, thetaDiv);
        m.Indices.Length.Should().Be(vertexCount * 3);
        m.Vertices.Length.Should().Be(vertexCount);
    }

    [Fact]
    public void Cylinder_Can_Be_Created()
    {
        const int thetaDiv = 30;
        const int points = 2;
        const int vertexCount = thetaDiv * 2 * (points - 1);

        var m = MeshBuilder.Cylinder(Vector3.Zero, Vector3.UnitY, 1f, thetaDiv);
        m.Indices.Length.Should().Be(vertexCount * 3);
        m.Vertices.Length.Should().Be(vertexCount);
    }

    [Fact]
    public void Ellipsoid_Can_Be_Created()
    {
        const int thetaDiv = 32;
        const int phiDiv = 16;
        const int rows = phiDiv + 1;
        const int cols = thetaDiv + 1;
        const int vertexCount = rows * cols;
        const int indexCount = (rows - 2) * (cols - 1) * 6;
        var m = MeshBuilder.Ellipsoid(1f, 2f, 3f, thetaDiv, phiDiv);
        m.Vertices.Length.Should().Be(vertexCount);
        m.Indices.Length.Should().Be(indexCount);
    }
    
    [Fact]
    public void Sphere_Can_Be_Created()
    {
        const int thetaDiv = 32;
        const int phiDiv = 16;
        const int rows = phiDiv + 1;
        const int cols = thetaDiv + 1;
        const int vertexCount = rows * cols;
        const int indexCount = (rows - 2) * (cols - 1) * 6;
        var m = MeshBuilder.Sphere(1f, thetaDiv, phiDiv);
        m.Vertices.Length.Should().Be(vertexCount);
        m.Indices.Length.Should().Be(indexCount);
    }

    [Fact]
    public void Box_Can_Be_Created()
    {
        var m = MeshBuilder.Box(Vector3.Zero);
        m.Vertices.Length.Should().Be(6 * 4);
        m.Indices.Length.Should().Be(6 * 6);
    }
}