using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace MealPlanPdfGenerator.Pdf.Core
{
    public static class PdfHeaderFormatter
    {
        public static void AddHeader(Document doc, string title)
        {
            // Create a simple, prominent header with the title centered at the top of the page
            Paragraph header = new Paragraph(title)
                .SetFont(PdfStyleSettings.HeadingBoldFont)
                .SetFontSize(34) // Larger font size for prominence
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginTop(10)
                .SetMarginBottom(20);

            doc.Add(header);
        }
    }
} 