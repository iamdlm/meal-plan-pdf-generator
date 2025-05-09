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
        public static readonly Color MealTextColor = new DeviceRgb(182, 161, 114);
        public static readonly Color MealDividerTextColor = new DeviceRgb(223, 214, 195);
        public static readonly Color RecipeHeaderColor = new DeviceRgb(233, 227, 214);
        public static readonly Color TableColor = new DeviceRgb(247, 245, 241);
        public static readonly Color ShoppingHeaderCellColor = new DeviceRgb(205, 191, 161);
        public static readonly Color ShoppingCellColor = new DeviceRgb(241, 236, 227);
        public static readonly Color TextWhite = new DeviceRgb(255, 255, 255);

        // Font Sizes
        public static readonly int HeaderFontSize = 24;

        // Font Paths
        private static readonly string HeadingFontPath = Path.Combine("wwwroot", "fonts", "EBGaramond", "EBGaramond-Regular.ttf");
        private static readonly string HeadingBoldFontPath = Path.Combine("wwwroot", "fonts", "EBGaramond", "EBGaramond-Bold.ttf");
        private static readonly string TextFontPath = Path.Combine("wwwroot", "fonts", "Colaborate", "ColabReg.otf");
        private static readonly string TextBoldFontPath = Path.Combine("wwwroot", "fonts", "Colaborate", "ColabBol.otf");
        private static readonly string TitleFontPath = Path.Combine("wwwroot", "fonts", "BackwardsSans", "BackwardsSansRegular.otf");
        private static readonly string TitleBoldFontPath = Path.Combine("wwwroot", "fonts", "BackwardsSans", "BackwardsSansBold.otf");
        private static readonly string BodyFontPath = Path.Combine("wwwroot", "fonts", "Chocolates", "ChocolatesRegular.otf");
        private static readonly string BodyBoldFontPath = Path.Combine("wwwroot", "fonts", "Chocolates", "ChocolatesBold.otf");
        private static readonly string BodyBoldItalicFontPath = Path.Combine("wwwroot", "fonts", "Chocolates", "ChocolatesBoldItalic.otf");
        private static readonly string BodyExtraBoldItalicFontPath = Path.Combine("wwwroot", "fonts", "Chocolates", "ChocolatesExtraBoldItalic.otf");

        // Fonts
        public static readonly PdfFont HeadingFont = PdfFontFactory.CreateFont(HeadingFontPath, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
        public static readonly PdfFont HeadingBoldFont = PdfFontFactory.CreateFont(HeadingBoldFontPath, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
        public static readonly PdfFont TextFont = PdfFontFactory.CreateFont(TextFontPath, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
        public static readonly PdfFont TextBoldFont = PdfFontFactory.CreateFont(TextBoldFontPath, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
        public static readonly PdfFont TitleFont = PdfFontFactory.CreateFont(TitleFontPath, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
        public static readonly PdfFont TitleBoldFont = PdfFontFactory.CreateFont(TitleBoldFontPath, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
        public static readonly PdfFont BodyFont = PdfFontFactory.CreateFont(BodyFontPath, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
        public static readonly PdfFont BodyBoldFont = PdfFontFactory.CreateFont(BodyBoldFontPath, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
        public static readonly PdfFont BodyBoldItalicFont = PdfFontFactory.CreateFont(BodyBoldItalicFontPath, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
        public static readonly PdfFont BodyExtraBoldItalicFont = PdfFontFactory.CreateFont(BodyExtraBoldItalicFontPath, PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
    }
}