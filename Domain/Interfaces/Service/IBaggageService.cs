using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IBaggageService
    {
        Task<List<Baggage>> GetAll();
        Task<Baggage> GetById(int id);
        Task Create(Baggage model);

        Task Update(Baggage model);
        Task Delete(int id);
    }
}
