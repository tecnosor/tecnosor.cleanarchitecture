using tecnosor.cleanarchitecture.common.domain;

namespace iam.domain;

public class PasswordHash : ValueObject
{
    public string Value { get; }

    public PasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("PasswordHash cannot be null or empty.");

        Value = value;
    }

    public override string ToString() => Value;
}