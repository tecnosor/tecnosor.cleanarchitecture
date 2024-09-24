namespace tecnosor.cleanarchitecture.common.domain.time;

interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}