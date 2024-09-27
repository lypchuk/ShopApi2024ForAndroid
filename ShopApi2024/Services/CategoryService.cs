using AutoMapper;
using ShopApi2024.DTOs;
using ShopApi2024.Entities;
using ShopApi2024.Interfaces;
using ShopApi2024.Specifications;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace ShopApi2024.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoryR;
        private readonly IMapper mapper;
        private readonly IFileService localStorageFileService;
        public CategoryService(IMapper mapper, 
                                IRepository<Category> categoryR, 
                                IFileService localStorageFileService)
        {
            this.mapper = mapper;
            this.categoryR = categoryR;
            this.localStorageFileService = localStorageFileService;
        }


        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            return mapper.Map<List<CategoryDto>>(await categoryR.GetListBySpec(new CategorySpecs.All()));
        }

        public async Task<IEnumerable<Category>> GetAllWithProducts()
        {
            /*
             // after a delete
                db.Movies.Count(); // 9
                db.Movies.IgnoreQueryFilters().Count(); // 10
            */
            //return mapper.Map<List<Category>>(await hotelRoomR.GetListBySpec(new HotelRoomSpecs.All()));
            return await categoryR.GetListBySpec(new CategorySpecs.All());
            //return List<Category>(await categoryR.GetListBySpec(new HotelRoomSpecs.All()));
            //return categoryR.GetAll();
        }

        public async Task<CategoryDto?> Get(int id)
        {
            if (id < 0) throw new HttpException(Errors.IdMustPositive, HttpStatusCode.BadRequest);

            var item = await categoryR.GetItemBySpec(new CategorySpecs.ById(id));
            if (item == null) throw new HttpException(Errors.CategoryNotFound, HttpStatusCode.NotFound);


            var dto = mapper.Map<CategoryDto>(item);

            return dto;
        }

        public void Delete(int id)
        {
            if (id < 0) throw new HttpException(Errors.IdMustPositive, HttpStatusCode.BadRequest);

            // delete product by id
            var category = categoryR.GetById(id);
            localStorageFileService.DeleteFileImage(category.ImageName);

            if (category == null) throw new HttpException(Errors.CategoryNotFound, HttpStatusCode.NotFound);

            categoryR.Delete(category);
            categoryR.Save();
        }

        public void SoftDelete(int id)
        {
            if (id < 0) throw new HttpException(Errors.IdMustPositive, HttpStatusCode.BadRequest);

            var category = categoryR.GetById(id);

            //delete image
            localStorageFileService.DeleteFileImage(category.ImageName);

            if (category == null) throw new HttpException(Errors.CategoryNotFound, HttpStatusCode.NotFound);

            category.IsDelete = true;
            category.DeleteTime = DateTime.Now;
            categoryR.Update(category);
            categoryR.Save();
        }

        public void Create(CreateCategoryDto model)
        {
            //string imageName = localStorageFileService.UploadFileImage();
            //Category category = mapper.Map<Category>(model);
            categoryR.Insert(mapper.Map<Category>(model));
            categoryR.Save();
        }


        public async void Edit(UpdateCategoryDto model)
        {
            Category categoryUpdate = mapper.Map<Category>(model);
            
            var categoryOld = categoryR.GetById(model.Id);

            if(model.ImageFile != null)
            {
                localStorageFileService.DeleteFileImage(categoryOld.ImageName);
                categoryUpdate.ImageName= await localStorageFileService.UploadFileImage(model.ImageFile);
            }
            

            categoryUpdate.CreationTime = categoryOld.CreationTime;
            //categoryUpdate.DeleteTime = categoryOld.DeleteTime;
            //categoryUpdate.IsDelete = categoryOld.IsDelete;
            
            

            categoryR.Update(categoryUpdate);

            categoryR.Save();
        }
    }
}
