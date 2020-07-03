using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleFramework.Utils.Imaging
{
    public static class ImagingHelper
    {
        public static bool IsValidImageFormat(string path)
        {
            using (var fs = File.OpenRead(path))
            {
                byte[] header = new byte[10];
                fs.Read(header, 0, 10);

                foreach (var pattern in new byte[][] {
                    Encoding.ASCII.GetBytes("BM"),
                    Encoding.ASCII.GetBytes("GIF"),
                    new byte[] { 137, 80, 78, 71 },     // PNG
                    new byte[] { 73, 73, 42 },          // TIFF
                    new byte[] { 77, 77, 42 },          // TIFF
                    new byte[] { 255, 216, 255, 224 },  // jpeg
                    new byte[] { 255, 216, 255, 225 }   // jpeg canon
                })
                {
                    if (pattern.SequenceEqual(header.Take(pattern.Length)))
                        return true;
                }
            }

            return false;
        }

        public static Image ResizeImage(Image image, int newWidth, int newHeight)
        {
            // Algorithm to resize proportionally the image
            // 2.) Determine the target image ratio and the file's image ratio (width divided by depth)
            // 3.) If the file's image ratio is greater-than the target image ratio, then we know that 
            // we have to size proportionally based on WIDTH, otherwise we size proportionally based 
            // on HEIGHT (and if it was not a valid image, return an empty resize string) 
            double originalRatio = (double)image.Width / image.Height;
            double standardRatio = (double)newWidth / newHeight;

            // Constraint the sizes
            if (originalRatio > standardRatio)
                newHeight = (int)(image.Height * newWidth / image.Width);
            else
                newWidth = (int)(image.Width * newHeight / image.Height);

            System.Drawing.Image result = new Bitmap(newWidth, newHeight);
            System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(result);

            // Set the quality options
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.CompositingQuality = CompositingQuality.HighQuality;

            // Draw the image with the resized dimensions
            graphic.DrawImage(image, 0, 0, newWidth, newHeight);

            return result;
        }

        public static Image ResizeImagePercent(Image imgToResize, int Resolution, int Percent)
        {

            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            decimal percent = Percent / 100.00M;
            int destWidth = sourceWidth;
            int destHeight = sourceHeight;
            if (sourceWidth > 5)
                destWidth = (int)(sourceWidth * percent);

            if (sourceHeight > 5)
                destHeight = (int)(sourceHeight * percent);

            Bitmap b = new Bitmap(destWidth, destHeight);

            Graphics g = Graphics.FromImage((System.Drawing.Image)b);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            b.SetResolution(Resolution, Resolution);
            return b;
        }

    }
}
