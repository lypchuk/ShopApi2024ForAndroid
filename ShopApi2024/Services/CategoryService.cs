using AutoMapper;
using ShopApi2024.DTOs;
using ShopApi2024.Entities;
using ShopApi2024.Interfaces;
using ShopApi2024.Specifications;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ShopApi2024.Services
{
    public class CategoryService/*(ShopApi2024DbContext _context)*/ : ICategoryService
    {
        private readonly IRepository<Category> categoryR;
        private readonly IMapper mapper;
        private readonly IFileService localStorageFileService;
        private readonly ShopApi2024DbContext _context;
        public CategoryService(IMapper mapper, 
                                IRepository<Category> categoryR, 
                                IFileService localStorageFileService,
                                ShopApi2024DbContext context)
        {
            this.mapper = mapper;
            this.categoryR = categoryR;
            this.localStorageFileService = localStorageFileService;
            this._context = context;
        }

        public async Task<IEnumerable<Category>> GetAllTeacher()
        {
            
            return await _context.Categories.ProjectTo<Category>(mapper.ConfigurationProvider).ToListAsync();//not wort in this project .... to work with dbcontext
            //return Ok(model);
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
            localStorageFileService.DeleteFileImage(category.ImagePath!);

            if (category == null) throw new HttpException(Errors.CategoryNotFound, HttpStatusCode.NotFound);

            categoryR.Delete(category);
            categoryR.Save();
        }

        public void SoftDelete(int id)
        {
            if (id < 0) throw new HttpException(Errors.IdMustPositive, HttpStatusCode.BadRequest);

            var category = categoryR.GetById(id);

            //delete image
            localStorageFileService.DeleteFileImage(category.ImagePath!);

            if (category == null) throw new HttpException(Errors.CategoryNotFound, HttpStatusCode.NotFound);

            category.IsDelete = true;
            category.DeleteTime = DateTime.UtcNow;
            //category.DeleteTime = DateTime.Now;
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


        public void Edit(UpdateCategoryDto model)
        {
            Category categoryUpdate = mapper.Map<Category>(model);
            
            var categoryOld = categoryR.GetById(model.Id);

            if(model.ImageFile != null)
            {
                localStorageFileService.DeleteFileImage(categoryOld.ImagePath!);
                categoryUpdate.ImagePath= localStorageFileService.SaveFileImage(model.ImageFile);
            }
            else
            {
                categoryUpdate.ImagePath = categoryOld.ImagePath;
            }
            

            categoryUpdate.CreationTime = categoryOld.CreationTime;
            //categoryUpdate.DeleteTime = categoryOld.DeleteTime;
            //categoryUpdate.IsDelete = categoryOld.IsDelete;

            

            categoryR.Update(categoryUpdate);

            categoryR.Save();
        }
    }
}
