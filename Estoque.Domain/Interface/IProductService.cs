using EstoqueApi.DTOs;

namespace EstoqueApi.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();
        Task<ProductDto> GetProductById(Guid uuid);
        Task<ProductDto> AddProduct(ProductDto productDto);
        Task<ProductDto> UpdateProduct(Guid uuid, ProductDto productDto);
        Task<bool> DeleteProduct(Guid uuid);
    }
}
