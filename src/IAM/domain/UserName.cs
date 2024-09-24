using tecnosor.cleanarchitecture.common.domain;

namespace iam.domain;

public class UserName : ValueObject
{
    public string Value { get; }

    public UserName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("UserName cannot be empty or null."); // TODO: Create specific exceptions?... should be

        if (value.Length < 3 || value.Length > 50)
            throw new ArgumentException("UserName must be between 3 and 50 characters long.");

        Value = value;
    }

    public override string ToString() => Value;
}