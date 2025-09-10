using System.ComponentModel.DataAnnotations;

namespace Estoque.Domain.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public Guid Uuid { get; set; } = Guid.NewGuid();

        [Required]
        public required string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
