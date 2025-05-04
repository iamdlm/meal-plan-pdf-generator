using iText.IO.Font.Constants;
using iText.Kernel.Font;
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
        public static void Write(Document doc, List<ShoppingList> shoppingList)
        {
            doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

            // Add the shopping list title
            PdfHeaderFormatter.AddHeader(doc, "Shopping List");

            // Filter out empty categories
            var nonEmptyCategories = shoppingList
                .Where(category => category.Items.Any())
                .Select(category => new
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
                .GroupBy(x => x.Index / 3)
                .Select(g => g.Select(x => x.Item).ToList())
                .ToList();

            foreach (var group in groupedCategories)
            {
                // Create a table with 3 columns
                Table table = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();

                // Remove all borders
                table.SetBorder(Border.NO_BORDER);

                // Add table headers (category names)
                for (int i = 0; i < 3; i++)
                {
                    if (i < group.Count)
                    {
                        table.AddHeaderCell(new Cell().Add(
                            new Paragraph(group[i].Category)
                                .SetFont(PdfStyleSettings.HeadingBoldFont).SetFontSize(14))
                                .SetBorder(Border.NO_BORDER));
                    }
                    else
                    {
                        table.AddHeaderCell(new Cell().Add(new Paragraph("")).SetBorder(Border.NO_BORDER));
                    }
                }

                // Find the maximum number of items in any category in this group
                int maxItems = group.Max(category => category.Items.Count);

                // Add table rows
                for (int i = 0; i < maxItems; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (j < group.Count && i < group[j].Items.Count)
                        {
                            var item = group[j].Items[i];

                            // Add checkbox
                            var checkbox = new Text("o")
                                .SetFontSize(12)
                                .SetFont(PdfFontFactory.CreateFont(StandardFonts.ZAPFDINGBATS));

                            var formattedQuantity = FormatQuantity(item.Quantity);
                            table.AddCell(
                                new Cell().Add(
                                    new Paragraph(checkbox).Add($" {formattedQuantity} {item.Unit} {item.Name}"))
                                        .SetFont(PdfStyleSettings.TextFont)
                                        .SetBorder(Border.NO_BORDER));
                        }
                        else
                        {
                            table.AddCell(
                                new Cell().Add(
                                    new Paragraph(""))
                                        .SetBorder(Border.NO_BORDER)); // Empty cell for alignment
                        }
                    }
                }

                // Add the table to the document
                doc.Add(table);

                // Add some space between tables
                doc.Add(new Paragraph("\n"));
            }
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
    }
} 