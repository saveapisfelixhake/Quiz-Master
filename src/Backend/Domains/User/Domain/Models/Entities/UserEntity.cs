namespace Backend.Domains.User.Domain.Models.Entities;

public class UserEntity
{
    #region Entity

    public void Update(string firstName, string lastName, string? email, string userName, bool isActive)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName, nameof(firstName));
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName, nameof(lastName));
        ArgumentException.ThrowIfNullOrWhiteSpace(userName, nameof(userName));

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = userName;
        IsActive = isActive;
    }

    public void UpdateHash(string passwordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash, nameof(passwordHash));

        PasswordHash = passwordHash;
    }

    #endregion Entity

    private UserEntity(Guid id, string firstName, string lastName, string? email, string userName, string passwordHash, bool isActive, bool isInitialUser)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = userName;
        PasswordHash = passwordHash;
        IsActive = isActive;
        IsInitialUser = isInitialUser;
    }

    public Guid Id { get; }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? Email { get; private set; }

    public string UserName { get; private set; }
    public string PasswordHash  { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsInitialUser { get; private set; }

    public static UserEntity Create(string firstName, string lastName, string? email, string userName, string passwordHash, bool isActive, bool isInitialUser = false)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName, nameof(firstName));
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName, nameof(lastName));
        ArgumentException.ThrowIfNullOrWhiteSpace(userName, nameof(userName));
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash, nameof(passwordHash));

        return new UserEntity(Guid.NewGuid(), firstName, lastName, email, userName, passwordHash, isActive, isInitialUser);
    }
}