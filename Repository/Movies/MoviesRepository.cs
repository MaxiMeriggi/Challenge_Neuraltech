using ChallengeNeuraltech.Persistence.Model;
using ChallengeNeuraltech.Persistence.MySql;
using Microsoft.EntityFrameworkCore;

namespace ChallengeNeuraltech.Repository.Movies
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly MySqlContext _context;

        public MoviesRepository(MySqlContext context)
        {
            _context = context;
        }

        public async Task<MovieModel> AddMovie(MovieModel movie)
        {
            try
            {
                _context.Movies.Add(movie);

                await _context.SaveChangesAsync();

                return movie;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert the entity", ex);
            }
        }

        public async Task<MovieModel?> GetMovieById(int movieId)
        {
            try
            {
                var movie = await _context.Movies
                    .Include(m => m.Actor)
                    .Include(m => m.Genre)
                    .Include(m => m.Image)
                    .Include(m => m.Producer)
                    .FirstOrDefaultAsync(movie => movie.Id == movieId);

                return movie;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }


        }

        public async Task<List<MovieModel>> GetAllMovies()
        {
            try
            {
                var movies = await _context.Movies
                    .Include(m => m.Actor)
                    .Include(m => m.Genre)
                    .Include(m => m.Image)
                    .Include(m => m.Producer)
                    .ToListAsync();
                return movies;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        public async Task RemoveMovie(int movieId)
        {
            try
            {
                var index = await GetMovieById(movieId);

                _context.Movies.Remove(index);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        public async Task<MovieModel> UpdateMovie(MovieModel movie)
        {
            try
            {
                var movieToUpdate = await GetMovieById(movie.Id);

                movieToUpdate.Title = movie.Title;
                movieToUpdate.Rating = movie.Rating;
                movieToUpdate.Duration = movie.Duration;
                movieToUpdate.Producer = movie.Producer;

                await _context.SaveChangesAsync();

                var updatedMovie = await GetMovieById(movie.Id);

                return updatedMovie;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }
    }
}
