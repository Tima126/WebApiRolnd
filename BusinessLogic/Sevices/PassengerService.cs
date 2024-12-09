
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Interfaces.Service;
using Domain.Interfaces;


namespace BusinessLogic.Sevices
{
    public class PassengerService : IPassengerService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public PassengerService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Passenger>> GetAll()
        {
            return await _repositoryWrapper.Passenger.FindAll();
        }

        public async Task<Passenger> GetById(int id)
        {
            var passenger = await _repositoryWrapper.Passenger
                .FindByCondition(x => x.PassengerId == id);
            return passenger.First();
        }

        public async Task Create(Passenger model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.FirstName))
            {
                throw new ArgumentException("FirstName cannot be null or empty.", nameof(model.FirstName));
            }
            if (string.IsNullOrEmpty(model.LastName))
            {
                throw new ArgumentException("LastName cannot be null or empty.", nameof(model.LastName));
            }
            if (string.IsNullOrEmpty(model.PassportNumber))
            {
                throw new ArgumentException("PassportNumber cannot be null or empty.", nameof(model.PassportNumber));
            }
            if (model.DateOfBirth == DateTime.MinValue)
            {
                throw new ArgumentException("DateOfBirth cannot be the minimum value.", nameof(model.DateOfBirth));
            }

            await _repositoryWrapper.Passenger.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Passenger model)
        {
            await _repositoryWrapper.Passenger.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var passenger = await _repositoryWrapper.Passenger
                .FindByCondition(x => x.PassengerId == id);

            await _repositoryWrapper.Passenger.Delete(passenger.First());
            await _repositoryWrapper.Save();
        }
    }
}
