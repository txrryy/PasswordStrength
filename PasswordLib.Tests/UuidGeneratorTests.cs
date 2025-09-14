using System;
using Xunit;
using PasswordLib;

namespace PasswordLib.Tests;

public class UuidGeneratorTests
{
    [Fact]
    public void GenerateV4_ReturnsParseableUuid()
    {
        var s = UuidGenerator.GenerateV4();
        Assert.True(Guid.TryParse(s, out _));
    }

    [Fact]
    public void GenerateV4_HasVersion4Bits()
    {
        var s = UuidGenerator.GenerateV4();
        var parsed = Guid.Parse(s);
        var bytes = parsed.ToByteArray();

        var version = (bytes[7] >> 4) & 0x0F;
        Assert.Equal(4, version);
    }

    [Fact]
    public void GenerateV4_HasRfc4122Variant()
    {
        var s = UuidGenerator.GenerateV4();
        var parsed = Guid.Parse(s);
        var bytes = parsed.ToByteArray();

        var variant = (bytes[8] >> 6) & 0x03;
        Assert.Equal(2, variant);
    }

    [Fact]
    public void GenerateV4_TwoCalls_YieldDifferentValues()
    {
        var a = UuidGenerator.GenerateV4();
        var b = UuidGenerator.GenerateV4();
        Assert.NotEqual(a, b);
    }
}
