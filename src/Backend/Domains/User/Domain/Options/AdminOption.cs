namespace Backend.Domains.User.Domain.Options;

public class AdminOption
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? Email { get; init; }
    public required string UserName { get; init; }
    public required string Password { get; init; }
}
