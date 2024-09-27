using ShopApi2024.DTOs;

namespace ShopApi2024.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAll();
    }
}
