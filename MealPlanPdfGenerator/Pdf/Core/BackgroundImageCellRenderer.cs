using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout.Element;
using iText.Layout.Renderer;

namespace MealPlanPdfGenerator.Pdf.Core
{
    public class BackgroundImageCellRenderer : CellRenderer
    {
        private ImageData imageData;

        public BackgroundImageCellRenderer(Cell modelElement, ImageData imageData)
            : base(modelElement)
        {
            this.imageData = imageData;
        }

        public override void Draw(DrawContext drawContext)
        {
            PdfCanvas canvas = drawContext.GetCanvas();
            Rectangle rect = GetOccupiedAreaBBox();
            canvas.SaveState();

            // Draw image as background
            canvas.AddXObjectFittedIntoRectangle(new PdfImageXObject(imageData), rect);
            canvas.RestoreState();

            // Draw content (text, etc.)
            base.Draw(drawContext);
        }
    }
}
