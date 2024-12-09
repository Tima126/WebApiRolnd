
using DataAccess.Repositories;
using Domain.Interfaces.Repository;
using Domain.Models;

namespace Domain.Repositories
{
    public class AiroportRepository : RepositoryBase<Airport>, IAirportRepository
    {
        public AiroportRepository(RolandContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
