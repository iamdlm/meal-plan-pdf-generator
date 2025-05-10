using MealPlanPdfGenerator.Enums;
using System.ComponentModel.DataAnnotations;

namespace MealPlanPdfGenerator.Models
{
    public class FormEntry
    {
        #region Properties

        [Key]
        public Guid Id { get; set; }

        public SystemOfUnit Unit { get; set; }

        [Required]
        public required string Gender { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public int Activity { get; set; }

        [Required]
        public int Goal { get; set; }

        [Required]
        public int Diet { get; set; }

        [Required]
        public int Meals { get; set; }

        [Required]
        public bool Wheat { get; set; }

        [Required]
        public bool Milk { get; set; }

        [Required]
        public bool Eggs { get; set; }

        [Required]
        public bool Soy { get; set; }

        [Required]
        public bool Nuts { get; set; }

        [Required]
        public bool Fish { get; set; }

        [Required]
        public bool None { get; set; }

        // Imperial properties

        [Required]
        public double WeightLbs { get; set; } // in pounds

        [Required]
        public int Feet { get; set; }

        [Required]
        public int Inches { get; set; }

        // Metric properties

        [Required]
        public double WeightKg { get; set; } // in kilograms

        [Required]
        public int Height { get; set; } // in centimeters        

        #endregion

        #region Navigation Properties

        public MealPlan MealPlan { get; set; }

        #endregion
    }
}
