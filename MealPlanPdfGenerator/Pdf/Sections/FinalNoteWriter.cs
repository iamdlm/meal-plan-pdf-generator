using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace MealPlanPdfGenerator.Pdf.Sections
{
    public static class FinalNoteWriter
    {
        public static void Write(Document doc, string code)
        {
            int margin = 75;

            doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

            // Add the thank you note
            doc.Add(new Paragraph("Final Note")
                .SetBold()
                .SetMarginTop(200)
                .SetMarginLeft(margin)
                .SetMarginRight(margin)
                .SetTextAlignment(TextAlignment.CENTER));

            doc.Add(new Paragraph("Thank you for trusting this meal plan in your journey to manage eosinophilic esophagitis. We understand that adjusting your diet can be challenging, but with the right guidance, it is possible to reduce symptoms and improve your overall quality of life. Remember to be patient with yourself as you navigate this new lifestyle, and know that we are here to support you every step of the way.")
                .SetMarginTop(25)
                .SetMarginLeft(margin)
                .SetMarginRight(margin)
                .SetTextAlignment(TextAlignment.CENTER));

            doc.Add(new Paragraph("Don't forget, you can use the code provided below to get your next 7-day meal plan at a discounted rate. We'd be delighted to continue helping you maintain your health while managing EoE.")
                .SetMarginTop(25)
                .SetMarginLeft(margin)
                .SetMarginRight(margin)
                .SetTextAlignment(TextAlignment.CENTER));

            doc.Add(new Paragraph("We wish you continued success on your path to wellness!")
                .SetMarginLeft(margin)
                .SetMarginRight(margin)
                .SetTextAlignment(TextAlignment.CENTER));

            doc.Add(new Paragraph("CODE:")
                .SetMarginTop(25)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER));
            doc.Add(new Paragraph(code)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER));
        }
    }
} 