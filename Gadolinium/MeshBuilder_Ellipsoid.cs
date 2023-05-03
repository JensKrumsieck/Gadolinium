using System.Numerics;

namespace Gadolinium;

public static partial class MeshBuilder
{
    /// <summary>
    /// Returns Ellipsoid Primitive
    /// </summary>
    /// <param name="center"></param>
    /// <param name="radiusX"></param>
    /// <param name="radiusY"></param>
    /// <param name="radiusZ"></param>
    /// <param name="thetaDiv"></param>
    /// <param name="phiDiv"></param>
    /// <returns></returns>
    public static Mesh Ellipsoid(Vector3 center, float radiusX, float radiusY, float radiusZ, int thetaDiv = 20, int phiDiv = 10)
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

    /// <summary>
    /// returns Ellipsoid Primitive
    /// </summary>
    /// <param name="radiusX"></param>
    /// <param name="radiusY"></param>
    /// <param name="radiusZ"></param>
    /// <param name="thetaDiv"></param>
    /// <param name="phiDiv"></param>
    /// <returns></returns>
    public static Mesh Ellipsoid(float radiusX, float radiusY, float radiusZ, int thetaDiv = 20, int phiDiv = 10) =>
        Ellipsoid(Vector3.Zero, radiusX, radiusY, radiusZ, thetaDiv, phiDiv);

    /// <summary>
    /// Returns Sphere Primitive
    /// </summary>
    /// <param name="center"></param>
    /// <param name="radius"></param>
    /// <param name="thetaDiv"></param>
    /// <param name="phiDiv"></param>
    /// <returns></returns>
    public static Mesh Sphere(Vector3 center, float radius = 1, int thetaDiv = 16, int phiDiv = 32) =>
        Ellipsoid(center, radius, radius, radius, thetaDiv, phiDiv);

    /// <summary>
    /// Returns a Sphere Primitive
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="thetaDiv"></param>
    /// <param name="phiDiv"></param>
    /// <returns></returns>
    public static Mesh Sphere(float radius = 1, int thetaDiv = 16, int phiDiv = 32) =>
        Sphere(Vector3.Zero, radius, thetaDiv, phiDiv);
}