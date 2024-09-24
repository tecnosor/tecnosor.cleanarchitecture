using tecnosor.cleanarchitecture.common.domain;

namespace iam.domain;

public class PhoneNumber : ValueObject
{
    public string Value { get; }

    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !IsValidPhoneNumber(value))
            throw new ArgumentException("Invalid phone number format.");

        Value = value;
    }

    private bool IsValidPhoneNumber(string phoneNumber)
    {
        return phoneNumber.All(char.IsDigit) && phoneNumber.Length >= 7 && phoneNumber.Length <= 15;
    }

    public override string ToString() => Value;
}
