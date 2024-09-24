using tecnosor.cleanarchitecture.common.domain;

namespace iam.domain;

public class ExternalAuth(bool isExternalUser, EProvider providerName) : ValueObject
{
    public bool IsExternalUser { get; } = isExternalUser;
    public EProvider ProviderName { get; } = providerName;
}


public enum EProvider
{
    GOOGLE,
    FACEBOOK
}