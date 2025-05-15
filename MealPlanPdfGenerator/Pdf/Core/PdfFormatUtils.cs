using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace MealPlanPdfGenerator.Pdf.Core
{
    public static class PdfFormatUtils
    {
        public static string FormatQuantity(double quantity)
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

        public static Table CreateStandardTable(int columns, bool useAllWidth = true)
        {
            var table = new Table(columns);
            if (useAllWidth)
            {
                table.UseAllAvailableWidth();
            }
            table.SetBorder(new SolidBorder(1));
            return table;
        }

        public static Cell CreateStandardCell(string text, bool isBold = false, TextAlignment alignment = TextAlignment.LEFT)
        {
            var cell = new Cell()
                .Add(new Paragraph(text))
                .SetBorder(new SolidBorder(1))
                .SetPadding(5)
                .SetTextAlignment(alignment);

            if (isBold)
            {
                cell.SetFont(PdfStyleSettings.TextBoldFont);
            }

            return cell;
        }

        public static Cell CreateHeaderCell(string text, TextAlignment alignment = TextAlignment.CENTER)
        {
            return new Cell()
                .Add(new Paragraph(text))
                .SetFont(PdfStyleSettings.HeadingBoldFont)
                .SetFontSize(14)
                .SetBackgroundColor(PdfStyleSettings.HeaderBackground)
                .SetFontColor(ColorConstants.WHITE)
                .SetBorder(new SolidBorder(1))
                .SetPadding(5)
                .SetTextAlignment(alignment);
        }

        public static void AddSectionBreak(Document doc)
        {
            doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
        }

        public static string FormatWithThousandSeparator(int value)
        {
            return value.ToString("N0");
        }

        public static int CalculateTotalLine(string text, int fontSize, int spacing, float containerWidth)
        {
            var tokens = text.Split(" ");
            float textWidth = 0;
            int counter = 0;
            int totalLine = 1;
            foreach (var token in tokens)
            {
                if (counter > 0)
                {
                    textWidth += spacing;
                }
                var tokenLength = token.Length * fontSize * 0.5f;
                if (textWidth + tokenLength > containerWidth)
                {
                    totalLine++;
                    textWidth = tokenLength;
                }
                else
                {
                    textWidth += tokenLength;
                }
                counter++;
            }
            return totalLine;
        }
    }
}