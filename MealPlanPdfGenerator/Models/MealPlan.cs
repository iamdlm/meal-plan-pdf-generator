using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MealPlanPdfGenerator.Models
{
    public class MealPlan
    {
        [Key]
        public Guid Id { get; set; }

        public Guid FormEntryId { get; set; }
        public FormEntry FormEntry { get; set; }

        public List<Day> Days { get; set; } = new List<Day>();

        [NotMapped]
        public List<ShoppingList> ShoppingList { get; set; } = new List<ShoppingList>();
    }
}
