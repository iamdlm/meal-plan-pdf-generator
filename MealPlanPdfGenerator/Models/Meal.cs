using MealPlanPdfGenerator.Enums;
using System.ComponentModel.DataAnnotations;

namespace MealPlanPdfGenerator.Models
{
    public class Meal
    {
        [Key]
        public Guid Id { get; set; }

        public int Number { get; set; }
        public string Title { get; set; }
        public string ImagePrompt { get; set; }
        public string? Image { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Carbs { get; set; }
        public double Sugar { get; set; }
        public double Fat { get; set; }
        public double SaturatedFat { get; set; }
        public double Fiber { get; set; }
        public double Salt { get; set; }

        public Guid DayId { get; set; }
        public Day Day { get; set; }

        public List<MealIngredient> Ingredients { get; set; } = new List<MealIngredient>();
        public List<Instruction> Preparation { get; set; } = new List<Instruction>();

        public string Summary { get; set; }
    }
}
