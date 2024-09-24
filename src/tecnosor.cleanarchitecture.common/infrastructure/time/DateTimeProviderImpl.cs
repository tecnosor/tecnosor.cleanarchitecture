using tecnosor.cleanarchitecture.common.domain.time;

namespace tecnosor.cleanarchitecture.common.infrastructure.time;

internal sealed class DateTimeProviderImpl : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
