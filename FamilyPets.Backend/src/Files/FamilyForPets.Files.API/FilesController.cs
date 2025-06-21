using Microsoft.AspNetCore.Mvc;

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
    }
}
