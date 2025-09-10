using EstoqueApi.Data;
using EstoqueApi.Interface;
using EstoqueApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EstoqueApi.Repository
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(Guid uuid)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Uuid == uuid) ?? throw new NullReferenceException("Product not found");
        }

        public async Task<Product> Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> Delete(Guid uuid)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Uuid == uuid);
            if (product == null)
            {
                throw new NullReferenceException("Product not found");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
