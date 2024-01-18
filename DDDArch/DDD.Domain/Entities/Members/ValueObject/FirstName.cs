namespace DDD.Domain.Entities.Members.ValueObject;

public sealed class FirstName : Domain.ValueObject
{
    public const int MaxLength = 50;
    
    private FirstName(string value)
    {
        Value = value;
    }
    public string Value { get; }

    public static FirstName Create(string firstName)
    {
        if (string.IsNullOrEmpty(firstName))
            throw new Exception("First name is empty.");

        if (firstName.Length > MaxLength)
        {
            throw new Exception("Firstname it too long");
        }

        return new FirstName(firstName);
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}