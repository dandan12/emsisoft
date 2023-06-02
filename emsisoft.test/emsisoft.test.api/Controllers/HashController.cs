using emsisoft.test.core.Application.Hash.Commands;
using emsisoft.test.core.Application.Hash.Models;
using emsisoft.test.core.Application.Hash.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace emsisoft.test.api.Controllers
{
    [Route("api/hashes")]
    [ApiController]
    public class HashController : ControllerBase
    {
        private readonly IMediator mediator;

        public HashController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHashes()
        {
            var command = new CreateHashCommand();
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<HashAggregate[]> GetHashes()
        {
            var query = new GetHashAggregateQuery();
            var result = await mediator.Send(query);
            return result;
        }

    }
}
