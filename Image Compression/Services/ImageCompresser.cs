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
        //time taken approx 18s size 240B
        //public async Task<byte[]> compress(string path)
        //{
        //    byte[] compressedBytes = null;
        //    bool f = false;
        //    try
        //    {
        //        count++;
        //        string fileName = Path.GetFileName(path);
        //        using var image = NetVips.Image.NewFromFile(path);
        //        compressedBytes = image.TiffsaveBuffer(compression: ForeignTiffCompression.Jpeg);
        //        NetVips.Image img = ByteToImg(compressedBytes);
        //        String writePath = "C:\\Users\\HIMANI\\Pictures\\Compress Image\\Compress\\" + count + fileName;
        //        img.Tiffsave(writePath, compression: ForeignTiffCompression.Jpeg);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Exception occured");
        //    }
        //    return compressedBytes;
        //}

        //ImageMagick
        //time taken aprox 8s size 240B
            public async Task compress(string path)
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
                String writePath = "C:\\Users\\HIMANI\\Pictures\\Compress Image\\Compress\\" + count +"_"+ fileName;
                     img.Jpegsave(writePath);
                 }
                 catch (Exception e)
                 {
                     Console.WriteLine("Exception occured");
                 }
                 //return compressedBytes;
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



        /* private byte[] ImgToByte(Image image)
         {
             ImageConverter imageConverter = new ImageConverter();
             return (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
         }*/

        private FileInfo fileInfo(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            return file;
        }
        private NetVips.Image ByteToImg(byte[] byteArr)
        {
            var ms = new MemoryStream(byteArr);
            return NetVips.Image.NewFromStream(ms);
        }
    }
}
