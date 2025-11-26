using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Product?> AddProduct(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            var fromDb = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (fromDb == null) return false;
            _db.Products.Remove(fromDb);
            int rowsAffected = await _db.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<Product?> GetProduct(Expression<Func<Product, bool>> predicate)
        {
            return await _db.Products.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product?>> GetProducts(Expression<Func<Product, bool>> predicate)
        {
            return await _db.Products.Where(predicate).ToListAsync();
        }

        public async Task<Product?> UpdateProduct(Product product)
        {
            var fromDb = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == product.ProductId);
            if (fromDb == null) return null;

            fromDb.ProductName = product.ProductName;
            fromDb.Category = product.Category;
            fromDb.UnitPrice = product.UnitPrice;
            fromDb.QuantityInStock = product.QuantityInStock;
            await _db.SaveChangesAsync();

            return fromDb;
        }
    }
}
