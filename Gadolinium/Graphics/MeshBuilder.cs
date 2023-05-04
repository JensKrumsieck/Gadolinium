using System.Numerics;

namespace Gadolinium.Graphics;

/// <summary>
/// Used to Build Meshes
/// heavily inspired by Helix Toolkit https://github.com/helix-toolkit/helix-toolkit/tree/develop/Source/HelixToolkit.Shared/Geometry/MeshBuilder.cs
/// which is licensed under MIT License
/// </summary>
internal static class MeshBuilder
{
    internal static Mesh Box(Vector3 center, Vector3 x, Vector3 y, float xLength, float yLength, float zLength)
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
    
    internal static Mesh Ellipsoid(Vector3 center, float radiusX, float radiusY, float radiusZ, int thetaDiv, int phiDiv)
    {
        var m = new Mesh();
        var dt = MathHelper.TwoPi / thetaDiv;
        var dp = MathF.PI / phiDiv;
        for (var pi = 0; pi <= phiDiv; pi++)
        {
            var phi = pi * dp;
            for (var ti = 0; ti <= thetaDiv; ti++)
            {
                var theta = ti * dt;
                var x = MathF.Cos(theta) * MathF.Sin(phi);
                var y = MathF.Sin(theta) * MathF.Sin(phi);
                var z = MathF.Cos(phi);
                var p = new Vector3(center.X + radiusX * x, center.Y + radiusY * y, center.Z + radiusZ * z);
                var n = new Vector3(x, y, z);
                var uv = new Vector2(theta / (MathHelper.TwoPi), phi / MathF.PI);
                m.AddVertex(p, n, uv);
            }
        }

        var rows = phiDiv + 1;
        var cols = thetaDiv + 1;
        for (var i = 0; i < rows - 1; i++)
        {
            for (var j = 0; j < cols - 1; j++)
            {
                var ij = i * cols + j;
                if (i > 0)
                {
                    m.AddIndex(ij + 1);
                    m.AddIndex(ij + 1 + cols);
                    m.AddIndex(ij);
                }

                if (i < rows - 2)
                {
                    m.AddIndex(ij+cols);
                    m.AddIndex(ij);
                    m.AddIndex(ij + cols + 1);
                }
            }
        }

        return m;
    }
    
    internal static Mesh Cone(Vector3 origin, Vector3 direction, float baseRadius, float topRadius, float height,
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

    private static Mesh AddCubeFace(Vector3 center, Vector3 normal, Vector3 up, float dist, float width, float height)
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
    
    private static IList<Vector2> GetCircle(int numberOfSegments, bool isClosed = false)
    {
        var circlePoints = new List<Vector2>();
        var numberOfPoints = isClosed ? numberOfSegments : numberOfSegments - 1;
        for (var i = 0; i < numberOfSegments; i++)
        {
            var theta = MathHelper.TwoPi * (i / (float) numberOfPoints);
            circlePoints.Add(new Vector2(MathF.Cos(theta), -MathF.Sin(theta)));
        }

        return circlePoints;
    }
    private static Mesh Revolve(IList<Vector2> points, IList<float> textureValues, Vector3 origin, Vector3 direction,
                                int thetaDiv)
    {
        var m = new Mesh();
        direction = Vector3.Normalize(direction);
        var u = direction.FindAnyPerpendicular();
        var v = Vector3.Cross(direction, u);
        u = Vector3.Normalize(u);
        v = Vector3.Normalize(v);

        var circle = GetCircle(thetaDiv);
        
        var n = points.Count;
        var totalNodes = (points.Count - 1) * 2 * thetaDiv;
        var rowNodes = (points.Count - 1) * 2;
        
        for (var i = 0; i < thetaDiv; i++)
        {
            var w = (v * circle[i].X) + (u * circle[i].Y);
            for (var j = 0; j + 1 < n; j++)
            {
                var q1 = origin + (direction * points[j].X) + (w * points[j].Y);
                var q2 = origin + (direction * points[j + 1].X) + (w * points[j + 1].Y);
                var tx = points[j + 1].X - points[j].X;
                var ty = points[j + 1].Y - points[j].Y;
                var normal = (-direction * ty) + (w * tx);
                var tv1 = new Vector2(i / (thetaDiv - 1f), textureValues == null ? j / (n - 1f) : textureValues[j]);
                var tv2 = new Vector2(i / (thetaDiv - 1f),
                                      textureValues == null ? (j + 1f) / (n - 1f) : textureValues[j + 1]);
                m.AddVertex(q1, normal, tv1);
                m.AddVertex(q2, normal, tv2);
                var i0 = (i * rowNodes) + (j * 2);
                var i1 = i0 + 1;
                var i2 = ((((i + 1) * rowNodes) + (j * 2)) % totalNodes);
                var i3 = i2 + 1;
                
                m.AddIndex(i2);
                m.AddIndex(i0);
                m.AddIndex(i1);

                m.AddIndex(i3);
                m.AddIndex(i2);
                m.AddIndex(i1);
            }
        }

        return m;
    }
}