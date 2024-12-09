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
    public class BaggageService : IBaggageService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public BaggageService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Baggage>> GetAll()
        {
            return await _repositoryWrapper.Baggage.FindAll();
        }

        public async Task<Baggage> GetById(int id)
        {
            var baggage = await _repositoryWrapper.Baggage
                .FindByCondition(x => x.BaggageId == id);
            return baggage.First();
        }

        public async Task Create(Baggage model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.PassengerId == null)
            {
                throw new ArgumentException("PassengerId cannot be null.", nameof(model.PassengerId));
            }
            if (model.Weight <= 0)
            {
                throw new ArgumentException("Weight must be greater than 0.", nameof(model.Weight));
            }
            if (string.IsNullOrEmpty(model.Status))
            {
                throw new ArgumentException("Status cannot be null or empty.", nameof(model.Status));
            }

            await _repositoryWrapper.Baggage.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Baggage model)
        {
            await _repositoryWrapper.Baggage.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var baggage = await _repositoryWrapper.Baggage
                .FindByCondition(x => x.BaggageId == id);

            await _repositoryWrapper.Baggage.Delete(baggage.First());
            await _repositoryWrapper.Save();
        }
    }
}
