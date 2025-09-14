  using System;

namespace PasswordLib;

public static class UuidGenerator
{
    /// Generates a random version 4 UUID 
    public static string GenerateV4()
    {
        return Guid.NewGuid().ToString();
    }
}