using System;
using Microsoft.Extensions.Logging;
using setVideo.Model;
using setVideo.Repository;

namespace setVideo.Service
{
    public class LocationServices : ILocationServices
    {
        private readonly ILogger<ILocationServices> _logger;
        private readonly ILocationRepository _locationRepository;
        private readonly IMovieRepository _movieRespository;
        private readonly ICustomerRepository _customerRepository;

        public LocationServices(ILogger<ILocationServices> logger,
                          ILocationRepository locationRepository,
                          IMovieRepository movieRepository,
                          ICustomerRepository customerRepository)
        {
            _logger = logger;
            _locationRepository = locationRepository;
            _customerRepository = customerRepository;
            _movieRespository = movieRepository;

        }


        public string Alter(int idMovie, int idCustomer, string action)
        {
            Movie movie = new Movie();
            Customer customer = new Customer();
            Location location = new Location();

            movie = _movieRespository.GetById(idMovie);
            customer = _customerRepository.GetById(idCustomer);
            location.Customer = customer;
            location.movie = movie;

            if (action == "location")
            {

                return Add(location);
            }
            else
            {
                return Devolution(location);
            }
        }

        public string Add(Location location)
        {
            string ret;
            try
            {


                if (location.movie.active == true && location.movie.amount > 0)
                {
                    location.locationDate = DateTime.Now;
                    location.locationDevolution = location.locationDate.AddDays(3);
                    _locationRepository.Add(location);
                    location.movie.amount = location.movie.amount - 1;
                    _locationRepository.Commit();

                    ret = "Locação feita com sucesso, com devolução marcada para : " + location.locationDevolution;
                }
                else
                {
                    ret = "Filme indisponível";
                }
                return ret;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adcionar Locação");
                throw;
            }
        }

        
        public string createDb()
        {
            string ret = "";

            ///Cria os dados no Banco em Memória

            Customer customer = new Customer();
            Movie movie = new Movie();

            customer.cpf = "30775784842";
            customer.name = "Bruno";
            customer.active = true;

            movie.amount = 1;
            movie.active = true;
            movie.movieName = "Vida";

            try
            {
                _customerRepository.Add(customer);
                _customerRepository.Commit();
                _movieRespository.Add(movie);
                _movieRespository.Commit();
                ret = "Carga dada com sucesso";
            }
            catch (Exception)
            {
                throw;
            }

            return ret;

        }

        public string Devolution(Location location)
        {
            string ret = "";
            try
            {
                Location locacationOld = _locationRepository.GetLocation(location.Customer);

                if (locacationOld.locationDevolution > DateTime.Now)
                {
                    ret = "Filme em Atraso!";

                }

                locacationOld.locationReturned = DateTime.Now;
                locacationOld.movie.amount += 1;
                _locationRepository.Update(location);
               

                ret += " Filme devolvido com sucesso !";
                return ret;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro");
                throw;
            }
        }
    }

    public interface ILocationServices
    {
        string Add(Location location);
        string Devolution(Location location);
        string Alter(int idMovie, int idCustomer, string action);
        string createDb();
    }
}

