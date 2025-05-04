using MealPlanPdfGenerator.Models;

namespace MealPlanPdfGenerator.Pdf
{
    public interface IPdfService
    {
        byte[] Write(FormEntry mealPlan);

        bool Save(byte[] pdfBytes, string filePath);
    }
}
