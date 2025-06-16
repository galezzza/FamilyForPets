using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.Files.UseCases.Test;
using FamilyForPets.Framework.Responses.EndpointResults;
using FamilyForPets.SharedKernel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FamilyForPets.Files.API
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        //public FilesController( IOptions<MinioOptions>)
        public FilesController()
        {
        }

        [HttpPost]
        public async Task<EndpointResult<Guid>> Test(
            [FromServices] ICommandHandler<TestCommand, Guid> handler,
            CancellationToken cancellationToken)
        {
            return await handler.HandleAsync(new TestCommand(), cancellationToken);
        }
    }
}
