using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace BusinessLogicLayer.ServiceContracts
{
    public interface IProductsService
    {
        Task<List<ProductResponse>> GetProducts();
        Task<List<ProductResponse?>> GetProducts(Expression<Func<Product, bool>> condition);
        Task<ProductResponse?> GetProduct(Expression<Func<Product, bool>> condition);

        Task<ProductResponse?> AddProduct(ProductAddRequest product);

        Task<ProductResponse> UpdateProduct(ProductUpdateRequest product);

        Task<bool> DeleteProduct(Guid productId);
    }
}
