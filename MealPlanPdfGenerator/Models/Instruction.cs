using System.ComponentModel.DataAnnotations;

namespace MealPlanPdfGenerator.Models
{
    public class Instruction
    {
        [Key]
        public Guid Id { get; set; }

        public int Index { get; set; }
        public string Description { get; set; }

        public Guid MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
