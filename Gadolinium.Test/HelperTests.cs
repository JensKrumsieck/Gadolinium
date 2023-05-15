using System.Numerics;
using FluentAssertions;
using Gadolinium.Utils;

namespace Gadolinium.Test;

public class HelperTests
{
    [Fact]
    public void Clamp_Works_Generic()
    {
        MathUtils.Clamp(20, 0, 10).Should().Be(10);
        MathUtils.Clamp(-1f, 0f, 1f).Should().Be(0f);
        MathUtils.Clamp(1d, 0d, 10d).Should().Be(1d);
    }

    [Fact]
    public void Lerp_Works_Generic()
    {
        MathUtils.Lerp(0, 1, .5).Should().Be(.5);
        MathUtils.Lerp(0f, 2f, .25f).Should().Be(.5f);
        MathUtils.Lerp(0, 10, 1).Should().Be(10); //you probably don't need to lerp ints^^
    }

    [Fact]
    public void ColorHelper_Works()
    {
        ColorUtils.GetRgbColorFromHex("#f0f1f2").Should().Be(new Vector3(240 / 255f, 241 / 255f, 242 / 255f));
        ColorUtils.GetArgbColorFromHex("#00f0f1f2").Should().Be(new Vector4(0, 240 / 255f, 241 / 255f, 242 / 255f));
        ColorUtils.GetArgbColorFromHex("#0ff0f1f2").Should()
            .Be(new Vector4(15 / 255f, 240 / 255f, 241 / 255f, 242 / 255f));
    }
}
