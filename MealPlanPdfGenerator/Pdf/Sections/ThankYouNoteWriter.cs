using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MealPlanPdfGenerator.Pdf.Core;

namespace MealPlanPdfGenerator.Pdf.Sections
{
    public static class ThankYouNoteWriter
    {
        public static void Write(Document doc, string code)
        {
            int margin = 75;

            doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

            // Add the thank you note
            doc.Add(new Paragraph("Thank you for choosing our personalized 7-day meal plan for eosinophilic esophagitis.")
                .SetFont(PdfStyleSettings.TextBoldFont)
                .SetMarginTop(200)
                .SetMarginLeft(margin)
                .SetMarginRight(margin)
                .SetTextAlignment(TextAlignment.CENTER));

            doc.Add(new Paragraph("We truly hope this guide brings ease and comfort to your dietary journey and helps you achieve your wellness goals. It's designed to support you step by step, making your meal choices more manageable and effective.")
                .SetFont(PdfStyleSettings.TextFont)
                .SetMarginTop(25)
                .SetMarginLeft(margin)
                .SetMarginRight(margin)
                .SetTextAlignment(TextAlignment.CENTER));

            doc.Add(new Paragraph("Before you start, don't forget—there's a special discount code included in this plan just for you! Use it for future meal plans, and we hope to welcome you back soon to explore more tailored options.")
                .SetFont(PdfStyleSettings.TextFont)
                .SetMarginTop(25)
                .SetMarginLeft(margin)
                .SetMarginRight(margin)
                .SetTextAlignment(TextAlignment.CENTER));

            doc.Add(new Paragraph("Thank you again, and we wish you continued success!")
                .SetFont(PdfStyleSettings.TextFont)
                .SetMarginLeft(margin)
                .SetMarginRight(margin)
                .SetTextAlignment(TextAlignment.CENTER));

            doc.Add(new Paragraph("CODE:")
                .SetFont(PdfStyleSettings.TextBoldFont)
                .SetMarginTop(25)
                .SetTextAlignment(TextAlignment.CENTER));

            doc.Add(new Paragraph(code)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER));
        }
    }
} 