using AutoMapper;
using ScoreManager.Dto.Request;
using ScoreManager.Entities;

namespace ScoreManager
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<InsertCategoryDto, Category>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.Name}")
                )
                ;
        }
    }
}