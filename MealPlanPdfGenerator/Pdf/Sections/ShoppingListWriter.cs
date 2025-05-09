using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using MealPlanPdfGenerator.Models;
using MealPlanPdfGenerator.Pdf.Core;

namespace MealPlanPdfGenerator.Pdf.Sections
{
    public static class ShoppingListWriter
    {
        private static readonly int TotalColumn = 3;

        public static void Write(Document doc, List<ShoppingList> shoppingList)
        {
            // Add the shopping list title
            AddTitle(doc);

            // Filter out empty categories
            var nonEmptyCategories = shoppingList
                .Where(category => category.Items.Any())
                .Select(category => new ShoppingListPerCategory
                {
                    Category = category.Category,
                    // Sort items by the full string length
                    Items = category.Items
                        .Select(item => new
                        {
                            Item = item,
                            FullText = $" {item.Quantity} {item.Unit} {item.Name}"
                        })
                        .OrderBy(x => x.FullText.Length)
                        .Select(x => x.Item)
                        .ToList()
                })
                .ToList();

            // Group categories into sets of three and order by number of items
            var groupedCategories = nonEmptyCategories
                .OrderByDescending(category => category.Items.Count)
                .Select((item, index) => new { Item = item, Index = index })
                .GroupBy(x => x.Index / TotalColumn)
                .Select(g => g.Select(x => x.Item).ToList())
                .ToList();

            // Add the shopping list 
            AddShoppingList(doc, groupedCategories);

            PdfFormatUtils.AddSectionBreak(doc);
        }

        private static void AddTitle(Document doc)
        {
            Paragraph header = new Paragraph()
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(54)
                .SetFixedLeading(54)
                .SetMarginBottom(30);

            header.Add(new Text("SHOPPING").SetFont(PdfStyleSettings.TitleBoldFont));
            header.Add(new Text($" LIST").SetFont(PdfStyleSettings.TitleFont));

            doc.Add(header);
        }

        private static void AddShoppingList(Document doc, List<List<ShoppingListPerCategory>> groupedCategories)
        {
            int rowNumber = 0;
            foreach (var group in groupedCategories)
            {
                // Create a table with 3 columns
                Table table = new Table(UnitValue.CreatePercentArray(TotalColumn)).UseAllAvailableWidth();

                // Remove all borders
                table.SetBorder(Border.NO_BORDER);

                int maxItems = group.Max(category => category.Items.Count);

                // Add table headers (category names)                
                for (int column = 0; column < TotalColumn; column++)
                {
                    if (column < group.Count)
                    {
                        var shoppingListPerCategory = group[column];

                        AddShoppingListPerCategoryTable(table, shoppingListPerCategory, column, maxItems);
                    }
                    else
                    {
                        // add empty cell for row that has column less than 3
                        table.AddCell(new Cell().Add(new Paragraph("")).SetBorder(Border.NO_BORDER));
                    }
                }

                // Add the table to the document
                doc.Add(table);

                // Add some space between tables
                doc.Add(new Paragraph("\n"));

                rowNumber++;
            }
        }

        private static void AddShoppingListPerCategoryTable(Table table, ShoppingListPerCategory shoppingListPerCategory, int column, int maxRows)
        {
            Table categoryTable = new Table(1).UseAllAvailableWidth();

            categoryTable.AddHeaderCell(new Cell().Add(
                new Paragraph(shoppingListPerCategory.Category.ToUpper())
                    .SetFont(PdfStyleSettings.TitleFont)
                    .SetFontSize(14)
                    .SetFontColor(ColorConstants.WHITE))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBackgroundColor(PdfStyleSettings.ShoppingHeaderCellColor));

            for (int j = 0; j < maxRows; j++)
            {
                if (j < shoppingListPerCategory.Items.Count)
                {
                    var item = shoppingListPerCategory.Items[j];

                    Table itemTable = new Table(UnitValue.CreatePercentArray(new float[] { 10f, 88f, 5f })).UseAllAvailableWidth();

                    // Add checkbox
                    Div checkbox = new Div();
                    checkbox.SetWidth(10);
                    checkbox.SetHeight(10);
                    checkbox.SetBackgroundColor(ColorConstants.WHITE);

                    itemTable.AddCell(new Cell().Add(checkbox)
                        .SetBackgroundColor(PdfStyleSettings.ShoppingCellColor)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                        .SetBorder(Border.NO_BORDER)
                        .SetPaddings(2, 5, 0, 5));

                    var formattedQuantity = FormatQuantity(item.Quantity);
                    var quantityCell = new Cell().Add(new Paragraph()
                            .Add($" {formattedQuantity} {item.Unit} {item.Name}")
                            .SetFont(PdfStyleSettings.TitleFont)
                            .SetFontSize(8))
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .SetBorder(Border.NO_BORDER)
                        .SetBackgroundColor(PdfStyleSettings.ShoppingCellColor);

                    if (j < maxRows - 1) // dont draw border in last item
                    {
                        quantityCell.SetBorderBottom(new SolidBorder(ColorConstants.WHITE, 1));
                    }

                    itemTable.AddCell(quantityCell);

                    itemTable.AddCell(new Cell()
                        .SetBackgroundColor(PdfStyleSettings.ShoppingCellColor)
                        .SetBorder(Border.NO_BORDER));

                    categoryTable.AddCell(new Cell()
                        .Add(itemTable)
                        .SetBackgroundColor(PdfStyleSettings.ShoppingCellColor)
                        .SetPadding(0)
                        .SetBorder(Border.NO_BORDER));
                }
                else
                {
                    categoryTable.AddCell(new Cell()
                            .SetHeight(17)
                            .SetBackgroundColor(PdfStyleSettings.ShoppingCellColor)
                            .SetBorder(Border.NO_BORDER));

                }
            }

            table.AddCell(new Cell()
                .Add(categoryTable)
                .SetBorder(Border.NO_BORDER)
                .SetPaddings(0, 0, 0, column > 0 ? 20 : 0));
        }

        private static string FormatQuantity(double quantity)
        {
            // Handle whole numbers
            if (quantity == Math.Floor(quantity))
            {
                return quantity.ToString("0");
            }

            // Split into whole number and fraction parts
            int wholeNumber = (int)Math.Floor(quantity);
            double fraction = quantity - wholeNumber;

            // Convert common decimal values to fractions
            string fractionStr = fraction switch
            {
                0.25 => "¼",
                0.5 => "½",
                0.75 => "¾",
                0.33 or 0.333 => "⅓",
                0.66 or 0.667 => "⅔",
                0.125 => "⅛",
                0.375 => "⅜",
                0.625 => "⅝",
                0.875 => "⅞",
                _ => fraction.ToString(".00").TrimEnd('0').TrimEnd('.') // Fallback to decimal
            };

            // Combine whole number and fraction if needed
            return wholeNumber > 0
                ? $"{wholeNumber} {fractionStr}"
                : fractionStr;
        }

        private class ShoppingListPerCategory
        {
            public required string Category { get; set; }

            public required List<ShoppingListItem> Items { get; set; }
        }
    }
}