using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Svg.Converter;
using MealPlanPdfGenerator.Models;
using MealPlanPdfGenerator.Pdf.Core;

namespace MealPlanPdfGenerator.Pdf.Sections.MealPlan
{
    public static class RecipeWriter
    {
        public static void WriteRecipe(PdfDocument pdfDoc, Document doc, Meal meal, double dayCalories)
        {
            AddHeaderBackground(pdfDoc, doc, meal);

            // Add title
            AddTitle(pdfDoc, doc, meal);

            // Add recipe information header
            AddRecipeInformation(pdfDoc, doc, meal);

            // Create main content container
            float[] columnWidths = { 1, 1 };
            Table mainContent = new Table(UnitValue.CreatePercentArray(columnWidths));
            mainContent.SetWidth(UnitValue.CreatePercentValue(100));

            // Left column content
            Cell leftColumn = new Cell();
            leftColumn.SetBorder(Border.NO_BORDER);

            // Add ingredients
            AddIngredients(pdfDoc, leftColumn, meal);

            // Add preparation steps
            AddPreparationSteps(pdfDoc, leftColumn, meal);

            mainContent.AddCell(leftColumn);

            // Right column content
            Cell rightColumn = new Cell();
            rightColumn.SetBorder(Border.NO_BORDER);

            // Add nutrition facts table
            AddNutritionFacts(rightColumn, meal, dayCalories);

            mainContent.AddCell(rightColumn);

            // Add the main content to the document
            doc.Add(mainContent);
        }
        private static void AddHeaderBackground(PdfDocument pdfDoc, Document doc, Meal meal)
        {
            PageSize pageSize = pdfDoc.GetDefaultPageSize();
            float pageWidth = pageSize.GetWidth();
            float pageHeight = pageSize.GetHeight();
            float leftMargin = doc.GetLeftMargin();
            float rightMargin = doc.GetRightMargin();
            float bgHeaderHeight = 89;

            PdfCanvas bgCanvas = new PdfCanvas(pdfDoc, pdfDoc.GetPageNumber(pdfDoc.GetLastPage()));
            bgCanvas.SaveState()
                .SetFillColor(PdfStyleSettings.RecipeHeaderColor)
                .Rectangle(0, pageHeight - bgHeaderHeight, pageWidth, bgHeaderHeight)
                .Fill()
                .RestoreState();
        }

        private static void AddTitle(PdfDocument pdfDoc, Document doc, Meal meal)
        {
            PageSize pageSize = pdfDoc.GetDefaultPageSize();
            float pageWidth = pageSize.GetWidth();
            float pageHeight = pageSize.GetHeight();
            float leftMargin = doc.GetLeftMargin();
            float rightMargin = doc.GetRightMargin();

            var title = meal.Title;
            var highlightedTitle = GetHighlightedTitle(title);
            var nonHighlightedTitle = GetNonHighlightedTitle(title);
            var iconWidth = 100;
            var paddingRightTitle = 10;
            var textWidth = pageWidth - iconWidth - paddingRightTitle - leftMargin - rightMargin;

            Table table = new Table(UnitValue.CreatePointArray(new float[] { textWidth, iconWidth }));
            table.SetMarginBottom(20);

            var initialFontSize = 50;
            var titleFontSize = PdfFormatUtils.GetFontSizeByMaxLine(title, initialFontSize, textWidth, 2);
            var fixedLeading = titleFontSize;

            Paragraph titleParagraph = new Paragraph()
                .SetCharacterSpacing(1)
                .SetFixedLeading(fixedLeading)
                .SetFontSize(titleFontSize);

            titleParagraph.Add(new Text(highlightedTitle.ToUpper()).SetFont(PdfStyleSettings.TitleFont));
            if (!string.IsNullOrEmpty(nonHighlightedTitle))
            {
                titleParagraph.Add(new Text($" {nonHighlightedTitle.ToUpper()}").SetFont(PdfStyleSettings.TitleBoldFont));
            }

            Cell titleCell = new Cell()
                .Add(titleParagraph)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetPaddings(0, paddingRightTitle, 0, 0)
                .SetBorder(Border.NO_BORDER);

            Cell iconCell = new Cell()
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetHorizontalAlignment(HorizontalAlignment.RIGHT)
                .SetPadding(0)
                .SetBorder(Border.NO_BORDER);

            AddIcon(iconCell, meal, iconWidth);

            table.AddCell(titleCell);
            table.AddCell(iconCell);

            doc.Add(table);
        }

        private static void AddIcon(Cell container, Meal meal, float iconWidth)
        {
            if (string.IsNullOrEmpty(meal.Image)) return;

            byte[] imageBytes = Convert.FromBase64String(meal.Image);
            Image img = new Image(ImageDataFactory.Create(imageBytes))
                .SetWidth(iconWidth)
                .SetBorderRadius(new BorderRadius(50))
                .SetBorder(new SolidBorder(PdfStyleSettings.IconBorderColor, 2));

            container.Add(img);
        }

        private static void AddIngredients(PdfDocument pdfDoc, Cell container, Meal meal)
        {
            AddSubSectionHeader(pdfDoc, container, "INGREDIENTS");

            // bullet
            byte[] svgBytes = File.ReadAllBytes(System.IO.Path.Combine("wwwroot", "svg", "circle.svg"));
            MemoryStream svgStream = new MemoryStream(svgBytes);
            Image bulletImage = SvgConverter.ConvertToImage(svgStream, pdfDoc);
            bulletImage.SetHeight(5)
                .SetWidth(5)
                .SetMarginBottom(3);

            List ingredients = new List()
                .SetMarginLeft(5)
                .SetFont(PdfStyleSettings.BodyFont)
                .SetFontSize(14)
                .SetListSymbol(bulletImage)
                .SetSymbolIndent(8)
                .SetMarginBottom(20);

            foreach (var ingredient in meal.Ingredients)
            {
                var formattedQuantity = PdfFormatUtils.FormatQuantity(ingredient.Quantity);
                string ingredientText = $"{formattedQuantity} {ingredient.Unit} {ingredient.Name}";
                ingredients.Add(new ListItem(ingredientText));
            }

            container.Add(ingredients);
        }

        private static void AddPreparationSteps(PdfDocument pdfDoc, Cell container, Meal meal)
        {
            AddSubSectionHeader(pdfDoc, container, "DIRECTIONS");

            List steps = new List(ListNumberingType.DECIMAL)
                .SetMarginLeft(5)
                .SetFont(PdfStyleSettings.BodyFont)
                .SetFontSize(14)
                .SetSymbolIndent(3)
                .SetMarginBottom(20);

            foreach (var instruction in meal.Preparation.OrderBy(p => p.Index))
            {
                steps.Add(new ListItem(instruction.Description));
            }

            container.Add(steps);
        }

        private static void AddSubSectionHeader(PdfDocument pdfDoc, Cell container, string title)
        {
            // bullet
            byte[] svgBytes = File.ReadAllBytes(System.IO.Path.Combine("wwwroot", "svg", "bullet.svg"));
            MemoryStream svgStream = new MemoryStream(svgBytes);
            Image bulletImage = SvgConverter.ConvertToImage(svgStream, pdfDoc);
            bulletImage.SetHeight(14)
                .SetWidth(20);

            Paragraph header = new Paragraph()
                .Add(bulletImage)
                .Add($" {title}")
                .SetFont(PdfStyleSettings.TitleFont)
                .SetFontSize(16)
                .SetFontColor(PdfStyleSettings.MealTextColor);

            container.Add(header);
        }

        private static void AddRecipeInformation(PdfDocument pdfDoc, Document doc, Meal meal)
        {
            // Create a table with 3 equal columns
            Table infoTable = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 1 }));
            infoTable.SetMarginBottom(14);
            infoTable.SetWidth(UnitValue.CreatePercentValue(100));

            // Add thin black borders on top and bottom only
            infoTable.SetBorder(Border.NO_BORDER);
            infoTable.SetBorderTop(new SolidBorder(PdfStyleSettings.MealDividerTextColor, 1f));
            infoTable.SetBorderBottom(new SolidBorder(PdfStyleSettings.MealDividerTextColor, 1f));

            AddInfoCell(pdfDoc, infoTable, "prep.svg", "PREP TIME", $"{meal.PrepTime} MIN");
            AddInfoCell(pdfDoc, infoTable, "cook.svg", "COOK TIME", $"{meal.CookTime} MIN");
            AddInfoCell(pdfDoc, infoTable, "difficulty.svg", "DIFFICULTY", meal.DifficultyLevel.ToString());

            // Add the table to the document
            doc.Add(infoTable);
        }

        private static void AddInfoCell(PdfDocument pdfDoc, Table table, string svgFileName, string label, string value)
        {
            Cell cell = new Cell();
            cell.SetBorder(Border.NO_BORDER);
            cell.SetPaddings(10, 0, 10, 0);
            cell.SetTextAlignment(TextAlignment.CENTER);
            cell.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            // Add SVG icon
            byte[] svgBytes = File.ReadAllBytes(System.IO.Path.Combine("wwwroot", "svg", svgFileName));
            MemoryStream svgStream = new MemoryStream(svgBytes);
            Image svgImage = SvgConverter.ConvertToImage(svgStream, pdfDoc);
            svgImage.SetHeight(30);
            svgImage.SetWidth(30);
            svgImage.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            // Create separate paragraphs for each component            
            Cell iconCell = new Cell()
                .Add(svgImage)
                .SetPadding(0)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetBorder(Border.NO_BORDER);

            Paragraph description = new Paragraph()
                .SetFontSize(12)
                .SetCharacterSpacing(1f);
            description.Add(new Text(label)
                .SetFontColor(PdfStyleSettings.MealTextColor)
                .SetFont(PdfStyleSettings.TitleBoldFont));
            description.Add(new Text(" | ")
                .SetFontColor(PdfStyleSettings.MealTextColor)
                .SetFont(PdfStyleSettings.TitleFont));
            description.Add(new Text(value)
                .SetFont(PdfStyleSettings.TitleFont));
            Cell descriptionCell = new Cell()
                .Add(description)
                .SetPadding(0)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetBorder(Border.NO_BORDER);

            Table mainContent = new Table(UnitValue.CreatePointArray(new float[] { 36f, 140f }));
            mainContent.AddCell(iconCell);
            mainContent.AddCell(descriptionCell);

            cell.Add(mainContent);

            table.AddCell(cell);
        }

        private static void AddNutritionFacts(Cell container, Meal meal, double calories)
        {
            // Create main table with single column
            Table nutritionTable = new Table(1);
            nutritionTable
                .SetBackgroundColor(PdfStyleSettings.TableColor)
                .SetWidth(UnitValue.CreatePercentValue(100))
                .SetMarginTop(10);

            // Add "Nutrition Facts" header
            AddNutritionFactsHeader(nutritionTable);

            // Add "Amount per serving" row
            AddAmountPerServiceHeader(nutritionTable);

            AddCaloriesRow(nutritionTable, meal.Calories);
            AddDailyValueHeader(nutritionTable);
            AddNutrientRows(nutritionTable, meal, calories);
            AddNutritionFootnote(nutritionTable);

            container.Add(nutritionTable);
        }

        private static void AddNutritionFactsHeader(Table table)
        {
            Cell headerCell = new Cell()
                .Add(new Paragraph("Nutrition Facts")
                    .SetFont(PdfStyleSettings.TitleFont)
                    .SetFontSize(24)
                    .SetFixedLeading(20)
                    .SetFontColor(ColorConstants.BLACK))
                .SetBorder(new SolidBorder(PdfStyleSettings.MealTextColor, 1))
                .SetPadding(10);
            table.AddCell(headerCell);
        }

        private static void AddAmountPerServiceHeader(Table table)
        {
            Cell servingCell = new Cell()
                .Add(new Paragraph("Amount per serving")
                    .SetFont(PdfStyleSettings.TitleFont)
                    .SetFontSize(10)
                    .SetFixedLeading(8)
                    .SetFontColor(ColorConstants.BLACK))
                .SetBorder(Border.NO_BORDER)
                .SetBorderLeft(new SolidBorder(PdfStyleSettings.MealTextColor, 1))
                .SetBorderRight(new SolidBorder(PdfStyleSettings.MealTextColor, 1))
                .SetPaddings(10, 10, 4, 10);
            table.AddCell(servingCell);
        }

        private static void AddCaloriesRow(Table table, double calories)
        {
            Table caloriesTable = new Table(2);
            caloriesTable.SetWidth(UnitValue.CreatePercentValue(100));

            Cell labelCell = new Cell()
                .Add(new Paragraph("Calories")
                    .SetFontSize(24)
                    .SetFixedLeading(20)
                    .SetFont(PdfStyleSettings.TitleFont)
                    .SetBold())
                .SetBorder(Border.NO_BORDER)
                .SetPadding(0);

            Cell valueCell = new Cell()
                .Add(new Paragraph($"{calories:F0}")
                    .SetFontSize(24)
                    .SetFixedLeading(20)
                    .SetFont(PdfStyleSettings.TitleFont)
                    .SetBold())
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetPadding(0);

            caloriesTable.AddCell(labelCell);
            caloriesTable.AddCell(valueCell);

            Cell container = new Cell()
                .Add(caloriesTable)
                .SetPadding(10)
                .SetBorder(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(PdfStyleSettings.MealTextColor, 1))
                .SetBorderLeft(new SolidBorder(PdfStyleSettings.MealTextColor, 1))
                .SetBorderRight(new SolidBorder(PdfStyleSettings.MealTextColor, 1));
            table.AddCell(container);
        }

        private static void AddDailyValueHeader(Table table)
        {
            Cell dvHeaderCell = new Cell()
                .Add(new Paragraph("% Daily Value*")
                    .SetFont(PdfStyleSettings.BodyFont)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetFontSize(10)
                    .SetFixedLeading(8))
                .SetBorder(Border.NO_BORDER)
                .SetBorderLeft(new SolidBorder(PdfStyleSettings.MealTextColor, 1))
                .SetBorderRight(new SolidBorder(PdfStyleSettings.MealTextColor, 1))
                .SetPaddings(10, 10, 2, 10);
            table.AddCell(dvHeaderCell);
        }

        private static void AddNutrientRows(Table table, Meal meal, double calories)
        {
            AddNutrientRow(table, "Total Fat", $"{meal.Fat:F0}g", $"{(((meal.Fat * 9) / calories) * 100):F0}%");
            AddNutrientRow(table, "Total Carbohydrate", $"{meal.Carbs:F0}g", $"{(((meal.Carbs * 4) / calories) * 100):F0}%");
            AddNutrientRow(table, "Protein", $"{meal.Protein:F0}g", $"{(((meal.Protein * 4) / calories) * 100):F0}%");
        }

        private static void AddNutrientRow(Table table, string nutrient, string amount, string dailyValue)
        {
            Table nutrientTable = new Table(UnitValue.CreatePercentArray(new float[] { 70f, 30f }));
            nutrientTable.SetWidth(UnitValue.CreatePercentValue(100));

            Cell nutrientCell = new Cell()
                .Add(new Paragraph()
                    .Add(new Text(nutrient)
                        .SetFontSize(18)
                        .SetFont(PdfStyleSettings.TitleBoldFont))
                    .Add(new Text(" " + amount)
                        .SetFontSize(12)
                        .SetFont(PdfStyleSettings.TitleFont))
                    .SetFixedLeading(15))
                .SetPadding(0)
                .SetBorder(Border.NO_BORDER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);

            Cell dvCell = new Cell()
                .Add(new Paragraph(dailyValue ?? "")
                    .SetFontSize(10)
                    .SetFont(PdfStyleSettings.BodyFont)
                    .SetFixedLeading(12))
                .SetPadding(0)
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetVerticalAlignment(VerticalAlignment.BOTTOM);

            nutrientTable.AddCell(nutrientCell);
            nutrientTable.AddCell(dvCell);

            Cell container = new Cell()
                .Add(nutrientTable)
                .SetBorder(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(PdfStyleSettings.MealTextColor, 1))
                .SetBorderLeft(new SolidBorder(PdfStyleSettings.MealTextColor, 1))
                .SetBorderRight(new SolidBorder(PdfStyleSettings.MealTextColor, 1))
                .SetPadding(10);

            table.AddCell(container);
        }

        private static void AddNutritionFootnote(Table table)
        {
            Cell footnoteCell = new Cell()
                .Add(new Paragraph($"* The % Daily Value (DV) tells you how much a nutrient in a serving of this recipe contributes to your daily diet of {2350:N0} calories, which is your recommended daily caloric intake based on your profile.")
                    .SetFontSize(8)
                    .SetFont(PdfStyleSettings.BodyFont)
                    .SetFixedLeading(10))
                .SetBorder(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(PdfStyleSettings.MealTextColor, 1))
                .SetBorderLeft(new SolidBorder(PdfStyleSettings.MealTextColor, 1))
                .SetBorderRight(new SolidBorder(PdfStyleSettings.MealTextColor, 1))
                .SetPadding(10);
            table.AddCell(footnoteCell);
        }

        private static string GetHighlightedTitle(string title)
        {
            title = title.Trim();
            var tokens = title.Split(" ");
            var withIndex = Array.FindIndex(tokens, s => string.Equals(s, "with", StringComparison.OrdinalIgnoreCase));
            if (withIndex == -1)
            {
                withIndex = tokens.Length;
            }
            return string.Join(" ", tokens.Take(withIndex).ToArray());
        }

        private static string GetNonHighlightedTitle(string title)
        {
            title = title.Trim();
            var tokens = title.Split(" ");
            var withIndex = Array.FindIndex(tokens, s => string.Equals(s, "with", StringComparison.OrdinalIgnoreCase));
            if (withIndex == -1)
            {
                withIndex = tokens.Length;
            }
            return string.Join(" ", tokens.Skip(withIndex).Take(tokens.Length - withIndex).ToArray());
        }
    }
}