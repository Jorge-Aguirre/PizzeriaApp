namespace BusinessDomain.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }

        public int IngredientTypeId { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public IngredientType IngredientType { get; set; }
    }
}
