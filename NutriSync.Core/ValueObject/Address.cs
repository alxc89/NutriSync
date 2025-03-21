namespace NutriSync.Core.ValueObject;

public record Address
{
    protected Address()
    {

    }

    public Address(string street, string city, string state, string zipCode)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    public string Street { get; } = string.Empty;
    public string City { get; } = string.Empty;
    public string State { get; } = string.Empty;
    public string ZipCode { get; } = string.Empty;
}
