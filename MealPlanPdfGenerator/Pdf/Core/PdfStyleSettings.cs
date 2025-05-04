using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Font;

namespace MealPlanPdfGenerator.Pdf.Core
{
    public static class PdfStyleSettings
    {
        // Website URL
        public static readonly string WebsiteUrl = "WWW.EOEDIETPLANNER.COM";

        // Colors
        public static readonly Color HeaderBackground = new DeviceRgb(40, 40, 40);
        public static readonly Color TextGray = new DeviceRgb(128, 128, 128);
        public static readonly Color ContentBackground = new DeviceRgb(245, 245, 245);
        public static readonly Color VegetarianBackground = new DeviceRgb(247, 246, 234);
        public static readonly Color PrepTimeBackground = new DeviceRgb(236, 242, 242);
        public static readonly Color CookTimeBackground = new DeviceRgb(247, 237, 235);
        public static readonly Color ServesBackground = new DeviceRgb(242, 242, 242);

        // Font Sizes
        public static readonly int HeaderFontSize = 24;

        // Font Paths
        private static readonly string HeadingFontPath = Path.Combine("wwwroot", "fonts", "EBGaramond", "EBGaramond-Regular.ttf");
        private static readonly string HeadingBoldFontPath = Path.Combine("wwwroot", "fonts", "EBGaramond", "EBGaramond-Bold.ttf");
        private static readonly string TextFontPath = Path.Combine("wwwroot", "fonts", "Colaborate", "ColabReg.otf");
        private static readonly string TextBoldFontPath = Path.Combine("wwwroot", "fonts", "Colaborate", "ColabBol.otf");

        // Fonts
        public static readonly PdfFont HeadingFont = PdfFontFactory.CreateFont(HeadingFontPath, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
        public static readonly PdfFont HeadingBoldFont = PdfFontFactory.CreateFont(HeadingBoldFontPath, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
        public static readonly PdfFont TextFont = PdfFontFactory.CreateFont(TextFontPath, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
        public static readonly PdfFont TextBoldFont = PdfFontFactory.CreateFont(TextBoldFontPath, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
    }
} 