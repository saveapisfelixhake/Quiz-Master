namespace Backend.Domains.Common.Infrastructure.Services;

public interface IHashingService
{
    string Hash(string plain);
    bool Verify(string plain, string hash);
}