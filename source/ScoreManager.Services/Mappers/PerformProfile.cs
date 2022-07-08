using AutoMapper;
using ScoreManager.Dto.Request;
using ScoreManager.Dto.Response;
using ScoreManager.Entities;

namespace ScoreManager
{
    public class PerformProfile : Profile
    {
        public PerformProfile()
        {
            CreateMap<InsertPerformDto, Perform>()
                .ForMember(
                    dest => dest.SongTitle,
                    opt => opt.MapFrom(src => $"{src.SongTitle}")
                )
                .ForMember(
                    dest => dest.SongLyrics,
                    opt => opt.MapFrom(src => $"{src.SongLyrics}")
                )
                .ForMember(
                    dest => dest.SongInterpreter,
                    opt => opt.MapFrom(src => $"{src.SongInterpreter}")
                )
                ;

            CreateMap<Perform, PerformDto>()
               .ForMember(
                   dest => dest.SongTitle,
                   opt => opt.MapFrom(src => $"{src.SongTitle}")
               )
               .ForMember(
                   dest => dest.SongLyrics,
                   opt => opt.MapFrom(src => $"{src.SongLyrics}")
               )
               .ForMember(
                   dest => dest.SongInterpreter,
                   opt => opt.MapFrom(src => $"{src.SongInterpreter}")
               )
               .ForMember(
                   dest => dest.Category,
                   opt => opt.MapFrom(src => $"{src.Category.Name}")
               )
               .ForMember(
                   dest => dest.City,
                   opt => opt.MapFrom(src => $"{src.PrimaryCandidate.City}")
               )
               .ForMember(
                   dest => dest.PrimaryCandidateName,
                   opt => opt.MapFrom(src => $"{src.PrimaryCandidate.Name}")
               )
               .ForMember(
                   dest => dest.SecondaryCandidateName,
                   opt => opt.MapFrom(src => $"{src.SecondaryCandidate.Name}")
               )
               .ForMember(
                   dest => dest.Status,
                   opt => opt.MapFrom(src => $"{src.Status}")
               )
               .ForMember(
                   dest => dest.Id,
                   opt => opt.MapFrom(src => $"{src.Id}")
               )

               ;
        }
    }
}