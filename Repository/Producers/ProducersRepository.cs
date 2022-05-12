using ChallengeNeuraltech.Persistence.Model;
using ChallengeNeuraltech.Persistence.MySql;
using ChallengeNeuraltech.Repository.Movies;
using Microsoft.EntityFrameworkCore;

namespace ChallengeNeuraltech.Repository.Producers
{
    public class ProducersRepository : IProducersRepository
    {
        private readonly MySqlContext _context;
        private readonly IMoviesRepository _moviesRepository;

        public ProducersRepository(MySqlContext context, IMoviesRepository moviesRepository)
        {
            _context = context;
            _moviesRepository = moviesRepository;
        }

        public async Task<ProducerModel> AddProducer(ProducerModel producer)
        {
            try
            {
                _context.Producers.Add(producer);

                await _context.SaveChangesAsync();

                return producer;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert the entity", ex);
            }
        }
        public async Task AssignProducer(int producerId, int movieId)
        {
            try
            {
                var movieToAssign = await _moviesRepository.GetMovieById(movieId);

                if (movieToAssign is null) throw new Exception("Movie not found.");

                var producerToAssign = await _context.Producers.Include(p => p.Movies).FirstOrDefaultAsync(prod => prod.Id == producerId);

                producerToAssign.Movies.Add(movieToAssign);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        public async Task<ProducerModel?> GetProducerById(int producerId)
        {
            try
            {
                var producer = await _context.Producers.FirstOrDefaultAsync(producer => producer.Id == producerId);

                return producer;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }


        }

        public async Task<List<ProducerModel>> GetAllProducers()
        {
            try
            {
                var producers = await _context.Producers.ToListAsync();
                return producers;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        public async Task RemoveProducer(int producerId)
        {
            try
            {
                var index = await GetProducerById(producerId);

                _context.Producers.Remove(index);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        public async Task<ProducerModel> UpdateProducer(ProducerModel producer)
        {
            try
            {
                var producerToUpdate = await GetProducerById(producer.Id);

                producerToUpdate.Name = producer.Name;
                producerToUpdate.FundationTime = producer.FundationTime;

                await _context.SaveChangesAsync();

                return producerToUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }
    }
}
