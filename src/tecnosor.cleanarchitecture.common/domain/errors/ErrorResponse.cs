namespace tecnosor.cleanarchitecture.common.domain.errors;

public sealed record ErrorResponse(
  int StatusCode,
  string Message
);