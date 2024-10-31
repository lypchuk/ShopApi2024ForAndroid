using Ardalis.Specification;
using AutoMapper;
using Bogus;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShopApi2024.DTOs;
using ShopApi2024.Entities;
using ShopApi2024.Interfaces;

namespace ShopApi2024.Profiles
{
    public class ApplicationProfile: Profile
    {


        public ApplicationProfile(IFileService fileService/*, IConfiguration configuration*/)
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>().ForMember(x=>x.DeleteTime, opt=> opt.Ignore())
                                            .ForMember(x => x.IsDelete, opt => opt.Ignore());
            //CreateMap<CreateCategoryDto, Category>().ForMember(x => x.ImageName, opt => opt
            //.MapFrom(src => fileService.UploadFileImage(src.ImageFile).Result));
            CreateMap<UpdateCategoryDto, Category>().ForMember(x => x.IsDelete, opt => opt.Ignore())
                                                        .ForMember(x => x.CreationTime, opt => opt.Ignore())
                                                        .ForMember(x => x.DeleteTime, opt => opt.Ignore());
                                                        //.ForMember(x => x.ImageName, opt => opt.Ignore());
            CreateMap<CreateCategoryDto, Category>()
                .ForMember(x => x.ImagePath, opt => opt.MapFrom(src =>
                //string.IsNullOrEmpty(src.ImageFile!.ToString())
                //string.IsNullOrEmpty(src.ImageFile!.ToString()) && src.ImageFile.Name.Length > 0 ?  (Path.DirectorySeparatorChar + "uploadingImages" + Path.DirectorySeparatorChar + "noimage.jpg") : (
                fileService.SaveFileImage(src.ImageFile!)))
                //: configuration["ImageDir"] + "/" + "noimage.jpg"))
                //.ForMember(x => x.ImageName, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.MapFrom(src => DateTime.Now));



            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>().ForMember(x=> x.CreationTime, opt => opt.MapFrom(src => DateTime.UtcNow));


            CreateMap<ProductImage, ProductImageDto>();


            /*
            CreateMap<TypeRoom, TypeRoomDto>().ReverseMap();
            */

            //CreateMap<CreateHotelRoom, HotelRoom>()
            //    .ForMember(x => x.ImagePath, opt => opt.MapFrom(src => fileService.SaveProductImage(src.Image).Result));

        }
    }
}
