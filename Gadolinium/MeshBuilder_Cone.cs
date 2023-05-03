using System.Numerics;

namespace Gadolinium;

public static partial class MeshBuilder
{
    /// <summary>
    /// Return a Cone Primitive
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    /// <param name="baseRadius"></param>
    /// <param name="topRadius"></param>
    /// <param name="height"></param>
    /// <param name="baseCap"></param>
    /// <param name="topCap"></param>
    /// <param name="thetaDiv"></param>
    /// <returns></returns>
    public static Mesh Cone(Vector3 origin, Vector3 direction, float baseRadius, float topRadius, float height,
                            bool baseCap, bool topCap, int thetaDiv)
    {
        var points = new List<Vector2>();
        var textureValues = new List<float>();
        if (baseCap)
        {
            points.Add(Vector2.Zero);
            textureValues.Add(0);
        }
        points.Add(Vector2.UnitY * baseRadius);
        textureValues.Add(1);
        points.Add(new Vector2(height, topRadius));
        textureValues.Add(0);
        if (topCap)
        {
            points.Add(Vector2.UnitX * height);
            textureValues.Add(1);
        }

        return Revolve(points, textureValues, origin, direction, thetaDiv);
    }

    /// <summary>
    /// Returns a Cone Primitive
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="apex"></param>
    /// <param name="baseRadius"></param>
    /// <param name="baseCap"></param>
    /// <param name="thetaDiv"></param>
    /// <returns></returns>
    public static Mesh Cone(Vector3 origin, Vector3 apex, float baseRadius, bool baseCap, int thetaDiv)
    {
        var dir = apex - origin;
        return Cone(origin, dir, baseRadius, 0, dir.Length(), baseCap, false, thetaDiv);
    }

    /// <summary>
    /// Returns a Cone Primitive
    /// </summary>
    /// <param name="baseRadius"></param>
    /// <param name="topRadius"></param>
    /// <param name="height"></param>
    /// <param name="baseCap"></param>
    /// <param name="topCap"></param>
    /// <param name="thetaDiv"></param>
    /// <returns></returns>
    public static Mesh Cone(float baseRadius, float topRadius, float height, bool baseCap, bool topCap, int thetaDiv) =>
        Cone(Vector3.Zero, Vector3.UnitY, baseRadius, topRadius, height, baseCap, topCap, thetaDiv);

    /// <summary>
    /// Returns a Cylinder Primitive with given Diameter
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="diameter"></param>
    /// <param name="thetaDiv"></param>
    /// <returns></returns>
    public static Mesh Cylinder(Vector3 p1, Vector3 p2, float diameter, int thetaDiv)
    {
        var n = p2 - p1;
        var l = n.Length();
        n = Vector3.Normalize(n);
        return Cone(p1, n, diameter / 2f, diameter / 2f, l, false, false, thetaDiv);
    }

    /// <summary>
    /// Returns a Cylinder Primitive with given Radius
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="radius"></param>
    /// <param name="thetaDiv"></param>
    /// <param name="cap1"></param>
    /// <param name="cap2"></param>
    /// <returns></returns>
    public static Mesh Cylinder(Vector3 p1, Vector3 p2, float radius = 1f, int thetaDiv = 32, bool cap1 = true,
                                bool cap2 = true)
    {
        var n = p2 - p1;
        var l = n.Length();
        n = Vector3.Normalize(n);
        return Cone(p1, n, radius, radius, l, cap1, cap2, thetaDiv);
    }
}