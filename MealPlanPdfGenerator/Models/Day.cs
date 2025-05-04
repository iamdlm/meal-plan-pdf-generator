using System.ComponentModel.DataAnnotations;

namespace MealPlanPdfGenerator.Models
{
    public class Day
    {
        [Key]
        public Guid Id { get; set; }

        public int DayNumber { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Carbs { get; set; }
        public double Sugar { get; set; }
        public double Fat { get; set; }
        public double SaturatedFat { get; set; }
        public double Fiber { get; set; }
        public double Salt { get; set; }

        public Guid MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; }

        public List<Meal> Meals { get; set; } = new List<Meal>();
        public List<ShoppingList> ShoppingList { get; set; } = new List<ShoppingList>();
    }
}
