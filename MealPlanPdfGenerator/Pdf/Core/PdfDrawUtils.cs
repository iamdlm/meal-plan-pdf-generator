using System.Drawing;

namespace MealPlanPdfGenerator.Pdf.Core
{
    public static class PdfDrawUtils
    {
        public static byte[] CropImageByAspectRatio(byte[] inputBytes, float aspectRatio)
        {
            using (var inputStream = new MemoryStream(inputBytes))
            using (var originalImage = Image.FromStream(inputStream))
            {
                int imageWidth = originalImage.Width;
                int cropHeight = (int)Math.Floor(imageWidth / aspectRatio);

                int cropX = 0;
                int cropY = Math.Max(0, (originalImage.Height - cropHeight) / 2);

                var cropArea = new Rectangle(cropX, cropY, imageWidth, cropHeight);

                using (var bmpImage = new Bitmap(originalImage))
                using (var croppedImage = bmpImage.Clone(cropArea, bmpImage.PixelFormat))
                using (var outputStream = new MemoryStream())
                {
                    croppedImage.Save(outputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return outputStream.ToArray();
                }
            }

        }
    }
}
