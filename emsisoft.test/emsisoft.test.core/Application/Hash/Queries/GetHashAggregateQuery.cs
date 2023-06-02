using emsisoft.test.core.Application.Hash.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emsisoft.test.core.Application.Hash.Queries
{
    public record GetHashAggregateQuery() : IRequest<HashAggregate[]>;
}
