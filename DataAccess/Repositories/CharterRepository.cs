
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Repository;

namespace DataAccess.Repositories
{
    internal class CharterRepository : RepositoryBase<Charter>, ICharterRepository
    {
        public CharterRepository(RolandContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
