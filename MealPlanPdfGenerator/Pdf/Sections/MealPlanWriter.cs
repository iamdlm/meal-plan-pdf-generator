using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MealPlanPdfGenerator.Models;
using MealPlanPdfGenerator.Pdf.Core;
using MealPlanPdfGenerator.Pdf.Sections.MealPlan;

namespace MealPlanPdfGenerator.Pdf.Sections
{
    public static class MealPlanWriter
    {
        public static void Write(PdfDocument pdfDoc, Document doc, FormEntry form)
        {
            int dayCount = 1;

            // for demo purposes use just one meal
            var day = form.MealPlan.Days.FirstOrDefault();

            WriteDayMenu(pdfDoc, doc, day, dayCount);

            // for demo purposes use just one meal
            var meal = day.Meals.FirstOrDefault();

            // Create a new page for each meal
            PdfFormatUtils.AddSectionBreak(doc);

            // Add title section
            PdfHeaderFormatter.AddHeader(doc, meal.Title);

            // Write the recipe using the specialized RecipeWriter
            RecipeWriter.WriteRecipe(pdfDoc, doc, meal, day.Calories);
        }

        private static void WriteDayMenu(PdfDocument pdfDoc, Document doc, Day day, int dayCount)
        {
            PdfFormatUtils.AddSectionBreak(doc);
            PdfHeaderFormatter.AddHeader(doc, $"Day {dayCount}");

            int mealNumber = 1;

            foreach (var meal in day.Meals)
            {
                int marginTop = mealNumber == 1 ? 0 : 20;

                doc.Add(new Paragraph($"{mealNumber:00} / {meal.Title}")
                    .SetFont(PdfStyleSettings.HeadingBoldFont)
                    .SetFontSize(20)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginTop(marginTop));

                mealNumber++;
            }
        }
    }
} 