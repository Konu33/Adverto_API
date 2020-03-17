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
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            
            CreateMap<User, UserResponse>().ForMember(dest => dest.Adverts, opt =>
                 opt.MapFrom(src => src.Adverts.Select(c => new AdvertResponse { Description = c.Description, Location = c.Location, Name = c.Name, Prize = c.Prize, CategoryId = c.CategoryId })));
           
            CreateMap<Category, CategoryResponse>()
                .ForMember(dest=>dest.SubCategories,opt =>
                opt.MapFrom(src => src.SubCategories.Select(c=> new SubCategoryResponse {   Name = c.Name, CategoryId = c.CategoryId})));


            CreateMap<Advert, AdvertResponse>();
                //.ForMember(dest => dest.CategoryId,opt =>
         //   opt.MapFrom(c=>c.CategoryId));


            CreateMap<SubCategory, SubCategoryResponse>();
            
        }
    }
}
