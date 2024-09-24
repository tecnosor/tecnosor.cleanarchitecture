using tecnosor.cleanarchitecture.common.domain;

namespace stolenCars.publication.domain
{
    [Serializable]
    public class PublicationExistException : ExistException
    {
        public PublicationExistException() {}

        public PublicationExistException(string? message) : base(message) {}

        public PublicationExistException(string? message, Exception? innerException) : base(message, innerException) {}
    }
}