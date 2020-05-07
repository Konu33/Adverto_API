using Adverto.Domain;
using Adverto.Dto.AdvertDto;
using Adverto.Dto.Category;
using Adverto.Dto.SubCategoriesDto;
using Adverto.Dto.UserDto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Mapping
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<UserRequest, User>()
                .ForMember(dest => dest.Adverts,opt =>
                opt.MapFrom(src => src.Adverts.Select(c=> new Advert() { Description = c.Description, Location = c.Location, Name= c.Name, Prize =c.Prize, CategoryId = c.CategoryId })));
            CreateMap<AdvertRequest, Advert>()
                .ForMember(dest => dest.CategoryId,opt=>
                opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.PhotoUrl,opt=>
                opt.MapFrom(src => src.file.FileName));
            CreateMap<CategoryRequest, Category>()
                .ForMember(dest => dest.SubCategories,opt =>
                opt.MapFrom(src =>src.SubCategories.Select(c=> new SubCategory() {  Name = c.Name , CategoryId =c.CategoryId})));
            CreateMap<SubCategoryRequest, SubCategory>();
           
        }
    }
}
