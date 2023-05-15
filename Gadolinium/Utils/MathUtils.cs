using System.Numerics;

namespace Gadolinium.Utils;

public static class MathUtils
{
    public const float TwoPi = MathF.PI * 2f;
    public const float PiOver2 = MathF.PI / 2f;

    public static Vector3 FindAnyPerpendicular(this Vector3 v)
    {
        v = Vector3.Normalize(v);
        var u = Vector3.Cross(Vector3.UnitY, v);
        if (u.LengthSquared() < 1e-3) u = Vector3.Cross(Vector3.UnitX, v);
        return u;
    }
    public static T Clamp<T>(T value, T min, T max) where T : INumber<T>
    {
        value = (value > max) ? max : value;
        value = (value < min) ? min : value;
        return value;
    }

    public static T Lerp<T>(T value1, T value2, T amount) where T : INumber<T> => value1 + (value2 - value1) * amount;
}
