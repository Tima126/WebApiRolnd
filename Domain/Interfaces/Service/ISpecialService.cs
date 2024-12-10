using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface ISpecialService
    {
        Task<List<Special>> GetAll();
        Task<Special> GetById(int id);
        Task Create(Special model);

        Task Update(Special model);
        Task Delete(int id);
    }
}
