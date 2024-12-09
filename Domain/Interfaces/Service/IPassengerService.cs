using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IPassengerService
    {
        Task<List<Passenger>> GetAll();
        Task<Passenger> GetById(int id);
        Task Create(Passenger model);

        Task Update(Passenger model);
        Task Delete(int id);
    }
}
