
using Domain.Interfaces.Repository;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class PassengerRepository : RepositoryBase<Passenger>, IPassengerRepository
    {
        public PassengerRepository(RolandContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
