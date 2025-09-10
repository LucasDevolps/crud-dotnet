namespace EstoqueApi.DTOs
{
    public sealed class ProductDto
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
