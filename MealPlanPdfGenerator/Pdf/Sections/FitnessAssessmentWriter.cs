using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Svg.Converter;
using MealPlanPdfGenerator.Pdf.Core;
using MealPlanPdfGenerator.Pdf.ViewModels;

namespace MealPlanPdfGenerator.Pdf.Sections
{
    public static class FitnessAssessmentWriter
    {
        public static void Write(PdfDocument pdfDoc, Document doc, int age, double weight, double height, string activityLevel)
        {
            AddTitle(doc);

            // Create main content container
            float[] columnWidths = { 1, 0.25f, 1 };
            Table mainContent = new Table(UnitValue.CreatePercentArray(columnWidths));
            mainContent.SetWidth(UnitValue.CreatePercentValue(100));

            // Left column content
            Cell leftColumn = new Cell().SetBorder(Border.NO_BORDER);

            // Right column content
            Cell rightColumn = new Cell().SetBorder(Border.NO_BORDER);

            mainContent.AddCell(leftColumn);
            mainContent.AddCell(new Cell().SetBorder(Border.NO_BORDER));
            mainContent.AddCell(rightColumn);

            // Calculate necessary values
            double bmi = CalculateBMI(weight, height);
            int maintenanceCalories = CalculateMaintenanceCalories(age, weight, height, activityLevel);
            double[] idealWeightRange = CalculateIdealWeightRange(height);

            AddImc(pdfDoc, leftColumn, bmi);

            AddMaintenanceCalories(pdfDoc, leftColumn, maintenanceCalories);

            AddMacrosDistribution(pdfDoc, leftColumn);

            rightColumn.Add(CreateSubSectionParagraph()
                .Add("People who use this simple tool from Amazon are usually in significantly better shape versus people who don’t.")
                .SetMarginBottom(14));

            rightColumn.Add(CreateBMITable()
                .SetMarginBottom(14));

            rightColumn.Add(CreateSubSectionParagraph()
                .Add("The table below shows the difference if you were to\r\n have selected a different activity level.")
                .SetMarginBottom(14));

            rightColumn.Add(CreateActivityLevelTable(maintenanceCalories)
                .SetMarginBottom(14));

            doc.Add(mainContent);
        }

        private static void AddTitle(Document doc)
        {
            Paragraph mainTitle = new Paragraph("FITNESS ASSESSMENT")
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(40)
                .SetFont(PdfStyleSettings.TitleBoldFont)
                .SetFixedLeading(40);

            Paragraph subTitle = new Paragraph("Optimizing Physical Health While Managing EoE")
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(26)
                .SetFont(PdfStyleSettings.TitleFont)
                .SetFixedLeading(26)
                .SetMarginBottom(30);

            doc.Add(mainTitle);
            doc.Add(subTitle);
        }

        private static void AddImc(PdfDocument pdfDoc, Cell container, double bmi)
        {
            AddSubSectionHeader(container, "Your IMC");

            BmiClassification bmiClassification = GetBMIClassification(bmi);
            string bmiClassificationText = GetBmiClassificationText(bmiClassification);

            Paragraph paragraph = CreateSubSectionParagraph()
                .Add($"Your BMI is {bmi:F1}, which means you are classified as {bmiClassificationText}.");

            container.Add(paragraph);

            AddImcBmiIcon(pdfDoc, container, bmi);
        }

        private static void AddImcBmiIcon(PdfDocument pdfDoc, Cell container, double bmi)
        {
            BmiClassification bmiClassification = GetBMIClassification(bmi);
            string bmiClassificationText = GetBmiClassificationText(bmiClassification);
            byte[] svgBytes = File.ReadAllBytes(Path.Combine("wwwroot", "svg", "bmi-obese.svg"));
            MemoryStream svgStream = new MemoryStream(svgBytes);
            Image bmiImage = SvgConverter.ConvertToImage(svgStream, pdfDoc);

            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 30f, 5f, 65f })).UseAllAvailableWidth();

            Cell iconCell = new Cell()
                .SetBackgroundColor(PdfStyleSettings.RecipeHeaderColor)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetPaddingLeft(20)
                .SetBorder(Border.NO_BORDER);

            bmiImage.SetHeight(48)
                .SetTextAlignment(TextAlignment.CENTER);
            iconCell.Add(bmiImage);

            table.AddCell(iconCell);

            // separator column between bmi icon and bmi desc
            table.AddCell(new Cell().SetWidth(5).SetBorder(Border.NO_BORDER));

            Table bmiDescTable = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();

            bmiDescTable.AddCell(new Cell()
                .Add(new Paragraph($"{bmi:F1}")
                    .SetFont(PdfStyleSettings.BodyBoldFont)
                    .SetFontSize(14))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1)));

            bmiDescTable.AddCell(new Cell()
                .Add(new Paragraph(bmiClassificationText)
                    .SetFont(PdfStyleSettings.BodyBoldFont)
                    .SetFontSize(12))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER));

            Cell bmiDescCell = new Cell()
                .Add(bmiDescTable)
                .SetBackgroundColor(PdfStyleSettings.RecipeHeaderColor)
                .SetBorder(Border.NO_BORDER)
                .SetPaddings(5, 10, 5, 10);

            table.AddCell(bmiDescCell);

            Div iconDiv = new Div();
            iconDiv.Add(table)
                .SetPaddings(10, 20, 10, 20);

            container.Add(iconDiv);
        }

        private static void AddMaintenanceCalories(PdfDocument pdfDoc, Cell container, int maintenanceCalories)
        {
            int maintenanceCaloriesPerWeek = 7 * maintenanceCalories;

            AddSubSectionHeader(container, "Your Maintenance Calories");

            Paragraph paragraph = new Paragraph()
                .Add(new Text($"Based on your stats, the best estimate for your maintenance calories is"))
                .Add(new Text($" {maintenanceCalories.ToStringWithThousandSeparator()} calories").SetFont(PdfStyleSettings.BodyBoldFont))
                .Add(new Text(" per day,"))
                .Add(new Text(" based on the Katch-McArdle Formula").SetFont(PdfStyleSettings.BodyBoldFont))
                .Add(new Text(", which is widely known to be the most accurate when body fat is provided"))
                .SetFont(PdfStyleSettings.BodyFont)
                .SetFixedLeading(16)
                .SetMarginBottom(8);

            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 48f, 4, 48f })).UseAllAvailableWidth();

            Cell caloriesPerDayCell = new Cell()
                .Add(new Paragraph(maintenanceCalories.ToStringWithThousandSeparator())
                    .SetFont(PdfStyleSettings.TitleBoldFont)
                    .SetFontSize(14))
                .Add(new Paragraph("calories per day"))
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER)
                .SetPaddings(10, 5, 10, 5);

            caloriesPerDayCell.SetNextRenderer(new BackgroundImageCellRenderer(caloriesPerDayCell,
                ImageDataFactory.Create(Path.Combine("wwwroot", "images", "rect-left.png"))));

            table.AddCell(caloriesPerDayCell);

            // separator column between bmi icon and bmi desc
            table.AddCell(new Cell().SetWidth(10).SetBorder(Border.NO_BORDER));

            Cell caloriesPerWeekCell = new Cell()
                .Add(new Paragraph(maintenanceCaloriesPerWeek.ToStringWithThousandSeparator())
                    .SetFont(PdfStyleSettings.TitleBoldFont)
                    .SetFontSize(14))
                .Add(new Paragraph("calories per week"))
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER)
                .SetPaddings(10, 5, 10, 5);

            caloriesPerWeekCell.SetNextRenderer(new BackgroundImageCellRenderer(caloriesPerWeekCell,
                ImageDataFactory.Create(Path.Combine("wwwroot", "images", "rect-right.png"))));

            table.AddCell(caloriesPerWeekCell)
                .SetMarginBottom(10);

            container.Add(paragraph);
            container.Add(table);
        }

        private static void AddMacrosDistribution(PdfDocument pdfDoc, Cell container)
        {
            AddSubSectionHeader(container, "Your recommended macros distribution");

            Paragraph paragraph = CreateSubSectionParagraph()
                .Add("Ulamco laborum laboris duis ea laborum consectetur in aute incididunt ea proident. Do culpa qui magna dolore velit fugiat in eu laboris do occaecat.")
                .SetMarginBottom(10);

            container.Add(paragraph);

            var carb = new MacroDistributionViewModel { Min = 40, Max = 50 };
            var fat = new MacroDistributionViewModel { Min = 10, Max = 20 };
            var protein = new MacroDistributionViewModel { Min = 30, Max = 50 };
            var pieChartImageBytes = PdfDrawUtils.CreateMacrosDistributionPieChart(carb, fat, protein);

            Image pieChartImage = new Image(ImageDataFactory.Create(pieChartImageBytes));
            pieChartImage.SetAutoScale(true);

            container.Add(pieChartImage);
        }

        private static Table CreateActivityLevelTable(int maintenanceCalories)
        {
            Table table = new Table(2).UseAllAvailableWidth();
            table.SetBorder(new SolidBorder(1));
            table.AddCell(CreateDescHeaderCell("Activity Level"));
            table.AddCell(CreateValueHeaderCell("Calories per Day"));

            string[] levels = { "Basal Metabolic Rate", "Sedentary", "Light Exercise", "Moderate Exercise", "Heavy Exercise", "Athlete" };
            double[] multipliers = { 1.0, 1.2, 1.375, 1.55, 1.725, 1.9 };

            for (int i = 0; i < levels.Length; i++)
            {
                table.AddCell(CreateDescCell(levels[i]));

                int caloriesPerDay = (int)(maintenanceCalories * multipliers[i]);
                table.AddCell(CreateValueCell(caloriesPerDay.ToStringWithThousandSeparator()));
            }

            return table;
        }

        private static Table CreateBMITable()
        {
            Table table = new Table(UnitValue.CreatePointArray(new float[] { 100f, 100f, }));
            table.SetBorder(new SolidBorder(1));
            table.AddCell(CreateDescHeaderCell("BMI Range"));
            table.AddCell(CreateValueHeaderCell("Classification"));

            string[][] data =
            {
                new[] { "18.5 or less", "Underweight" },
                new[] { "18.5 - 24.99", "Normal Weight" },
                new[] { "25 - 29.99", "Overweight" },
                new[] { "30+", "Obese" }
            };

            foreach (var row in data)
            {
                table.AddCell(CreateDescCell(row[0]));
                table.AddCell(CreateValueCell(row[1]));
            }

            return table;
        }

        private static Table CreateMacronutrientTable(int calories)
        {
            Table table = new Table(4).UseAllAvailableWidth();
            table.SetBorder(new SolidBorder(1));
            table.AddCell(new Cell().Add(new Paragraph("Plan").SetBold()).SetBorder(new SolidBorder(1)));
            table.AddCell(new Cell().Add(new Paragraph("Protein").SetBold()).SetBorder(new SolidBorder(1)));
            table.AddCell(new Cell().Add(new Paragraph("Fats").SetBold()).SetBorder(new SolidBorder(1)));
            table.AddCell(new Cell().Add(new Paragraph("Carbs").SetBold()).SetBorder(new SolidBorder(1)));

            string[] plans = { "Moderate Carb (30/35/35)", "Lower Carb (40/40/20)", "Higher Carb (30/20/50)" };
            int[][] ratios = { new[] { 30, 35, 35 }, new[] { 40, 40, 20 }, new[] { 30, 20, 50 } };

            foreach (var (plan, ratio) in plans.Zip(ratios, (p, r) => (p, r)))
            {
                table.AddCell(new Cell().Add(new Paragraph(plan)).SetBorder(new SolidBorder(1)));
                for (int i = 0; i < 3; i++)
                {
                    int grams = (int)((calories * ratio[i] / 100.0) / (i == 0 ? 4 : i == 1 ? 9 : 4));
                    table.AddCell(new Cell().Add(new Paragraph($"{grams}g")).SetBorder(new SolidBorder(1)));
                }
            }

            return table;
        }

        private static void AddSubSectionHeader(Cell container, string title)
        {
            Paragraph paragraph = new Paragraph(title)
                .SetFont(PdfStyleSettings.TitleBoldFont)
                .SetCharacterSpacing(1)
                .SetFontSize(12)
                .SetBold();

            container.Add(paragraph);
        }

        private static Cell CreateDescHeaderCell(string desc)
        {
            return new Cell()
                .Add(new Paragraph(desc)
                    .SetFont(PdfStyleSettings.TitleBoldFont)
                    .SetFontSize(14))
                .SetBackgroundColor(PdfStyleSettings.RecipeHeaderColor)
                .SetBorder(new SolidBorder(1))
                .SetPaddings(0, 10, 0, 10);
        }

        private static Cell CreateValueHeaderCell(string value)
        {
            return new Cell()
                .Add(new Paragraph(value)
                    .SetFont(PdfStyleSettings.BodyFont)
                    .SetFontSize(12))
                .SetBackgroundColor(PdfStyleSettings.RecipeHeaderColor)
                .SetBorder(new SolidBorder(1))
                .SetPaddings(4, 10, 0, 10);
        }

        private static Cell CreateDescCell(string desc)
        {
            return new Cell()
                .Add(new Paragraph(desc))
                .SetFontSize(10)
                .SetPaddings(4, 10, 4, 10)
                .SetBorder(new SolidBorder(1));
        }

        private static Cell CreateValueCell(string value)
        {
            return new Cell()
                .Add(new Paragraph(value))
                .SetPaddings(4, 10, 4, 10)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(new SolidBorder(1));
        }

        private static double CalculateBMI(double weight, double height)
        {
            return weight / Math.Pow(height / 100, 2);
        }

        private static int CalculateMaintenanceCalories(int age, double weight, double height, string activityLevel)
        {
            // Mifflin-St Jeor Formula
            double bmr = 10 * weight + 6.25 * height - 5 * age + 5;

            double activityMultiplier = activityLevel switch
            {
                "sedentary" => 1.2,
                "light" => 1.375,
                "moderate" => 1.55,
                "heavy" => 1.725,
                "athlete" => 1.9,
                _ => 1.2
            };

            return (int)(bmr * activityMultiplier);
        }

        private static double[] CalculateIdealWeightRange(double height)
        {
            double lower = 18.5 * Math.Pow(height / 100, 2);
            double upper = 24.9 * Math.Pow(height / 100, 2);
            return new[] { lower, upper };
        }

        private static BmiClassification GetBMIClassification(double bmi)
        {
            if (bmi < 18.5) return BmiClassification.Underweight;
            if (bmi < 25) return BmiClassification.Underweight;
            if (bmi < 30) return BmiClassification.Overweight;
            return BmiClassification.Obese;
        }

        private static string GetBmiClassificationText(BmiClassification classification)
        {
            switch (classification)
            {
                case BmiClassification.Underweight:
                    return "Underweight";
                case BmiClassification.NormalWeight:
                    return "Normal Weight";
                case BmiClassification.Overweight:
                    return "Overweight";
                case BmiClassification.Obese:
                    return "Obese";
            }

            return "";
        }

        private static Paragraph CreateSubSectionParagraph()
        {
            return new Paragraph()
                .SetFont(PdfStyleSettings.BodyFont)
                .SetFixedLeading(16);
        }

        private enum BmiClassification
        {
            Underweight,
            NormalWeight,
            Overweight,
            Obese
        }
    }
}