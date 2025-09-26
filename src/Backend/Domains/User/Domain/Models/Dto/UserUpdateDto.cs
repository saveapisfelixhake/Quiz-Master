namespace Backend.Domains.User.Domain.Models.Dto;

public class UserUpdateDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? Email { get; init; }
    public required string UserName { get; init; }
    public bool IsActive { get; init; } = true;
}
