using System.ComponentModel.DataAnnotations;

namespace MealPlanPdfGenerator.Models
{
    public class ShoppingList
    {
        [Key]
        public Guid Id { get; set; }

        public string Category { get; set; }

        public Guid? DayId { get; set; }
        public Day Day { get; set; }

        public List<ShoppingListItem> Items { get; set; } = new List<ShoppingListItem>();
    }
}
