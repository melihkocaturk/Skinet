using API.Entities;

namespace API.Data.Interfaces
{
    public interface IProductRepository
    {
        IReadOnlyList<Product> GetProducts();
        Product GetProductById(int id);
        IReadOnlyList<ProductType> GetProductTypes();
        IReadOnlyList<ProductBrand> GetProductBrands();
    }
}
