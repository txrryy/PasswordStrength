// PasswordLib.Tests/PasswordCheckTests.cs
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
    [InlineData("AAAA")]   // only uppercase
    [InlineData("aaaa")]   // only lowercase
    [InlineData("1234")]   // only digits
    [InlineData("@#$%")]   // only symbols
    public void Weak_WhenExactlyOneCriterion(string input)
    {
        Assert.Equal("WEAK", PasswordCheck.Evaluate(input));
    }

    [Theory]
    [InlineData("AAA1")]   // upper + digit
    [InlineData("aaa1")]   // lower + digit
    [InlineData("Aaaa")]   // upper + lower
    [InlineData("A@aa")]   // upper + symbol
    [InlineData("a@aa")]   // lower + symbol
    [InlineData("1@11")]   // digit + symbol
    [InlineData("Aa11")]   // upper + lower + digit
    [InlineData("Aa@@")]   // upper + lower + symbol
    [InlineData("a1@a")]   // lower + digit + symbol
    [InlineData("A1@A")]   // upper + digit + symbol
    public void Medium_WhenTwoOrThreeCriteria(string input)
    {
        Assert.Equal("MEDIUM", PasswordCheck.Evaluate(input));
    }

    [Theory]
    [InlineData("Aa1!")]
    [InlineData("Passw0rd!")]
    [InlineData("Zz9#")]
    public void Strong_WhenAllFourCriteria(string input)
    {
        Assert.Equal("STRONG", PasswordCheck.Evaluate(input));
    }

    [Fact]
    public void Weak_WhenWhitespaceOnly_DoesNotCountAsSymbol()
    {
        // spaces only → no upper/lower/digit/symbol (since whitespace isn't symbol)
        Assert.Equal(Ineligible, PasswordCheck.Evaluate("     "));
    }
}
