using System.Numerics;

namespace Gadolinium;

public static partial class MeshBuilder
{
    /// <summary>
    /// returns Box Primitive
    /// </summary>
    /// <param name="center"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="xLength"></param>
    /// <param name="yLength"></param>
    /// <param name="zLength"></param>
    /// <returns></returns>
    public static Mesh Box(Vector3 center, Vector3 x, Vector3 y, float xLength, float yLength, float zLength)
    {
        var z = Vector3.Cross(x, y);
        var m = AddCubeFace(center, x, z, xLength, yLength, zLength);
        m.Add(AddCubeFace(center, -x, z, xLength, yLength, zLength));
        m.Add(AddCubeFace(center, -y, z, xLength, yLength, zLength));
        m.Add(AddCubeFace(center, y, z, xLength, yLength, zLength));
        m.Add(AddCubeFace(center, z, y, xLength, yLength, zLength));
        m.Add(AddCubeFace(center, -z, y, xLength, yLength, zLength));
        return m;
    }

    /// <summary>
    /// Returns Box Primitve
    /// </summary>
    /// <param name="center"></param>
    /// <param name="xLength"></param>
    /// <param name="yLength"></param>
    /// <param name="zLength"></param>
    /// <returns></returns>
    public static Mesh Box(Vector3 center, float xLength = 1f, float yLength = 1f, float zLength= 1f) =>
        Box(center, Vector3.UnitX, Vector3.UnitY, xLength, yLength, zLength);
    
    public static Mesh AddCubeFace(Vector3 center, Vector3 normal, Vector3 up, float dist, float width, float height)
    {
        var m = new Mesh();
        var right = Vector3.Cross(normal, up);
        var n = normal * dist / 2f;
        up *= width / 2f;
        right *= width / 2f;
        var p1 = center + n - up - right;
        var p2 = center + n - up + right;
        var p3 = center + n + up + right;
        var p4 = center + n + up - right;
        m.AddVertex(p1, normal, Vector2.One);
        m.AddVertex(p2, normal, Vector2.UnitY);
        m.AddVertex(p3, normal, Vector2.Zero);
        m.AddVertex(p4, normal, Vector2.UnitX);
        
        m.AddIndex(2);
        m.AddIndex(1);
        m.AddIndex(0);
        
        m.AddIndex(0);
        m.AddIndex(3);
        m.AddIndex(2);
        return m;
    }
}