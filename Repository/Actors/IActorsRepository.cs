using ChallengeNeuraltech.Persistence.Model;

namespace ChallengeNeuraltech.Repository.Actors
{
    public interface IActorsRepository
    {
        Task<List<ActorModel>> GetAllActors();
        Task<ActorModel?> GetActorById(int actorId);
        Task<ActorModel> AddActor(ActorModel actor);
        Task<ActorModel> UpdateActor(ActorModel actor);
        Task RemoveActor(int actorId);
        Task AssignActor(int actorId, int movieId);
    }
}
