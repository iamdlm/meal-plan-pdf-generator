using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace MealPlanPdfGenerator.Pdf.Sections
{
    public static class FitnessAssessmentWriter
    {
        public static void Write(Document doc, int age, double weight, double height, string activityLevel)
        {
            doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

            // Calculate necessary values
            double bmi = CalculateBMI(weight, height);
            int maintenanceCalories = CalculateMaintenanceCalories(age, weight, height, activityLevel);
            double[] idealWeightRange = CalculateIdealWeightRange(height);

            // Add title
            Paragraph title = new Paragraph("Your Fitness Assessment")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20)
                .SetBold();
            doc.Add(title);

            // Main table
            Table mainTable = new Table(new float[] { 1, 2 }).UseAllAvailableWidth();
            mainTable.SetBorder(new SolidBorder(1));

            // Left column: Maintenance Calories
            Cell leftCell = new Cell(1, 1).SetBorder(new SolidBorder(1));
            leftCell.Add(new Paragraph("Your\nMaintenance\nCalories").SetBold());
            leftCell.Add(new Paragraph(maintenanceCalories.ToString()).SetFontSize(24).SetBold());
            leftCell.Add(new Paragraph("calories per day"));
            leftCell.Add(new Paragraph((maintenanceCalories * 7).ToString()).SetFontSize(18));
            leftCell.Add(new Paragraph("calories per week"));
            mainTable.AddCell(leftCell);

            // Right column: Explanation and Activity Levels
            Cell rightCell = new Cell(1, 1).SetBorder(new SolidBorder(1));
            rightCell.Add(new Paragraph($"Based on your stats, the best estimate for your maintenance calories is {maintenanceCalories} calories per day based on the Mifflin-St Jeor Formula, which is widely known to be the most accurate. The table below shows the difference if you were to have selected a different activity level."));
            rightCell.Add(CreateActivityLevelTable(maintenanceCalories));
            mainTable.AddCell(rightCell);

            doc.Add(mainTable);

            // BMI and Ideal Weight section
            Table bmiSection = new Table(2).UseAllAvailableWidth();
            bmiSection.SetBorder(new SolidBorder(1));

            // Ideal Weight
            Cell idealWeightCell = new Cell().SetBorder(new SolidBorder(1));
            idealWeightCell.Add(new Paragraph($"Ideal Weight: {idealWeightRange[0]:F1}-{idealWeightRange[1]:F1} kg")
                .SetBold());
            idealWeightCell.Add(new Paragraph("Your ideal body weight is estimated to be between " +
                $"{idealWeightRange[0]:F1}-{idealWeightRange[1]:F1} kg based on various formulas. " +
                "These formulas are based on your height and represent averages, so don't take them too seriously, especially if you lift weights."));
            bmiSection.AddCell(idealWeightCell);

            // BMI
            Cell bmiCell = new Cell().SetBorder(new SolidBorder(1));
            bmiCell.Add(new Paragraph($"BMI Score: {bmi:F1}").SetBold());
            bmiCell.Add(new Paragraph($"Your BMI is {bmi:F1}, which means you are classified as {GetBMIClassification(bmi)}."));
            bmiCell.Add(CreateBMITable());
            bmiSection.AddCell(bmiCell);

            doc.Add(bmiSection);

            // Macronutrients section
            doc.Add(new Paragraph("Macronutrients").SetFontSize(16).SetBold());
            doc.Add(new Paragraph($"These macronutrient values reflect your maintenance calories of {maintenanceCalories} calories per day."));
            doc.Add(CreateMacronutrientTable(maintenanceCalories));
        }

        private static Table CreateActivityLevelTable(int maintenanceCalories)
        {
            Table table = new Table(2).UseAllAvailableWidth();
            table.SetBorder(new SolidBorder(1));
            table.AddCell(new Cell().Add(new Paragraph("Activity Level").SetBold()).SetBorder(new SolidBorder(1)));
            table.AddCell(new Cell().Add(new Paragraph("Calories per Day").SetBold()).SetBorder(new SolidBorder(1)));

            string[] levels = { "Basal Metabolic Rate", "Sedentary", "Light Exercise", "Moderate Exercise", "Heavy Exercise", "Athlete" };
            double[] multipliers = { 1.0, 1.2, 1.375, 1.55, 1.725, 1.9 };

            for (int i = 0; i < levels.Length; i++)
            {
                table.AddCell(new Cell().Add(new Paragraph(levels[i])).SetBorder(new SolidBorder(1)));
                table.AddCell(new Cell().Add(new Paragraph($"{(int)(maintenanceCalories * multipliers[i])}")).SetBorder(new SolidBorder(1)));
            }

            return table;
        }

        private static Table CreateBMITable()
        {
            Table table = new Table(2).UseAllAvailableWidth();
            table.SetBorder(new SolidBorder(1));
            table.AddCell(new Cell().Add(new Paragraph("BMI Range").SetBold()).SetBorder(new SolidBorder(1)));
            table.AddCell(new Cell().Add(new Paragraph("Classification").SetBold()).SetBorder(new SolidBorder(1)));

            string[][] data =
            {
                new[] { "18.5 or less", "Underweight" },
                new[] { "18.5 - 24.99", "Normal Weight" },
                new[] { "25 - 29.99", "Overweight" },
                new[] { "30+", "Obese" }
            };

            foreach (var row in data)
            {
                table.AddCell(new Cell().Add(new Paragraph(row[0])).SetBorder(new SolidBorder(1)));
                table.AddCell(new Cell().Add(new Paragraph(row[1])).SetBorder(new SolidBorder(1)));
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

        private static string GetBMIClassification(double bmi)
        {
            if (bmi < 18.5) return "Underweight";
            if (bmi < 25) return "Normal Weight";
            if (bmi < 30) return "Overweight";
            return "Obese";
        }
    }
} 