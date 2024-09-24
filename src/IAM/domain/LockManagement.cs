using tecnosor.cleanarchitecture.common.domain;

namespace iam.domain;

public class LockManagement : ValueObject
{
    public LockoutEnd LockoutEnd { get; }
    public bool LockoutEnabled { get; }
    public int AccessFailedCount { get; }

    public LockManagement(LockoutEnd lockoutEnd,
                          bool lockoutEnabled,
                          int accessFailedCount)
    {
        if (accessFailedCount < 0)
            throw new ArgumentException("AccessFailedCount cannot be negative.");

        LockoutEnd = lockoutEnd;
        LockoutEnabled = lockoutEnabled;
        AccessFailedCount = accessFailedCount;
    }
}