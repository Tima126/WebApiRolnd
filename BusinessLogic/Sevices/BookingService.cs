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
    public class BookingService : IBookingService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public BookingService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Booking>> GetAll()
        {
            return await _repositoryWrapper.Booking.FindAll();
        }

        public async Task<Booking> GetById(int id)
        {
            var booking = await _repositoryWrapper.Booking
                .FindByCondition(x => x.BookingId == id);
            return booking.First();
        }

        public async Task Create(Booking model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.UserId == null)
            {
                throw new ArgumentException("UserId cannot be null.", nameof(model.UserId));
            }
            if (model.FlightId == null)
            {
                throw new ArgumentException("FlightId cannot be null.", nameof(model.FlightId));
            }
            if (model.BookingDate == DateTime.MinValue)
            {
                throw new ArgumentException("BookingDate cannot be the minimum value.", nameof(model.BookingDate));
            }
            if (string.IsNullOrEmpty(model.Status))
            {
                throw new ArgumentException("Status cannot be null or empty.", nameof(model.Status));
            }

            await _repositoryWrapper.Booking.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Booking model)
        {
            await  _repositoryWrapper.Booking.Update(model);
            await  _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var booking = await _repositoryWrapper.Booking
                .FindByCondition(x => x.BookingId == id);

            await _repositoryWrapper.Booking.Delete(booking.First());
            await _repositoryWrapper.Save();
        }
    }
}
