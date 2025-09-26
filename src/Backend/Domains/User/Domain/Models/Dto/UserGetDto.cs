namespace Backend.Domains.User.Domain.Models.Dto;

public class UserGetDto
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? Email { get; init; }
    public required string UserName { get; init; }
    public required bool IsActive { get; init; }
    public required bool IsInitialUser { get; init; }
}
