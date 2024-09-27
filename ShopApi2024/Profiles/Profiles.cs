using AutoMapper;
using ShopApi2024.DTOs;
using ShopApi2024.Entities;
using ShopApi2024.Interfaces;

namespace ShopApi2024.Profiles
{
    public class ApplicationProfile: Profile
    {
        public ApplicationProfile(IFileService fileService)
        {
            CreateMap<Category, CategoryDto>();

            CreateMap<CategoryDto, Category>().ForMember(x=>x.DeleteTime, opt=> opt.Ignore())
                                            .ForMember(x => x.IsDelete, opt => opt.Ignore());


            //CreateMap<CreateCategoryDto, Category>().ForMember(x=> x.CreationTime , opt=> opt.MapFrom(src=> DateTime.Now));



            //CreateMap<CreateCategoryDto, Category>().ForMember(x => x.ImageName, opt => opt
            //.MapFrom(src => fileService.UploadFileImage(src.ImageFile).Result));

            CreateMap<UpdateCategoryDto, Category>().ForMember(x=>x.IsDelete, opt => opt.Ignore())
                                                        .ForMember(x => x.CreationTime, opt => opt.Ignore())
                                                        .ForMember(x => x.DeleteTime, opt => opt.Ignore())
                                                        .ForMember(x => x.ImageName, opt => opt.Ignore());

            CreateMap<CreateCategoryDto, Category>()
                .ForMember(x => x.ImageName, opt => opt
                .MapFrom(src => fileService.UploadFileImage(src.ImageFile).Result))
                .ForMember(x => x.CreationTime, opt => opt.MapFrom(src => DateTime.Now));


            CreateMap<Product, ProductDto>();


            //CreateMap<HotelRoomDto, HotelRoom>().ForMember(x => x.TypeRoom, opt => opt.Ignore());
            //CreateMap<HotelRoom, HotelRoomDto>();

            /*
            CreateMap<TypeRoom, TypeRoomDto>().ReverseMap();
            CreateMap<NumberOfSeats, NumberOfSeatsDto>().ReverseMap();
            CreateMap<NumberOfBeds, NumberOfBedsDto>().ReverseMap();
            CreateMap<CreateHotelRoom, HotelRoom>();
            */

            //CreateMap<CreateHotelRoom, HotelRoom>()
            //    .ForMember(x => x.ImageUrl, opt => opt.MapFrom(src => fileService.SaveProductImage(src.Image).Result));

        }
    }
}
