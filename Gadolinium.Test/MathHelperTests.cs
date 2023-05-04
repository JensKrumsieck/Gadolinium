using FluentAssertions;

namespace Gadolinium.Test;

public class MathHelperTests
{
    [Fact]
    public void Clamp_Works_Generic()
    {
        MathHelper.Clamp(20, 0, 10).Should().Be(10);
        MathHelper.Clamp(-1f, 0f, 1f).Should().Be(0f);
        MathHelper.Clamp(1d, 0d, 10d).Should().Be(1d);
    }

    [Fact]
    public void Lerp_Works_Generic()
    {
        MathHelper.Lerp(0, 1, .5).Should().Be(.5);
        MathHelper.Lerp(0f, 2f, .25f).Should().Be(.5f);
        MathHelper.Lerp(0, 10, 1).Should().Be(10); //you probably don't need to lerp ints^^
    }
}