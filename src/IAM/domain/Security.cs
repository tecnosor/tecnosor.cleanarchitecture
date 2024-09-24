using tecnosor.cleanarchitecture.common.domain;

namespace iam.domain;

public class Security : ValueObject
{
    public PasswordHash PasswordHash { get; }
    public bool TwoFactorEnabled { get; }

    public Security(PasswordHash passwordHash, bool twoFactorEnabled)
    {
        PasswordHash = passwordHash;
        TwoFactorEnabled = twoFactorEnabled;
    }
}