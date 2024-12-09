
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Interfaces.Repository;

namespace DataAccess.Repositories
{
    internal class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(RolandContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
