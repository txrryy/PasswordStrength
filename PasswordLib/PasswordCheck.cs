using System.Linq;

namespace PasswordLib;

public static class PasswordCheck
{
    private const string Ineligible = "INELIGABLE";

 
    public static string Evaluate(string? password)
    {
        if (string.IsNullOrEmpty(password))
            return Ineligible;

        bool hasUpper  = password.Any(char.IsUpper);
        bool hasLower  = password.Any(char.IsLower);
        bool hasDigit  = password.Any(char.IsDigit);
        bool hasMinLen8 = password.Length >= 8;

        bool hasSymbol = password.Any(ch => char.IsPunctuation(ch) || char.IsSymbol(ch));

        int met = 0;
        if (hasUpper)  met++;
        if (hasLower)  met++;
        if (hasDigit)  met++;
        if (hasSymbol) met++;
        if (hasMinLen8) met++;  // NEW criterion


        return met switch
        {
            0 => Ineligible,
            1 => "WEAK",
            2 or 3 => "MEDIUM",
            4 => "STRONG",
            _ => Ineligible
        };
    }
}
