using Domain.Interfaces;
using Domain.Interfaces.Service;
using Domain.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Sevices
{
    public class SpecialService2 : ISpecialService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public SpecialService2(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Special>> GetAll()
        {
            return await _repositoryWrapper.Special.FindAll();
        }
        public async Task<Special> GetById(int id)
        {
            var special = await _repositoryWrapper.Special
                .FindByCondition(x => x.ServiceId == id);
            return special.First();
        }
        public async Task Create(Special model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.ServiceName))
            {
                throw new ArgumentException("ServiceName cannot be null or empty.", nameof(model.ServiceName));
            }

            await _repositoryWrapper.Special.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Special model)
        {
            await _repositoryWrapper.Special.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var special = await _repositoryWrapper.Special
                .FindByCondition(x => x.ServiceId == id);

            await _repositoryWrapper.Special.Delete(special.First());
            await _repositoryWrapper.Save();
        }
    }
}
