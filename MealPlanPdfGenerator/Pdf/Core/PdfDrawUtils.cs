using MealPlanPdfGenerator.Pdf.ViewModels;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;

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

        public static byte[] CreateMacrosDistributionPieChart(MacroDistributionViewModel carb, MacroDistributionViewModel fat, MacroDistributionViewModel protein)
        {
            int width = 400, height = 300;
            Bitmap bitmap = new Bitmap(width, height);
            using Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);

            float carbAvg = carb.Avg;
            float fatAvg = fat.Avg;

            // Pie sections
            float[] values = { carbAvg, fatAvg, 100 - carbAvg - fatAvg }; // carb, fat, protein
            string[] labels = { $"CARB\n{carb.RangeText}", $"FAT\n{fat.RangeText}", $"PROTEIN\n{protein.RangeText}" };
            Color[] colors = { Color.FromArgb(177, 156, 107), Color.FromArgb(213, 200, 175), Color.FromArgb(199, 182, 148) };

            float total = 100f;
            float startAngle = 0;
            Rectangle pieRect = new Rectangle(100, 50, 150, 150);

            using (var fontCollection = new PrivateFontCollection())
            {
                fontCollection.AddFontFile(PdfStyleSettings.TitleBoldFontPath);
                fontCollection.AddFontFile(PdfStyleSettings.BodyFontPath);

                Font boldFont = new Font(fontCollection.Families[0], 14);
                Font labelFont = new Font(fontCollection.Families[1], 12);
                Brush textBrush = Brushes.Black;

                for (int i = 0; i < values.Length; i++)
                {
                    float sweepAngle = values[i] / total * 360f;
                    using Brush b = new SolidBrush(colors[i]);
                    g.FillPie(b, pieRect, startAngle, sweepAngle);

                    // Label positions
                    double angle = (startAngle + sweepAngle / 2) * Math.PI / 180;

                    string[] parts = labels[i].Split('\n');
                    string label = parts[0];
                    string rangeValue = parts[1];
                    int labelSize = Math.Max(label.Length, rangeValue.Length) * 8;

                    double cosResult = Math.Cos(angle);
                    double offsetX = cosResult >= 0 ? cosResult * 120 : cosResult * (100 + labelSize);

                    double sinResult = Math.Sin(angle);
                    double offsetY = sinResult >= 0 ? sinResult * 80 : sinResult * 120;

                    float labelX = (float)(pieRect.X + pieRect.Width / 2 + offsetX);
                    float labelY = (float)(pieRect.Y + pieRect.Height / 2 + offsetY);

                    // Draw label (bold + normal)
                    g.DrawString(parts[0], boldFont, textBrush, labelX, labelY);
                    g.DrawString(parts[1], labelFont, textBrush, labelX, labelY + 18);

                    startAngle += sweepAngle;
                }
            }

            // Export as byte[]
            using MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }
}
