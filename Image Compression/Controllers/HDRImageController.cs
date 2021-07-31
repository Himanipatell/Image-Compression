using Image_Compression.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using static NetVips.Enums;

namespace Image_Compression.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HDRImageController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> ImageCompress([FromBody] ImageModel imageobj)
        {
            byte[] originalBytes = null;
            byte[] compressedBytes = null;
            try
            {
                string fileName = Path.GetFileName(imageobj.images[0]);
                using var image = NetVips.Image.NewFromFile(imageobj.images[0]);
                using (WebClient webClient = new WebClient())
                {
                    originalBytes = webClient.DownloadData(imageobj.images[0]);
                }

                NetVips.Image img1 = ByteToImg(originalBytes);
                NetVips.Image img = img1.Smartcrop((int)(img1.Width * 0.8), (int)(img1.Height * 0.8), Interesting.Centre);
                NetVips.Image i = NetVips.Image.Heifload(imageobj.images[0],access:Access.Random);
                bool a = img1.Equals(i);
                compressedBytes = img.HeifsaveBuffer(compression:ForeignHeifCompression.Hevc,lossless:true,speed:3);
               // compressedBytes = img.HeifsaveBuffer(compression: ForeignHeifCompression.Hevc);
                String writePath = "C:\\Users\\HIMANI\\Pictures\\Compress Image\\Compress\\" + "_" + fileName;
              //  i.Heifsave(writePath,compression:ForeignHeifCompression.Hevc);
               // ByteToImg(compressedBytes).Jpegsave(writePath);
                // img.Tiffsave(writePath, compression: ForeignTiffCompression.Jpeg);
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured");
            }

            return Ok(new { compressedBytes = compressedBytes });
        }

        private NetVips.Image ByteToImg(byte[] byteArr)
        {
            var ms = new MemoryStream(byteArr);
            return NetVips.Image.NewFromStream(ms);
        }
    }
}
