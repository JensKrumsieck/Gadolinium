using System.Globalization;
using System.Numerics;

namespace Gadolinium.Utils;

public static class ColorUtils
{
    public static Vector3 GetRgbColorFromHex(ReadOnlySpan<char> hexColor)
    {
        if (hexColor[0] != '#' || hexColor.Length != 7) return Vector3.Zero;
        var r = int.Parse(hexColor.Slice(1, 2), NumberStyles.HexNumber);
        var g = int.Parse(hexColor.Slice(3, 2), NumberStyles.HexNumber);
        var b = int.Parse(hexColor.Slice(5, 2), NumberStyles.HexNumber);
        return new Vector3(r / 255f, g / 255f, b / 255f);
    }

    public static Vector4 GetArgbColorFromHex(ReadOnlySpan<char> hexColor)
    {
        if (hexColor[0] != '#' || hexColor.Length != 9) return Vector4.Zero;
        var a = int.Parse(hexColor.Slice(1, 2), NumberStyles.HexNumber);
        var r = int.Parse(hexColor.Slice(3, 2), NumberStyles.HexNumber);
        var g = int.Parse(hexColor.Slice(5, 2), NumberStyles.HexNumber);
        var b = int.Parse(hexColor.Slice(7, 2), NumberStyles.HexNumber);
        return new Vector4(a / 255f, r / 255f, g / 255f, b / 255f);
    }
}
