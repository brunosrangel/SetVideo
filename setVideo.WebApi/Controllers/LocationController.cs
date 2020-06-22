using Microsoft.AspNetCore.Mvc;
using setVideo.Model;
using setVideo.Service;

namespace setVideo.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class LocationController : Controller, ILocationController
    {
        private readonly ILocationServices _locationServices;

        public LocationController(ILocationServices locationService, ICustomerServices customerService, IMoviesServices moviesService)
        {
            _locationServices = locationService;

        }


        [HttpPost()]
        public string Post([FromBody] dtLocation loc)
        {
            return _locationServices.Alter(loc.idCustomer, loc.idMovie, loc.action);
        }
        [HttpPost("cretateDB")]
        public string CretateDb()
        {
            return _locationServices.createDb();
        }
    }
    public class dtLocation
    {
        public int idMovie { get; set; }
        public int idCustomer { get; set; }
        public string action { get; set; }

    }

    public interface ILocationController
    {
         string Post(dtLocation loc);
        //string Devolution(int idMovie, int idCustomer);
        

    }
}
