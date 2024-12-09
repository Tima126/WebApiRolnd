
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Interfaces.Repository;

namespace DataAccess.Repositories
{
    internal class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(RolandContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
