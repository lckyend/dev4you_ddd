namespace DDD.Domain.Entities.Members.ValueObject;

public class LastName : Domain.ValueObject
{
    public const int MaxLength = 50;
    
    private LastName(string value)
    {
        Value = value;
    }
    public string Value { get; }

    public static LastName Create(string lastName)
    {
        if (string.IsNullOrEmpty(lastName))
            throw new Exception("LastName is empty.");

        if (lastName.Length > MaxLength)
        {
            throw new Exception("LastName it too long");
        }

        return new LastName(lastName);
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}