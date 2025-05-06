using MealPlanPdfGenerator.MockData;
using MealPlanPdfGenerator.Models;
using MealPlanPdfGenerator.Pdf;

namespace MealPlanPdfGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating meal plan PDF...");
            
            // Get mock data
            var formEntry = MockDataGenerator.CreateFormEntry();
            Console.WriteLine("Mock data created successfully.");
            
            // Create PDF service
            IPdfService pdfService = new PdfService();
            
            // Generate PDF
            Console.WriteLine("Writing PDF content...");
            byte[] pdfBytes = pdfService.Write(formEntry);
            Console.WriteLine($"PDF generated with size: {pdfBytes.Length / 1024} KB");
            
            // Save to file
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MealPlan.pdf");
            Console.WriteLine($"Saving PDF to: {outputPath}");
            bool saved = pdfService.Save(pdfBytes, outputPath);
            
            if (saved)
            {
                Console.WriteLine($"PDF generated successfully and saved to: {outputPath}");
                Console.WriteLine("You can now open the PDF file to view your meal plan.");
            }
            else
            {
                Console.WriteLine("Failed to save the PDF. Please check if the directory is writable.");
            }
        }
    }
}