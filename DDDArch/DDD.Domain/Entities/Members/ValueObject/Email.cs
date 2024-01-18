namespace DDD.Domain.Entities.Members.ValueObject;

public class Email : Domain.ValueObject
{
    private Email(string value)
    {
        Value = value;
    }
    public string Value { get; }

    public static Email Create(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new Exception("Email is empty.");

        if(!email.Contains("."))
            throw new Exception("Wrong email.");
        
        return new Email(email);
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}