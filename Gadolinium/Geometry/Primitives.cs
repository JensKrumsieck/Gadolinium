using System.Numerics;

namespace Gadolinium.Geometry;

public static class Primitives
{
    public static Mesh Cube()
        => MeshBuilder.Box(Vector3.Zero, Vector3.UnitX, Vector3.UnitY, 1f, 1f, 1f);
    public static Mesh Box(Vector3 center, Vector3 x, Vector3 y, float xLength, float yLength, float zLength)
        => MeshBuilder.Box(center, x, y, xLength, yLength, zLength);
    public static Mesh Box(Vector3 center, float xLength = 1f, float yLength = 1f, float zLength = 1f)
        => MeshBuilder.Box(center, Vector3.UnitX, Vector3.UnitY, xLength, yLength, zLength);
    public static Mesh Box(float xLength = 1f, float yLength = 1f, float zLength = 1f)
        => MeshBuilder.Box(Vector3.Zero, Vector3.UnitX, Vector3.UnitY, xLength, yLength, zLength);


    public static Mesh Cone(Vector3 origin, Vector3 direction, float baseRadius, float topRadius, float height,
        bool baseCap, bool topCap, int thetaDiv)
        => MeshBuilder.Cone(origin, direction, baseRadius, topRadius, height, baseCap, topCap, thetaDiv);
    public static Mesh Cone(float baseRadius, float topRadius, float height, bool baseCap, bool topCap, int thetaDiv)
        => MeshBuilder.Cone(Vector3.Zero, Vector3.UnitY, baseRadius, topRadius, height, baseCap, topCap, thetaDiv);
    public static Mesh Cone(Vector3 origin, Vector3 apex, float baseRadius, bool baseCap, int thetaDiv)
    {
        var dir = apex - origin;
        return MeshBuilder.Cone(origin, dir, baseRadius, 0, dir.Length(), baseCap, false, thetaDiv);
    }
    public static Mesh Cone(int thetaDiv = 32)
        => MeshBuilder.Cone(Vector3.Zero, Vector3.UnitY, 1f, 0f, 1f, true, true, thetaDiv);

    public static Mesh Cylinder(int thetaDiv = 32)
        => MeshBuilder.Cone(Vector3.Zero, Vector3.UnitY, 1f, 1f, 1f, true, true, thetaDiv);
    public static Mesh Cylinder(Vector3 p1, Vector3 p2, float diameter, int thetaDiv)
    {
        var n = p2 - p1;
        var l = n.Length();
        n = Vector3.Normalize(n);
        return MeshBuilder.Cone(p1, n, diameter / 2f, diameter / 2f, l, false, false, thetaDiv);
    }
    public static Mesh Cylinder(Vector3 p1, Vector3 p2, float radius = 1f, int thetaDiv = 32, bool cap1 = true,
        bool cap2 = true)
    {
        var n = p2 - p1;
        var l = n.Length();
        n = Vector3.Normalize(n);
        return MeshBuilder.Cone(p1, n, radius, radius, l, cap1, cap2, thetaDiv);
    }

    public static Mesh Ellipsoid(Vector3 center, float radiusX, float radiusY, float radiusZ, int thetaDiv, int phiDiv)
        => MeshBuilder.Ellipsoid(center, radiusX, radiusY, radiusZ, thetaDiv, phiDiv);
    public static Mesh Ellipsoid(float radiusX, float radiusY, float radiusZ, int thetaDiv = 20, int phiDiv = 10)
        => MeshBuilder.Ellipsoid(Vector3.Zero, radiusX, radiusY, radiusZ, thetaDiv, phiDiv);

    public static Mesh Sphere(Vector3 center, float radius = 1, int thetaDiv = 16, int phiDiv = 32)
        => MeshBuilder.Ellipsoid(center, radius, radius, radius, thetaDiv, phiDiv);
    public static Mesh Sphere(float radius = 1, int thetaDiv = 16, int phiDiv = 32)
        => MeshBuilder.Ellipsoid(Vector3.Zero, radius, radius, radius, thetaDiv, phiDiv);
}
