using Microsoft.AspNetCore.Mvc;

namespace FamilyForPets.Framework
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("{id:guid}/sdadas")]
        public async Task<Guid> SomeMethodName(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            return id;
        }
    }
}
