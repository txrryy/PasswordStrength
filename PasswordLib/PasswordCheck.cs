using System.Linq;

namespace PasswordLib;

public static class PasswordCheck
{
    private const string Ineligible = "INELIGABLE"; // ensure tests match this

    public static string Evaluate(string? password)
    {
        if (string.IsNullOrEmpty(password))
            return Ineligible;

        bool hasUpper   = password.Any(char.IsUpper);
        bool hasLower   = password.Any(char.IsLower);
        bool hasDigit   = password.Any(char.IsDigit);

        // Exclude whitespace from symbol criterion:
        bool hasSymbol  = password.Any(ch => char.IsPunctuation(ch) || char.IsSymbol(ch));

        // New length criterion
        bool hasMinLen8 = password.Length >= 8;

        int met = 0;
        if (hasUpper)   met++;
        if (hasLower)   met++;
        if (hasDigit)   met++;
        if (hasSymbol)  met++;
        if (hasMinLen8) met++;

        // Choose one mapping and make tests match. This is the 4-or-5=>STRONG version:
        return met switch
        {
            0       => Ineligible,
            1       => "WEAK",
            2 or 3  => "MEDIUM",
            4 or 5  => "STRONG",
            _       => Ineligible
        };
    }
}
