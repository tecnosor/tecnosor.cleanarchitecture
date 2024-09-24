using tecnosor.cleanarchitecture.common.domain.mediator;

namespace tecnosor.cleanarchitecture.common.infrastructure.mediator;

public class MediatorAdapter : IMediator
{
    private readonly MediatR.IMediator _mediator;

    public MediatorAdapter(MediatR.IMediator mediator)
    {
        _mediator = mediator;
    }

    // Implementación de CreateStream<TResponse>
    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var mediatrRequest = request as MediatR.IStreamRequest<TResponse>;
        return _mediator.CreateStream(mediatrRequest, cancellationToken);
    }

    // Implementación de CreateStream(object)
    public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
    {
        return _mediator.CreateStream(request, cancellationToken);
    }

    // Implementación de Publish(object)
    public async Task Publish(object notification, CancellationToken cancellationToken = default)
    {
        await _mediator.Publish(notification, cancellationToken);
    }

    // Implementación de Publish<TNotification>
    public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        var mediatrNotification = notification as MediatR.INotification;
        await _mediator.Publish(mediatrNotification, cancellationToken);
    }

    // Implementación de Send<TResponse>
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var mediatrRequest = request as MediatR.IRequest<TResponse>;
        return await _mediator.Send(mediatrRequest, cancellationToken);
    }

    // Implementación de Send<TRequest>
    public async Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest
    {
        var mediatrRequest = request as MediatR.IRequest;
        await _mediator.Send(mediatrRequest, cancellationToken);
    }

    // Implementación de Send(object)
    public async Task<object?> Send(object request, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(request, cancellationToken);
    }
}

/*
 * using tecnosor.cleanarchitecture.common.domain.mediator;

namespace tecnosor.cleanarchitecture.common.infrastructure.mediator
{
    /// <summary>
    /// Adapter that implements the IMediator interface to decouple the use of MediatR in domain and application layers.
    /// This class acts as a bridge between the domain interface and MediatR.
    /// </summary>
    public class MediatorAdapter : IMediator
    {
        private readonly MediatR.IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the MediatorAdapter.
        /// </summary>
        /// <param name="mediator">Instance of MediatR.IMediator injected from infrastructure.</param>
        public MediatorAdapter(MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates an asynchronous stream of responses for a specific request type.
        /// </summary>
        /// <typeparam name="TResponse">The type of the expected response.</typeparam>
        /// <param name="request">Request of type IStreamRequest.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>An asynchronous stream of type TResponse.</returns>
        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var mediatrRequest = request as MediatR.IStreamRequest<TResponse>;
            return _mediator.CreateStream(mediatrRequest, cancellationToken);
        }

        /// <summary>
        /// Creates an asynchronous stream of responses for a generic request.
        /// </summary>
        /// <param name="request">Generic request.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>An asynchronous stream of responses of type object.</returns>
        public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
        {
            return _mediator.CreateStream(request, cancellationToken);
        }

        /// <summary>
        /// Publishes a notification of type object.
        /// </summary>
        /// <param name="notification">Notification of type object.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>An asynchronous task representing the publish operation.</returns>
        public async Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            await _mediator.Publish(notification, cancellationToken);
        }

        /// <summary>
        /// Publishes a generic notification of type TNotification.
        /// </summary>
        /// <typeparam name="TNotification">Notification type implementing INotification.</typeparam>
        /// <param name="notification">Notification instance of type TNotification.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>An asynchronous task representing the publish operation.</returns>
        public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : INotification
        {
            var mediatrNotification = notification as MediatR.INotification;
            await _mediator.Publish(mediatrNotification, cancellationToken);
        }

        /// <summary>
        /// Sends a request and waits for a response of type TResponse.
        /// </summary>
        /// <typeparam name="TResponse">The type of the expected response.</typeparam>
        /// <param name="request">Request instance implementing IRequest.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>The response of type TResponse.</returns>
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var mediatrRequest = request as MediatR.IRequest<TResponse>;
            return await _mediator.Send(mediatrRequest, cancellationToken);
        }

        /// <summary>
        /// Sends a request of type TRequest that does not expect a specific response.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request implementing IRequest.</typeparam>
        /// <param name="request">Request instance of type TRequest.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>An asynchronous task representing the send operation.</returns>
        public async Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IRequest
        {
            var mediatrRequest = request as MediatR.IRequest;
            await _mediator.Send(mediatrRequest, cancellationToken);
        }

        /// <summary>
        /// Sends a generic request of type object.
        /// </summary>
        /// <param name="request">Request of type object.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>The response as an object.</returns>
        public async Task<object?> Send(object request, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(request, cancellationToken);
        }
    }
}
*/