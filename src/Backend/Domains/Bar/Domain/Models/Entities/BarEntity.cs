namespace Backend.Domains.Bar.Domain.Models.Entities;

public class BarEntity
{
    #region Entity

    public bool HasChanges(string name, string street, string number)
    {
        return !Name.Equals(name)
               || Address.HasChanges(street, number);
    }

    public void Update(string name, bool isActive, string street, string number)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
        IsActive = isActive;
        Address.Update(street, number);
    }

    #endregion Entity

    private readonly List<Quiz.Entity.Quiz> _quizzes = [];
    private readonly BarAddressEntity? _address = null;

    private BarEntity(Guid id, string name, bool isActive, Guid addressId)
    {
        Id = id;
        Name = name;
        IsActive = isActive;
        AddressId = addressId;
    }

    public Guid Id { get; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; }

    public Guid AddressId { get; }
    public BarAddressEntity Address => _address ?? throw new InvalidOperationException("Bar has no assigned address");
    public IReadOnlyList<Quiz.Entity.Quiz> Quizzes => _quizzes.AsReadOnly();
    

    public static BarEntity Create(string name, bool isActive, Guid addressId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        return new BarEntity(Guid.NewGuid(), name, isActive, addressId);
    }
}