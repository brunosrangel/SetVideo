using System;
using Microsoft.Extensions.Logging;
using setVideo.Model;
using setVideo.Repository;

namespace setVideo.Service
{
    public class MoviesServices : IMoviesServices
    {
        private readonly ILogger<IMoviesServices> _logger;
        private readonly IMovieRepository _movieRepository;

        public MoviesServices(ILogger<IMoviesServices> logger, IMovieRepository movieRepository)
        {
            _logger = logger;
            _movieRepository = movieRepository;
        }
        public void Add(Movie movie)
        {

            try
            {
                _movieRepository.Add(movie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adcionar Filme");
                throw;
            }
        }

        

    }

    public interface IMoviesServices
    {
        void Add(Movie movie);
    }
}
