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
    public class CharterService : ICharterService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public CharterService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Charter>> GetAll()
        {
            return await _repositoryWrapper.Charter.FindAll();
        }

        public async Task<Charter> GetById(int id)
        {
            var charter = await _repositoryWrapper.Charter
                .FindByCondition(x => x.CharterId == id);
            return charter.First();
        }

        public async Task Create(Charter model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.FlightId == null)
            {
                throw new ArgumentException("FlightId cannot be null.", nameof(model.FlightId));
            }
            if (string.IsNullOrEmpty(model.CharterCompany))
            {
                throw new ArgumentException("CharterCompany cannot be null or empty.", nameof(model.CharterCompany));
            }

            await _repositoryWrapper.Charter.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Charter model)
        {
            await _repositoryWrapper.Charter.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var charter = await _repositoryWrapper.Charter
                .FindByCondition(x => x.CharterId == id);

            await _repositoryWrapper.Charter.Delete(charter.First());
            await _repositoryWrapper.Save();
        }
    }
}
