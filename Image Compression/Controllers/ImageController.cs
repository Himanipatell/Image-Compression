using Image_Compression.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Image_Compression.Models;
using System.Net.Http;
using System.Collections.Generic;

namespace Image_Compression.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : Controller
    {
        [HttpPost]
        //  public async Task<IActionResult> ImageCompress([FromQuery] string path)
        public async Task<IActionResult> ImageCompress([FromBody] ImageModel imageobj)
        {
            bool a = false;
            var tasks = new List<Task>();
            int i = 0;
            foreach (string path in imageobj.images)
            {
                string s = "";
                HttpClient httpCleint = new HttpClient();
                HttpResponseMessage responseMessage = new HttpResponseMessage();
                ImageCompresser imageCompresser = new ImageCompresser();
                // byte[] compressedBytes =
               
                 Task t=  Task.Run(() =>
                    {
                       imageCompresser.compress(path);
                    });
                tasks.Add(t);
            }
            //return Ok(await imageCompresser.compress(path));          
            //Task.WaitAll();
           // Task.WaitAny(tasks.ToArray());
            Task.WaitAll(tasks.ToArray());
            return Ok("images compressed"); ;
            //else
              //  return NoContent();
        }
    }
}
