using AutoMapper;
using ShopApi2024.DTOs;
using ShopApi2024.Entities;
using ShopApi2024.Interfaces;
using ShopApi2024.Specifications;

namespace ShopApi2024.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> productR;
        private readonly IMapper mapper;

        public ProductService(IMapper mapper, IRepository<Product> productR)
        {
            this.mapper = mapper;
            this.productR = productR;
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return mapper.Map<List<ProductDto>>(await productR.GetListBySpec(new ProductSpecs.All()));
        }
    }
}
