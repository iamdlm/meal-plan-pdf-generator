using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using MealPlanPdfGenerator.Models;
using MealPlanPdfGenerator.Pdf.Core;
using MealPlanPdfGenerator.Pdf.Events;
using MealPlanPdfGenerator.Pdf.Sections;

namespace MealPlanPdfGenerator.Pdf
{
    public class PdfService : IPdfService
    {
        public byte[] Write(FormEntry form)
        {
            // Create the PDF and get its file contents as a byte array
            using var stream = new MemoryStream();

            // Create writer properties with compression settings
            var writerProperties = new WriterProperties()
                .SetPdfVersion(PdfVersion.PDF_1_5)
                .UseSmartMode()  // Enables object reuse
                .SetCompressionLevel(CompressionConstants.BEST_COMPRESSION);

            // Create a new PDF document with compression settings
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(stream, writerProperties));

            // Set page size to A4
            PageSize pageSize = PageSize.A4;
            pdfDoc.SetDefaultPageSize(pageSize);

            // Add footer handler
            pdfDoc.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterEventHandler());

            // Create a new document layout
            Document document = new Document(pdfDoc);

            // Set font
            document.SetFont(PdfStyleSettings.BodyFont);
            document.SetMargins(40, 60, 40, 60);

            // Following pages with meal plan details
            MealPlanWriter.Write(pdfDoc, document, form);

            // Fitness assessment
            FitnessAssessmentWriter.Write(document, form.Age, form.WeightKg, form.Height, form.Activity.ToString());

            // Write the shopping list
            ShoppingListWriter.Write(document, form.MealPlan.ShoppingList);

            // Close the document
            document.Close();

            return stream.ToArray();
        }

        public bool Save(byte[] pdfBytes, string filePath)
        {
            string dirName = System.IO.Path.GetDirectoryName(filePath);

            if (!Directory.Exists(dirName))
            {
                return false;
            }

            // Ensure the directory exists
            Directory.CreateDirectory(dirName);

            // Write the byte array to the specified file path
            File.WriteAllBytes(filePath, pdfBytes);

            return true;
        }
    }
}
