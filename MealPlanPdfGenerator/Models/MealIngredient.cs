using System.ComponentModel.DataAnnotations;

namespace MealPlanPdfGenerator.Models
{
    public class MealIngredient
    {
        [Key]
        public Guid Id { get; set; }

        public Guid MealId { get; set; }
        public Meal Meal { get; set; }

        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
    }
}
