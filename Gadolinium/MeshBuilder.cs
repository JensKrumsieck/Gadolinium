using System.Numerics;

namespace Gadolinium;

/// <summary>
/// Used to Build Meshes
/// heavily inspired by Helix Toolkit https://github.com/helix-toolkit/helix-toolkit/tree/develop/Source/HelixToolkit.Shared/Geometry/MeshBuilder.cs
/// which is licensed under MIT License
/// </summary>
public static partial class MeshBuilder
{
   public static IList<Vector2> GetCircle(int thetaDiv, bool closed = false)
    {
        var circle = new List<Vector2>();
        var num = closed ? thetaDiv : thetaDiv - 1;
        for (var i = 0; i < thetaDiv; i++)
        {
            var theta = MathHelper.TwoPi * (i / (float) num);
            circle.Add(new Vector2(MathF.Cos(theta), -MathF.Sin(theta)));
        }

        return circle;
    }

    public static Mesh Revolve(IList<Vector2> points, IList<float> textureValues, Vector3 origin, Vector3 direction,
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
                var q2 = origin + (direction * points[j + 1].X) + (w * points[j].Y);
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