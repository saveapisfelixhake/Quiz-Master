using BCrypt.Net;

namespace Backend.Domains.Common.Infrastructure.Services;

public class HashingService : IHashingService
{
    public string Hash(string plain)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt();

        return BCrypt.Net.BCrypt.HashPassword(plain, salt, true, HashType.SHA512);
    }

    public bool Verify(string plain, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(plain, hash, true, HashType.SHA512);
    }
}