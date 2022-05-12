using AutoMapper;
using ChallengeNeuraltech.DTO.Request;
using ChallengeNeuraltech.DTO.Response;
using ChallengeNeuraltech.Persistence.Model;

namespace ChallengeNeuraltech.Automapper
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<ActorModel, ActorResponse>().ReverseMap();
            CreateMap<GenreModel, GenreResponse>().ReverseMap();
            CreateMap<ImageModel, ImageResponse>().ReverseMap();
            CreateMap<MovieModel, MovieResponse>().ReverseMap();
            CreateMap<ProducerModel, ProducerResponse>().ReverseMap();

            CreateMap<ActorModel, ActorRequest>().ReverseMap();
            CreateMap<GenreModel, GenreRequest>().ReverseMap();
            CreateMap<ImageModel, ImageRequest>().ReverseMap();
            CreateMap<MovieModel, MovieRequest>().ReverseMap();
            CreateMap<ProducerModel, ProducerRequest>().ReverseMap();

            CreateMap<ActorModel, ActorUpdateRequest>().ReverseMap();
            CreateMap<GenreModel, GenreUpdateRequest>().ReverseMap();
            CreateMap<ImageModel, ImageUpdateRequest>().ReverseMap();
            CreateMap<MovieModel, MovieUpdateRequest>().ReverseMap();
            CreateMap<ProducerModel, ProducerUpdateRequest>().ReverseMap();
        }
    }
}
