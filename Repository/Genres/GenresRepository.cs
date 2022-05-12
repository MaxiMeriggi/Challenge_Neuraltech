using ChallengeNeuraltech.Persistence.Model;
using ChallengeNeuraltech.Persistence.MySql;
using ChallengeNeuraltech.Repository.Movies;
using Microsoft.EntityFrameworkCore;

namespace ChallengeNeuraltech.Repository.Genres
{
    public class GenresRepository : IGenresRepository
    {
        private readonly MySqlContext _context;
        private readonly IMoviesRepository _moviesRepository;
        public GenresRepository(MySqlContext context, IMoviesRepository moviesRepository)
        {
            _context = context;
            _moviesRepository = moviesRepository;
        }

        public async Task<GenreModel> AddGenre(GenreModel genre)
        {
            try
            {
                _context.Genres.Add(genre);

                await _context.SaveChangesAsync();

                return genre;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert the entity", ex);
            }
        }

        public async Task<GenreModel?> GetGenreById(int genreId)
        {
            try
            {
                var genre = await _context.Genres.FirstOrDefaultAsync(genre => genre.Id == genreId);

                return genre;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }


        }

        public async Task<List<GenreModel>> GetAllGenres()
        {
            try
            {
                var genres = await _context.Genres.ToListAsync();
                return genres;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        public async Task RemoveGenre(int genreId)
        {
            try
            {
                var index = await GetGenreById(genreId);

                _context.Genres.Remove(index);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        public async Task<GenreModel> UpdateGenre(GenreModel genre)
        {
            try
            {
                var genreToUpdate = await GetGenreById(genre.Id);

                genreToUpdate.Name = genre.Name;

                await _context.SaveChangesAsync();

                return genreToUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        public async Task AssignGenre(int genreId, int movieId)
        {
            try
            {
                var movieToAssign = await _moviesRepository.GetMovieById(movieId);

                if (movieToAssign is null) throw new Exception("Movie not found.");

                var genreToAssign = await _context.Genres.Include(p => p.Movies).FirstOrDefaultAsync(prod => prod.Id == genreId);

                genreToAssign.Movies.Add(movieToAssign);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }
    }
}
