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
        Task<List<SpecialService>> GetAll();
        Task<SpecialService> GetById(int id);
        Task Create(SpecialService model);

        Task Update(SpecialService model);
        Task Delete(int id);
    }
}
