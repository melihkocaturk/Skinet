using API.Data;
using API.Data.Interfaces;
using API.Data.Specifications;
using API.Dtos;
using API.Entities;
using API.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo, 
            IGenericRepository<ProductBrand> productBrandRepo, 
            IGenericRepository<ProductType> productTypeRepo,
            IMapper mapper)
        {
            this._productsRepo = productsRepo;
            this._productBrandRepo = productBrandRepo;
            this._productTypeRepo = productTypeRepo;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IReadOnlyList<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = _productsRepo.Count(countSpec);
            var products = _productsRepo.List(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        public ActionResult<ProductToReturnDto> GetProduct(int id)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(id);

            var product = _productsRepo.GetEntityWithSpec(spec);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public ActionResult<ProductBrand> GetProductBrands()
        {
            var brands = _productBrandRepo.ListAll();

            return Ok(brands);
        }

        [HttpGet("types")]
        public ActionResult<ProductType> GetProductTypes()
        {
            var types = _productTypeRepo.ListAll();

            return Ok(types);
        }
    }
}
