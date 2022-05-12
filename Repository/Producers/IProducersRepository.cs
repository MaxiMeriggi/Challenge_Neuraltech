using ChallengeNeuraltech.Persistence.Model;

namespace ChallengeNeuraltech.Repository.Producers
{
    public interface IProducersRepository
    {
        Task<List<ProducerModel>> GetAllProducers();
        Task<ProducerModel?> GetProducerById(int producerId);
        Task<ProducerModel> AddProducer(ProducerModel producer);
        Task<ProducerModel> UpdateProducer(ProducerModel producer);
        Task RemoveProducer(int producerId);
        Task AssignProducer(int producerId, int movieId);
    }
}
