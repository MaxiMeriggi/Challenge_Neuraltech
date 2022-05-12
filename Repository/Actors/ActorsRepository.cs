using ChallengeNeuraltech.Persistence.Model;
using ChallengeNeuraltech.Persistence.MySql;
using ChallengeNeuraltech.Repository.Movies;
using Microsoft.EntityFrameworkCore;

namespace ChallengeNeuraltech.Repository.Actors
{
    public class ActorsRepository : IActorsRepository
    {
        private readonly MySqlContext _context;
        private readonly IMoviesRepository _moviesRepository;

        public ActorsRepository(MySqlContext context, IMoviesRepository moviesRepository)
        {
            _context = context;
            _moviesRepository = moviesRepository;
        }

        public async Task<ActorModel> AddActor(ActorModel actor)
        {
            try
            {
                _context.Actors.Add(actor);
                
                await _context.SaveChangesAsync();
                
                return actor;
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to insert the entity", ex);
            }
        }

        public async Task AssignActor(int actorId, int movieId)
        {
            try
            {
                var movieToAssign = await _moviesRepository.GetMovieById(movieId);

                if (movieToAssign is null) throw new Exception("Movie not found.");

                var actorToAssign = await _context.Actors.Include(p => p.Movies).FirstOrDefaultAsync(prod => prod.Id == actorId);

                actorToAssign.Movies.Add(movieToAssign);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        public async Task<ActorModel?> GetActorById(int actorId)
        {
            try
            {
                var actor = await _context.Actors.FirstOrDefaultAsync(actor => actor.Id == actorId);
                return actor;
            }
            catch(Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        public async Task<List<ActorModel>> GetAllActors()
        {
            try
            {
                var actors = await _context.Actors.ToListAsync();
                return actors;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        public async Task RemoveActor(int actorId)
        {
            try
            {
                var index = await GetActorById(actorId);
                _context.Actors.Remove(index);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        public async Task<ActorModel> UpdateActor(ActorModel actor)
        {
            try
            {
                var actorToUpdate = await GetActorById(actor.Id);
                actorToUpdate.FirstName = actor.FirstName;
                actorToUpdate.LastName = actor.LastName;
                actorToUpdate.BirthDate = actor.BirthDate;

                await _context.SaveChangesAsync();
                return actorToUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }
    }
}
