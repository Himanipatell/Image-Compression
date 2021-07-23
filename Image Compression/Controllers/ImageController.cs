using Image_Compression.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Image_Compression.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : Controller
    {
       [HttpPost]
       public async Task<IActionResult> ImageCompress([FromQuery] string path)
        {
            ImageCompresser imageCompresser = new ImageCompresser();
            byte[] compressedBytes=await imageCompresser.compress(path); 
            return Ok(new { compressedBytes = compressedBytes });
        }
    }
}
