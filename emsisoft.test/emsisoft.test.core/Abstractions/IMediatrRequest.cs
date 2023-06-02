using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emsisoft.test.core.Abstractions
{
    public interface IMediatrRequest<TResponse> : IRequest<TResponse>
    {
    }
}
