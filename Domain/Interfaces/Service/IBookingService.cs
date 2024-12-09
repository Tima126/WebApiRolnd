using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAll();
        Task<Booking> GetById(int id);
        Task Create(Booking model);

        Task Update(Booking model);
        Task Delete(int id);
    }
}
