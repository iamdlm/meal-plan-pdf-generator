using iText.IO.Image;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MealPlanPdfGenerator.Pdf.Core;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Font;

namespace MealPlanPdfGenerator.Pdf.Sections
{
    public static class CoverWriter
    {
        public static void Write(Document doc)
        {
            int fontSize = 40;

            // Add the title
            doc.Add(new Paragraph("7-day meal plan for")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(PdfStyleSettings.HeadingBoldFont)
                .SetFontSize(fontSize)
                .SetMarginTop(20));

            doc.Add(new Paragraph("eosinophilic esophagitis")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(PdfStyleSettings.HeadingBoldFont)
                .SetFontSize(fontSize)
                .SetMarginTop(-20));

            // Add the image
            string imagePath = System.IO.Path.Combine("wwwroot", "images", "ebook", "white-plate-with-food.png");

            // Load and add the image, centering it
            Image img = new Image(ImageDataFactory.Create(imagePath))
                .SetMarginTop(115)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetHeight(300);

            doc.Add(img);

            // Add the footer
            doc.Add(new Paragraph("Expert-Backed Nutrition Powered by AI Insights")
                .SetFont(PdfStyleSettings.TextFont)
                .SetFontSize(16)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetMarginTop(120));

            // Get the current page
            PdfPage page = doc.GetPdfDocument().GetLastPage();
            
            // Website URL settings
            string websiteUrl = PdfStyleSettings.WebsiteUrl;
            float fontSize2 = 10;
            PdfFont font = PdfStyleSettings.TextFont;
            
            // Add website URL as a paragraph
            Paragraph websiteParagraph = new Paragraph(websiteUrl)
                .SetFont(font)
                .SetFontSize(fontSize2)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetMarginTop(0);
            
            doc.Add(websiteParagraph);
            
            // Calculate text width and position for the link annotation
            float urlWidth = font.GetWidth(websiteUrl, fontSize2);
            Rectangle pageSize = page.GetPageSize();
            
            // Center position calculation
            float xCenter = pageSize.GetWidth() / 2;
            float yPosition = 70; // Approximate Y position of the website URL
            float leftX = xCenter - (urlWidth / 2);
            
            // Create clickable link annotation for the URL
            string fullUrl = "https://" + websiteUrl.ToLower();
            var linkAnnotation = new PdfLinkAnnotation(
                new Rectangle(leftX, yPosition - 2, urlWidth, fontSize2 + 4));
            linkAnnotation.SetAction(PdfAction.CreateURI(fullUrl));
            linkAnnotation.SetBorder(new PdfArray(new float[] { 0, 0, 0 })); // No visible border
            
            page.AddAnnotation(linkAnnotation);
        }
    }
} 