using AutoMapper;
using ScoreManager.Dto.Request;
using ScoreManager.Entities;

namespace ScoreManager
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<InsertRateDto, Rating>()
                .ForMember(
                    dest => dest.Rate,
                    opt => opt.MapFrom(src => $"{src.Rate}")
                )
                ;
        }
    }
}