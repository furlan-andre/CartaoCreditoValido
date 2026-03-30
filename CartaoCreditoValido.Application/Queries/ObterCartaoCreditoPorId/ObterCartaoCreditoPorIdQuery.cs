using MediatR;

namespace CartaoCreditoValido.Application.Queries.ObterCartaoCreditoPorId;

public sealed record ObterCartaoCreditoPorIdQuery(long Id)
    : IRequest<ObterCartaoCreditoPorIdResponse?>;