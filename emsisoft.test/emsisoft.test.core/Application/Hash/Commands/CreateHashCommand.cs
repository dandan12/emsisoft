using MediatR;

namespace emsisoft.test.core.Application.Hash.Commands
{
    public record CreateHashCommand() : IRequest<Unit>;
}
