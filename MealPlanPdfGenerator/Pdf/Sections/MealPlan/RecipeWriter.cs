using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
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
            // Create main content container
            float[] columnWidths = { 1, 1 };
            Table mainContent = new Table(UnitValue.CreatePercentArray(columnWidths));
            mainContent.SetWidth(UnitValue.CreatePercentValue(100));

            // Left column content
            Cell leftColumn = new Cell();
            leftColumn.SetBorder(Border.NO_BORDER);

            // Add ingredients
            AddIngredients(leftColumn, meal);

            // Add preparation steps
            AddPreparationSteps(leftColumn, meal);

            mainContent.AddCell(leftColumn);

            // Right column content
            Cell rightColumn = new Cell();
            rightColumn.SetBorder(Border.NO_BORDER);

            // Add recipe image
            if (!string.IsNullOrEmpty(meal.Image))
            {
                byte[] imageBytes = Convert.FromBase64String(meal.Image);
                ImageData imageData = ImageDataFactory.Create(imageBytes);
                Image image = new Image(imageData);
                image.SetWidth(UnitValue.CreatePercentValue(100));
                rightColumn.Add(image);
            }

            // Add nutrition facts table
            AddNutritionFacts(rightColumn, meal, dayCalories);

            mainContent.AddCell(rightColumn);

            // Add recipe information header
            AddRecipeInformation(pdfDoc, doc, meal);

            // Add the main content to the document
            doc.Add(mainContent);
        }

        private static void AddIngredients(Cell container, Meal meal)
        {
            Paragraph header = new Paragraph("Ingredients")
                .SetFontSize(20)
                .SetBold()
                .SetMarginBottom(10);
            container.Add(header);

            List ingredients = new List()
                .SetListSymbol("•")
                .SetSymbolIndent(12)
                .SetMarginBottom(20);

            foreach (var ingredient in meal.Ingredients)
            {
                var formattedQuantity = PdfFormatUtils.FormatQuantity(ingredient.Quantity);
                string ingredientText = $"{formattedQuantity} {ingredient.Unit} {ingredient.Name}";
                ingredients.Add(new ListItem(ingredientText));
            }

            container.Add(ingredients);
        }

        private static void AddPreparationSteps(Cell container, Meal meal)
        {
            Paragraph header = new Paragraph("Instructions")
                .SetFontSize(20)
                .SetBold()
                .SetMarginBottom(10);
            container.Add(header);

            List steps = new List(ListNumberingType.DECIMAL)
                .SetSymbolIndent(3)
                .SetMarginBottom(20);

            foreach (var instruction in meal.Preparation.OrderBy(p => p.Index))
            {
                steps.Add(new ListItem(instruction.Description));
            }

            container.Add(steps);
        }

        private static void AddRecipeInformation(PdfDocument pdfDoc, Document doc, Meal meal)
        {
            // Create a table with 3 equal columns
            Table infoTable = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 1 }));
            infoTable.SetWidth(UnitValue.CreatePercentValue(100));

            // Add thin black borders on top and bottom only
            infoTable.SetBorder(Border.NO_BORDER);
            infoTable.SetBorderTop(new SolidBorder(ColorConstants.BLACK, 1f));
            infoTable.SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1f));

            // Add margins
            infoTable.SetMarginBottom(40);

            AddInfoCell(pdfDoc, infoTable, "prep.svg", "Preparation Time", $"{meal.PrepTime} min");
            AddInfoCell(pdfDoc, infoTable, "cook.svg", "Cook Time", $"{meal.CookTime} min");
            AddInfoCell(pdfDoc, infoTable, "difficulty.svg", "Difficulty", meal.DifficultyLevel.ToString());

            // Add the table to the document
            doc.Add(infoTable);
        }

        private static void AddInfoCell(PdfDocument pdfDoc, Table table, string svgFileName, string label, string value)
        {
            Cell cell = new Cell();
            cell.SetBorder(Border.NO_BORDER);
            cell.SetPadding(10);
            cell.SetTextAlignment(TextAlignment.CENTER);
            cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);

            // Add SVG icon
            byte[] svgBytes = File.ReadAllBytes(Path.Combine("wwwroot", "svg", svgFileName));
            MemoryStream svgStream = new MemoryStream(svgBytes);
            Image svgImage = SvgConverter.ConvertToImage(svgStream, pdfDoc);
            svgImage.SetHeight(20);
            svgImage.SetWidth(20);
            svgImage.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            // Create separate paragraphs for each component
            Paragraph iconPara = new Paragraph().Add(svgImage);
            iconPara.SetTextAlignment(TextAlignment.CENTER);
            iconPara.SetFixedLeading(24);

            Paragraph labelPara = new Paragraph(label).SetFontSize(10);
            labelPara.SetTextAlignment(TextAlignment.CENTER);
            labelPara.SetFixedLeading(14);

            Paragraph valuePara = new Paragraph(value).SetFontSize(12).SetBold();
            valuePara.SetTextAlignment(TextAlignment.CENTER);
            valuePara.SetFixedLeading(16);

            cell.Add(iconPara);
            cell.Add(labelPara);
            cell.Add(valuePara);

            table.AddCell(cell);
        }

        private static void AddNutritionFacts(Cell container, Meal meal, double calories)
        {
            // Create main table with single column
            Table nutritionTable = new Table(1);
            nutritionTable
                .SetWidth(UnitValue.CreatePercentValue(100))
                .SetBorder(new SolidBorder(ColorConstants.BLACK, 1))
                .SetMarginTop(40);

            // Add "Nutrition Facts" header
            Cell headerCell = new Cell()
                .Add(new Paragraph("Nutrition Facts")
                    .SetFontSize(16)
                    .SetBold()
                    .SetFontColor(ColorConstants.BLACK))
                .SetBorder(Border.NO_BORDER)
                .SetPadding(5);
            nutritionTable.AddCell(headerCell);

            // Add thick border
            Cell thickBorder = new Cell()
                .SetBorder(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 2))
                .SetHeight(0);
            nutritionTable.AddCell(thickBorder);

            // Add "Amount per serving" row
            Cell servingCell = new Cell()
                .Add(new Paragraph("Amount per serving")
                    .SetFontSize(10)
                    .SetFontColor(ColorConstants.BLACK))
                .SetBorder(Border.NO_BORDER)
                .SetPadding(2);
            nutritionTable.AddCell(servingCell);

            AddCaloriesRow(nutritionTable, meal.Calories);
            AddDailyValueHeader(nutritionTable);
            AddNutrientRows(nutritionTable, meal, calories);
            AddNutritionFootnote(nutritionTable);

            container.Add(nutritionTable);
        }

        private static void AddCaloriesRow(Table table, double calories)
        {
            Table caloriesTable = new Table(2);
            caloriesTable.SetWidth(UnitValue.CreatePercentValue(100));

            Cell labelCell = new Cell()
                .Add(new Paragraph("Calories")
                    .SetFontSize(14)
                    .SetBold())
                .SetBorder(Border.NO_BORDER);

            Cell valueCell = new Cell()
                .Add(new Paragraph($"{calories:F0}")
                    .SetFontSize(14)
                    .SetBold())
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.RIGHT);

            caloriesTable.AddCell(labelCell);
            caloriesTable.AddCell(valueCell);

            Cell container = new Cell()
                .Add(caloriesTable)
                .SetBorder(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1));
            table.AddCell(container);
        }

        private static void AddDailyValueHeader(Table table)
        {
            Cell dvHeaderCell = new Cell()
                .Add(new Paragraph("% Daily Value*")
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetFontSize(10))
                .SetBorder(Border.NO_BORDER)
                .SetPadding(2);
            table.AddCell(dvHeaderCell);
        }

        private static void AddNutrientRows(Table table, Meal meal, double calories)
        {
            AddNutrientRow(table, "Protein", $"{meal.Protein:F0}g", $"{(((meal.Protein * 4) / calories) * 100):F0}%");
            AddNutrientRow(table, "Total Fat", $"{meal.Fat:F0}g", $"{(((meal.Fat * 9) / calories) * 100):F0}%");
            AddNutrientRow(table, "Total Carbohydrate", $"{meal.Carbs:F0}g", $"{(((meal.Carbs * 4) / calories) * 100):F0}%");
        }

        private static void AddNutrientRow(Table table, string nutrient, string amount, string dailyValue)
        {
            Table nutrientTable = new Table(UnitValue.CreatePercentArray(new float[] { 60f, 40f }));
            nutrientTable.SetWidth(UnitValue.CreatePercentValue(100));

            Cell nutrientCell = new Cell()
                .Add(new Paragraph()
                    .Add(new Text(nutrient)
                        .SetFontSize(10)
                        .SetBold())
                    .Add(new Text(" " + amount)
                        .SetFontSize(10)))
                .SetBorder(Border.NO_BORDER);

            Cell dvCell = new Cell()
                .Add(new Paragraph(dailyValue ?? "")
                    .SetFontSize(10))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.RIGHT);

            nutrientTable.AddCell(nutrientCell);
            nutrientTable.AddCell(dvCell);

            Cell container = new Cell()
                .Add(nutrientTable)
                .SetBorder(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5f))
                .SetPadding(2);

            table.AddCell(container);
        }

        private static void AddNutritionFootnote(Table table)
        {
            Cell footnoteCell = new Cell()
                .Add(new Paragraph($"* The % Daily Value (DV) tells you how much a nutrient in a serving of this recipe contributes to your daily diet of {2350:N0} calories, which is your recommended daily caloric intake based on your profile.")
                    .SetFontSize(8)
                    .SetMarginTop(5))
                .SetBorder(Border.NO_BORDER)
                .SetPadding(5);
            table.AddCell(footnoteCell);
        }
    }
} 