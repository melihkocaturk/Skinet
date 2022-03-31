using API.Data.Interfaces;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            this._context = context;
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Include(p => p.ProductType).Include(p => p.ProductBrand).FirstOrDefault(p => p.Id == id);
        }

        public IReadOnlyList<Product> GetProducts()
        {
            return _context.Products.Include(p => p.ProductType).Include(p => p.ProductBrand).ToList();
        }

        public IReadOnlyList<ProductType> GetProductTypes()
        {
            return _context.ProductTypes.ToList();
        }

        public IReadOnlyList<ProductBrand> GetProductBrands()
        {
            return _context.ProductBrands.ToList();
        }
    }
}
