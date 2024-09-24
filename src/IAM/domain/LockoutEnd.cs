using tecnosor.cleanarchitecture.common.domain;

namespace iam.domain;

public class LockoutEnd : ValueObject
{
    public DateTime? Value { get; }

    public LockoutEnd(DateTime? value)
    {
        if (value.HasValue && value.Value < DateTime.UtcNow)
            throw new ArgumentException("LockoutEnd cannot be in the past.");

        Value = value;
    }

    public override string ToString() => Value?.ToString() ?? "No Lockout End";
}