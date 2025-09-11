using Estoque.Domain.Models;
using EstoqueApi.DTOs;
using EstoqueApi.Interface;

namespace Estoque.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            try
            {
                var products = await _productRepository.GetAll();
                return products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    Uuid = p.Uuid
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductDto> GetProductById(Guid uuid)
        {
            try
            {
                var product = await _productRepository.GetById(uuid);
                if (product == null) throw new NullReferenceException("Product not found");

                return new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    Uuid = product.Uuid

                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductDto> AddProduct(ProductDto productDto)
        {
            try
            {
                var product = new Product
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Quantity = productDto.Quantity,
                    Price = productDto.Price,
                    Uuid = Guid.NewGuid()
                };
                var addedProduct = await _productRepository.Add(product);
                return new ProductDto
                {
                    Id = addedProduct.Id,
                    Name = addedProduct.Name,
                    Description = addedProduct.Description,
                    Quantity = addedProduct.Quantity,
                    Price = addedProduct.Price,
                    Uuid = addedProduct.Uuid
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductDto> UpdateProduct(Guid uuid, ProductDto productDto)
        {
            try
            {
                var productToUpdate = await _productRepository.GetById(uuid);
                if (productToUpdate == null) throw new NullReferenceException("Product not found");

                productToUpdate.Name = productDto.Name;
                productToUpdate.Description = productDto.Description;
                productToUpdate.Quantity = productDto.Quantity;
                productToUpdate.Price = productDto.Price;

                var updatedProduct = await _productRepository.Update(productToUpdate);
                return new ProductDto
                {
                    Id = updatedProduct.Id,
                    Name = updatedProduct.Name,
                    Description = updatedProduct.Description,
                    Quantity = updatedProduct.Quantity,
                    Price = updatedProduct.Price,
                    Uuid = updatedProduct.Uuid
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteProduct(Guid uuid)
        {
            try
            {
                return await _productRepository.Delete(uuid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
