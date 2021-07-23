using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ImageMagick;
using NetVips;
namespace Image_Compression.Services
{
    public class ImageCompresser
    {
        public async Task<byte[]> compress(string path)
        {
            byte[] compressedBytes=null;
            try
            {
                /* double scaleFactor = 0.7;
                 string fileName = Path.GetFileName(path);
                 using var image = Image.NewFromFile(path);                 
                 Image resize = image.Resize(scaleFactor);
                 int position = path.LastIndexOf(fileName);
                 string path2 = path.Substring(0, position) + "compressed_" + fileName;
                 resize.Jpegsave(path2);
                 using (WebClient webClient = new WebClient())
                 {
                     compressedBytes = webClient.DownloadData(path2);
                 }*/
                string fileName = Path.GetFileName(path);
                var optimizer = new ImageOptimizer();
                optimizer.LosslessCompress(fileInfo(path));              
                int position = path.LastIndexOf(fileName);
                using (WebClient webClient = new WebClient())
                {
                    compressedBytes = webClient.DownloadData(path);
                }
                Image img = ByteToImg(compressedBytes);
                string path2 = path.Substring(0, position) + "compressed_" + fileName;
                img.Jpegsave(path2);
            }
            catch(Exception e)
            {

            }
            return compressedBytes;
        }
        private byte[] ImgToByte(Image image)
        {
            ImageConverter imageConverter = new ImageConverter();
            return (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
        }

        private FileInfo fileInfo(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            return file;
        }
        private Image ByteToImg(byte[] byteArr)
        {
            var ms = new MemoryStream(byteArr);
            return Image.NewFromStream(ms);
        }
    }
}
