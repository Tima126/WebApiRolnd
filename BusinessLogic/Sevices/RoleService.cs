using Domain.Models;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Service;
using Domain.Interfaces;

namespace BusinessLogic.Sevices
{
    public class RoleService : IRoleService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public RoleService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Role>> GetAll()
        {
            return await _repositoryWrapper.Role.FindAll();
        }
        public async Task<Role> GetById(int id)
        {
            var role = await _repositoryWrapper.Role
                .FindByCondition(x => x.RoleId == id);
            return role.First();
        }
        public async Task Create(Role model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.RoleName))
            {
                throw new ArgumentException("RoleName cannot be null or empty.", nameof(model.RoleName));
            }

            await _repositoryWrapper.Role.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Role model)
        {
            await _repositoryWrapper.Role.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var role = await _repositoryWrapper.Role
                .FindByCondition(x => x.RoleId == id);

            await _repositoryWrapper.Role.Delete(role.First());
            await _repositoryWrapper.Save();
        }
    }
}
