using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ImageMagick;
using NetVips;
using static NetVips.Enums;
using Image_Compression.Models;

namespace Image_Compression.Services
{
    public class ImageCompresser
    {
        static int count = 0;
        //Netvips Tiff

        //For single image
        public async Task<byte[]> compress(string path)
        {
            byte[] originalBytes = null;
            byte[] compressedBytes = null;
            bool f = false;
            try
            {
                string fileName = Path.GetFileName(path);
                using var image = NetVips.Image.NewFromFile(path);
                using (WebClient webClient = new WebClient())
                {
                    originalBytes = webClient.DownloadData(path);
                }
                NetVips.Image img1 = ByteToImg(originalBytes);
                NetVips.Image img = img1.Smartcrop((int)(img1.Width * 0.8), (int)(img1.Height * 0.8), Interesting.Centre);
                count++;
                compressedBytes = img.TiffsaveBuffer(ForeignTiffCompression.Jpeg);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured");
            }
             return compressedBytes;
        }

        //For multiple image
        /*public async Task compress(string path)
        {
            byte[] originalBytes = null;
            bool f = false;
            try
            {
                string fileName = Path.GetFileName(path);
                using var image = NetVips.Image.NewFromFile(path);
                using (WebClient webClient = new WebClient())
                {
                    originalBytes = webClient.DownloadData(path);
                }
                NetVips.Image img1 = ByteToImg(originalBytes);
                NetVips.Image img = img1.Smartcrop((int)(img1.Width  * 0.8), (int)(img1.Height * 0.8), Interesting.Centre);
                count++;
                String writePath = "C:\\Users\\HIMANI\\Pictures\\Compress Image\\Compress\\" + count + "_" + fileName;
                img.Tiffsave(writePath, compression: ForeignTiffCompression.Jpeg);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured");
            }
        }*/

        //ImageMagick
        /*public async Task compress(string path)
        {
            byte[] compressedBytes = null;
            bool f = false;
            try
            {
                string fileName = Path.GetFileName(path);
                var optimizer = new ImageOptimizer();
                optimizer.LosslessCompress(fileInfo(path));
                using (WebClient webClient = new WebClient())
                {
                    compressedBytes = webClient.DownloadData(path);
                }
                NetVips.Image img = ByteToImg(compressedBytes);
                count++;
                String writePath = "C:\\Users\\HIMANI\\Pictures\\Compress Image\\Compress\\" + count + "_" + fileName;
                img.Jpegsave(writePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured");
            }
            //return compressedBytes;
        }*/

        private NetVips.Image ByteToImg(byte[] byteArr)
        {
            var ms = new MemoryStream(byteArr);
            return NetVips.Image.NewFromStream(ms);
        }

        //Netvips Resize

        /*        public async Task<byte[]> compress(string path)
                {
                    byte[] compressedBytes = null;
                    bool f = false;
                    try
                    {
                        double scaleFactor = 0.7;
                        string fileName = Path.GetFileName(path);
                        using var image = Image.NewFromFile(path);

                        Image resize = image.Resize(scaleFactor);
                        int position = path.LastIndexOf(fileName);
                        string path2 = path.Substring(0, position) + "compressed_" + fileName;
                        resize.Jpegsave(path2);
                        using (WebClient webClient = new WebClient())
                        {
                            compressedBytes = webClient.DownloadData(path2);
                        }

                    }
                    catch (Exception e)
                    {

                    }
                    return compressedBytes;
                }*/


        //Drawing 
        /* public async Task<byte[]> compress(string path)
         {
             Bitmap myBitmap;
             ImageCodecInfo myImageCodecInfo;
             Encoder myEncoder;
             EncoderParameter myEncoderParameter;
             EncoderParameters myEncoderParameters;
             myBitmap = new Bitmap(path);
             myImageCodecInfo = GetEncoderInfo("image/jpeg");
             myEncoder = Encoder.Compression;
             myEncoderParameters = new EncoderParameters();
             myEncoderParameter = new EncoderParameter(
                 myEncoder,
                 (long)EncoderValue.CompressionRle);
             myEncoderParameters.Param[0] = myEncoderParameter;
             myBitmap.Save("C:\\Users\\HIMANI\\Pictures\\Compress Image\\Compress\\drawing.jpg", myImageCodecInfo, myEncoderParameters);
             return new byte[] { 0 };

         }
         private static ImageCodecInfo GetEncoderInfo(String mimeType)
         {
             int j;
             ImageCodecInfo[] encoders;
             encoders = ImageCodecInfo.GetImageEncoders();
             for (j = 0; j < encoders.Length; ++j)
             {
                 if (encoders[j].MimeType == mimeType)
                     return encoders[j];
             }
             return null;
         }*/



      /*  private byte[] ImgToByte(Netvips.Image image)
        {
            ImageConverter imageConverter = new ImageConverter();
            return (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
        }*/

        private FileInfo fileInfo(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            return file;
        }
       
    }
}
