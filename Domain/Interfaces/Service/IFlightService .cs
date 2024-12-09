using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IFlightService
    {
        Task<List<Flight>> GetAll();
        Task<Flight> GetById(int id);
        Task Create(Flight model);

        Task Update(Flight model);
        Task Delete(int id);
    }
}
