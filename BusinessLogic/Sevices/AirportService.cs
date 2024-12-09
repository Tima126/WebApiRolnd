using Domain.Interfaces;
using Domain.Interfaces.Service;
using Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Sevices
{
    public class AirportService : IAirportService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public AirportService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Airport>> GetAll()
        {
            return await _repositoryWrapper.Airport.FindAll();
        }

        public async Task<Airport> GetById(int id)
        {
            var airport = await _repositoryWrapper.Airport
                .FindByCondition(x => x.AirportId == id);
            return airport.First();
        }

        public async Task Create(Airport model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.AirportCode))
            {
                throw new ArgumentException("AirportCode cannot be null or empty.", nameof(model.AirportCode));
            }
            if (string.IsNullOrEmpty(model.AirportName))
            {
                throw new ArgumentException("AirportName cannot be null or empty.", nameof(model.AirportName));
            }
            if (string.IsNullOrEmpty(model.City))
            {
                throw new ArgumentException("City cannot be null or empty.", nameof(model.City));
            }
            if (string.IsNullOrEmpty(model.Country))
            {
                throw new ArgumentException("Country cannot be null or empty.", nameof(model.Country));
            }

            await _repositoryWrapper.Airport.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Airport model)
        {
            await _repositoryWrapper.Airport.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var airport = await _repositoryWrapper.Airport
                .FindByCondition(x => x.AirportId == id);

            await _repositoryWrapper.Airport.Delete(airport.First());
            await _repositoryWrapper.Save();
        }
    }
}
