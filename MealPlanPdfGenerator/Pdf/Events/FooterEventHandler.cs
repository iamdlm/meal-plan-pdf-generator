using iText.Kernel.Colors;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using MealPlanPdfGenerator.Pdf.Core;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Events;

namespace MealPlanPdfGenerator.Pdf.Events
{
    public class FooterEventHandler : IEventHandler
    {
        private readonly PdfFont _font;

        public FooterEventHandler()
        {
            _font = PdfStyleSettings.TextFont;
        }

        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();
            int pageNumber = pdfDoc.GetPageNumber(page);

            // Skip footer on first page (cover)
            if (pageNumber == 1)
                return;

            Rectangle pageSize = page.GetPageSize();
            PdfCanvas canvas = new PdfCanvas(page);
            
            // Footer settings
            float footerY = 20;
            float fontSize = 9;
            float margin = 30;

            canvas.BeginText()
                .SetFontAndSize(_font, fontSize)
                .SetColor(ColorConstants.DARK_GRAY, true);

            // Add website URL on the left
            string websiteUrl = PdfStyleSettings.WebsiteUrl;
            float urlWidth = _font.GetWidth(websiteUrl, fontSize);
            canvas.SetTextMatrix(margin, footerY)
                .ShowText(websiteUrl);

            // Create clickable link annotation for the URL
            string fullUrl = "https://" + websiteUrl.ToLower();
            var linkAnnotation = new PdfLinkAnnotation(
                new Rectangle(margin, footerY - 2, urlWidth, fontSize + 4));
            linkAnnotation.SetAction(PdfAction.CreateURI(fullUrl));
            linkAnnotation.SetBorder(new PdfArray(new float[] { 0, 0, 0 })); // No visible border
            
            page.AddAnnotation(linkAnnotation);

            // Add page number on the right
            string pageText = $"{pageNumber:D2}";
            float textWidth = _font.GetWidth(pageText, fontSize);
            float rightX = pageSize.GetWidth() - margin - textWidth;

            canvas.SetTextMatrix(rightX, footerY)
                .ShowText(pageText)
                .EndText();
        }
    }
}
