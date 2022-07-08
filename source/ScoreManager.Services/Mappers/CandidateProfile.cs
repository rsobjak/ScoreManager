using AutoMapper;
using ScoreManager.Dto.Request;
using ScoreManager.Entities;

namespace ScoreManager
{
    public class CandidateProfile : Profile
    {
        public CandidateProfile()
        {
            CreateMap<InsertCandidateDto, Candidate>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.Name}")
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => $"{src.Email}")
                )
                .ForMember(
                    dest => dest.Cellphone,
                    opt => opt.MapFrom(src => $"{src.Cellphone}")
                )
                .ForMember(
                    dest => dest.City,
                    opt => opt.MapFrom(src => $"{src.City}")
                )
                .ForMember(
                    dest => dest.Document,
                    opt => opt.MapFrom(src => $"{src.Document}")
                )

                ;
        }
    }
}