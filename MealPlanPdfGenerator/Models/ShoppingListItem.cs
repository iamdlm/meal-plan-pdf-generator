using System.ComponentModel.DataAnnotations;

namespace MealPlanPdfGenerator.Models
{
    public class ShoppingListItem
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }

        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
    }
}
