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
    public class FlightService : IFlightService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public FlightService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Flight>> GetAll()
        {
            return await _repositoryWrapper.Flight.FindAll();
        }

        public async Task<Flight> GetById(int id)
        {
            var flight = await _repositoryWrapper.Flight
                .FindByCondition(x => x.FlightId == id);
            return flight.First();
        }

        public async Task Create(Flight model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.FlightNumber))
            {
                throw new ArgumentException("FlightNumber cannot be null or empty.", nameof(model.FlightNumber));
            }
            if (model.DepartureAirportId == null)
            {
                throw new ArgumentException("DepartureAirportId cannot be null.", nameof(model.DepartureAirportId));
            }
            if (model.ArrivalAirportId == null)
            {
                throw new ArgumentException("ArrivalAirportId cannot be null.", nameof(model.ArrivalAirportId));
            }
            if (model.DepartureTime == DateTime.MinValue)
            {
                throw new ArgumentException("DepartureTime cannot be the minimum value.", nameof(model.DepartureTime));
            }
            if (model.ArrivalTime == DateTime.MinValue)
            {
                throw new ArgumentException("ArrivalTime cannot be the minimum value.", nameof(model.ArrivalTime));
            }

            await _repositoryWrapper.Flight.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Flight model)
        {
            await _repositoryWrapper.Flight.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var flight = await _repositoryWrapper.Flight
                .FindByCondition(x => x.FlightId == id);

            await _repositoryWrapper.Flight.Delete(flight.First());
            await _repositoryWrapper.Save();
        }
    }
}
