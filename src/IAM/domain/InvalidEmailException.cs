using tecnosor.cleanarchitecture.common.domain;

namespace iam.domain;

public class InvalidEmailException : DomainException
{
        public InvalidEmailException(string email)
            : base($"The email '{email}' is not valid.") { }
}
