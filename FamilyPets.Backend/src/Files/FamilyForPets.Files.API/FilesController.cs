using FamilyForPets.Files.Infrastructure.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FamilyForPets.Files.API
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly MinioOptions _options;

        public FilesController( IOptionsMonitor<MinioOptions> options)
        {
            _options = options.CurrentValue;
        }

    }
}
