using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
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

            var meals = day.Meals;

            foreach (var meal in meals)
            {
                // Create a new page for each meal
                PdfFormatUtils.AddSectionBreak(doc);

                AddMealDetailHeader(pdfDoc, doc, meal);

                // Write the recipe using the specialized RecipeWriter
                RecipeWriter.WriteRecipe(pdfDoc, doc, meal, day.Calories);
            }

            PdfFormatUtils.AddSectionBreak(doc);
        }

        private static void WriteDayMenu(PdfDocument pdfDoc, Document doc, Day day, int dayCount)
        {
            // Title
            Paragraph heading = new Paragraph()
                .SetFont(PdfStyleSettings.TitleFont)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(48)
                .Add(new Text("DAY "))
                .Add(new Text(dayCount.ToString())
                    .SetFont(PdfStyleSettings.TitleBoldFont));

            doc.Add(heading);

            int mealNumber = 1;

            foreach (var meal in day.Meals)
            {
                var menuIconCell = new Cell(2, 1)
                        .SetVerticalAlignment(VerticalAlignment.TOP)
                        .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                        .SetBorder(Border.NO_BORDER);

                var titleCell = new Cell()
                        .SetBorder(Border.NO_BORDER);

                var pageNumberCell = new Cell()
                    .SetPaddingTop(10)
                    .SetVerticalAlignment(VerticalAlignment.TOP)
                    .SetBorder(Border.NO_BORDER);

                var descriptionCell = new Cell(1, 2)
                    .SetPaddingTop(10)
                    .SetBorder(Border.NO_BORDER)
                    .SetBorderTop(new SolidBorder(PdfStyleSettings.MealDividerTextColor, 1));

                AddMealIcon(menuIconCell, meal);
                AddMealTitle(titleCell, meal);
                AddMealPageNumber(pageNumberCell, meal);
                AddMealDescription(descriptionCell, meal);

                var columnWidths = new float[] { 2, 5, 1 };
                var row = new Table(UnitValue.CreatePercentArray(columnWidths))
                    .UseAllAvailableWidth();

                // space between meal
                if (mealNumber > 0)
                {
                    row.AddCell(new Cell(1, 3).SetBorder(Border.NO_BORDER).SetHeight(24));
                }

                row.AddCell(menuIconCell)
                    .AddCell(titleCell)
                    .AddCell(pageNumberCell)
                    .AddCell(descriptionCell);

                doc.Add(row);

                mealNumber++;
            }
        }

        private static void AddMealIcon(Cell container, Meal meal)
        {
            if (string.IsNullOrEmpty(meal.Image)) return;

            byte[] imageBytes = Convert.FromBase64String(meal.Image);
            Image img = new Image(ImageDataFactory.Create(imageBytes))
                .SetMarginRight(10)
                .SetAutoScale(true)
                .SetBorderRadius(new BorderRadius(50));

            container.Add(img);
        }

        private static void AddMealTitle(Cell container, Meal meal)
        {
            var titleNumber = meal.Number.ToString().PadLeft(2, '0');

            Paragraph title = new Paragraph($"{titleNumber} / {meal.Title}")
                .SetFont(PdfStyleSettings.TitleFont)
                .SetFontColor(PdfStyleSettings.MealTextColor)
                .SetFontSize(20);

            container.Add(title);
        }

        private static void AddMealPageNumber(Cell container, Meal meal)
        {
            var pageNumberText = $"P. {meal.Number.ToString().PadLeft(2, '0')}";
            Paragraph pageNumber = new Paragraph(pageNumberText)
                .SetFont(PdfStyleSettings.BodyBoldFont)
                .SetItalic()
                .SetFontSize(16)
                .SetFontColor(PdfStyleSettings.MealTextColor)
                .SetTextAlignment(TextAlignment.RIGHT);

            container.Add(pageNumber);
        }

        private static void AddMealDescription(Cell continainer, Meal meal)
        {
            Paragraph desc = new Paragraph(meal.Summary)
                .SetFontSize(14);

            continainer.Add(desc);
        }

        private static void AddMealDetailHeader(PdfDocument pdfDoc, Document doc, Meal meal)
        {
            PageSize pageSize = pdfDoc.GetDefaultPageSize();
            float pageWidth = pageSize.GetWidth();
            float pageHeight = pageSize.GetHeight();
            float leftMargin = doc.GetLeftMargin();
            float rightMargin = doc.GetRightMargin();
            float bgHeaderHeight = 180;

            PdfCanvas bgCanvas = new PdfCanvas(pdfDoc, pdfDoc.GetPageNumber(pdfDoc.GetLastPage()));
            bgCanvas.SaveState()
                .SetFillColor(PdfStyleSettings.RecipeHeaderColor)
                .Rectangle(0, pageHeight - bgHeaderHeight, pageWidth, bgHeaderHeight)
                .Fill()
                .RestoreState();

            if (meal.Image != null)
            {
                float imageContainerWidth = pageWidth - leftMargin - rightMargin;
                float imageContainerHeight = 210;
                float containerAspectRatio = imageContainerWidth / imageContainerHeight;

                byte[] imageBytes = Convert.FromBase64String(meal.Image);
                imageBytes = PdfDrawUtils.CropImageByAspectRatio(imageBytes, containerAspectRatio);

                Image img = new Image(ImageDataFactory.Create(imageBytes))
                    .SetWidth(UnitValue.CreatePercentValue(100));
                img.SetMarginBottom(20);

                doc.Add(img);
            }
        }
    }
}