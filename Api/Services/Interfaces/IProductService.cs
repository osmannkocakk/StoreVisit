using Domain;

namespace Api.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts(int page, int pageSize);
        Task<Product> AddProduct(Product product);
    }

}
