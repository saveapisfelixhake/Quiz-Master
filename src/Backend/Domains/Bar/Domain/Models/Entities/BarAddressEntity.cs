namespace Backend.Domains.Bar.Domain.Models.Entities;

public class BarAddressEntity
{
    #region Entity

    public bool HasChanges(string street, string number)
    {
        return !Street.Equals(street)
               || !Number.Equals(number);
    }

    public void Update(string street, string number)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(street);
        ArgumentException.ThrowIfNullOrWhiteSpace(number);

        Street = street;
        Number = number;
    }

    #endregion Entity
    private BarAddressEntity(Guid id, string street, string number)
    {
        Id = id;
        Street = street;
        Number = number;
    }

    public Guid Id { get; }
    public string Street { get; private set; }
    public string Number { get; private set; }

    public static BarAddressEntity Create(string street, string number)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(street, nameof(street));
        ArgumentException.ThrowIfNullOrWhiteSpace(number, nameof(number));

        return new BarAddressEntity(Guid.NewGuid(), street, number);
    }
}