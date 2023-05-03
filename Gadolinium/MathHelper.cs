using System.Numerics;

namespace Gadolinium;

public static class MathHelper
{
    public const float TwoPi = MathF.PI * 2f;

    public static Vector3 FindAnyPerpendicular(this Vector3 v)
    {
        v = Vector3.Normalize(v);
        var u = Vector3.Cross(Vector3.UnitY, v);
        if (u.LengthSquared() < 1e-3) u = Vector3.Cross(Vector3.UnitX, v);
        return u;
    }
}