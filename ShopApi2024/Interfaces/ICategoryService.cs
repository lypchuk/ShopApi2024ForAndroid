using ShopApi2024.DTOs;
using ShopApi2024.Entities;

namespace ShopApi2024.Interfaces
{
    public interface ICategoryService
    {
        void Delete(int id);
        void SoftDelete(int id);
        void Create(CreateCategoryDto model);
        void Edit(UpdateCategoryDto model);


        Task <CategoryDto?> Get(int id);
        Task<IEnumerable<CategoryDto>> GetAll();
        Task<IEnumerable<Category>> GetAllWithProducts();
        
    }
}
