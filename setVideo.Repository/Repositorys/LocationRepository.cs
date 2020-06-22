using setVideo.Model;
using System.Linq;

namespace setVideo.Repository
{
    public class LocationRepository : BaseRepository<setVideoContext, Location>, ILocationRepository
    {
        public LocationRepository(setVideoContext contexto)
        {
            DataContext = contexto;
            DbSet = DataContext.Set<Location>();
        }

        public Location GetLocation(Customer customer) {
            return DataContext.Locations.FirstOrDefault(d => d.Customer.Id == customer.Id);
        }
    }

    public interface ILocationRepository : IBaseRepository<Location>
    {
        Location GetLocation(Customer customer);
    }
}
