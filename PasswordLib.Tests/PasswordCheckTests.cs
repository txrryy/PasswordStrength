using Xunit;
using PasswordLib;

namespace PasswordLib.Tests;

public class PasswordCheckTests
{
    private const string Ineligible = "INELIGABLE";

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Ineligible_WhenNullOrEmpty(string? input)
    {
        Assert.Equal(Ineligible, PasswordCheck.Evaluate(input));
    }

    [Theory]
    [InlineData("AAAA")]   // upper only, len=4 -> 1 criterion
    [InlineData("aaaa")]   // lower only
    [InlineData("1234")]   // digits only
    [InlineData("@#$%")]   // symbols only
    public void Weak_WhenExactlyOneCriterion(string input)
    {
        Assert.Equal("WEAK", PasswordCheck.Evaluate(input));
    }

    [Theory]
    [InlineData("Aaaa")]    // upper+lower (2)
    [InlineData("aaa1")]    // lower+digit (2)
    [InlineData("A@aa")]    // upper+symbol (2)
    [InlineData("a@aa")]    // lower+symbol (2)
    [InlineData("1@11")]    // digit+symbol (2)
    [InlineData("Aa11")]    // upper+lower+digit (3)
    [InlineData("Aa@@")]    // upper+lower+symbol (3)
    [InlineData("a1@a")]    // lower+digit+symbol (3)
    [InlineData("A1@A")]    // upper+digit+symbol (3)
    public void Medium_WhenTwoOrThreeCriteria(string input)
    {
        Assert.Equal("MEDIUM", PasswordCheck.Evaluate(input));
    }

    [Theory]
    [InlineData("Aa1!aaaa")]   // 5/5: upper, lower, digit, symbol, len>=8
    [InlineData("Passw0rd!")]  // 5/5
    [InlineData("Zz9#xxxx")]   // 5/5
    [InlineData("Aa1!aa")]     // 4/5: upper, lower, digit, symbol, but len=6 (<8) -> still STRONG under 4-or-5 mapping
    public void Strong_WhenFourOrFiveCriteria(string input)
    {
        Assert.Equal("STRONG", PasswordCheck.Evaluate(input));
    }

    // Length boundary checks
    [Fact]
    public void Medium_WhenOnlyLengthAndOneOtherCriterion()
    {
        // lower + len=8 -> 2 criteria => MEDIUM
        Assert.Equal("MEDIUM", PasswordCheck.Evaluate("aaaaaaaa"));
    }

    [Fact]
    public void Weak_WhenOnlyOneCriterion_AndTooShort()
    {
        // lower only, len=4 -> 1 criterion => WEAK
        Assert.Equal("WEAK", PasswordCheck.Evaluate("aaaa"));
    }
}
