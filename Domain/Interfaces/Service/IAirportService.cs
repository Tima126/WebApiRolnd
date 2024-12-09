using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IAirportService
    {
        Task<List<Airport>> GetAll();
        Task<Airport> GetById(int id);
        Task Create(Airport model);

        Task Update(Airport model);
        Task Delete(int id);
    }
}
