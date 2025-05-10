namespace MealPlanPdfGenerator.Pdf.ViewModels
{
    public class MacroDistributionViewModel
    {
        public int Min { get; set; }

        public int Max { get; set; }

        public string RangeText
        {
            get
            {
                return $"{Min} - {Max} %";
            }
        }

        public float Avg
        {
            get
            {
                return (Max + Min) / 2;
            }
        }
    }
}
