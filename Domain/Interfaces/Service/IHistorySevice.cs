using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IHistorySevice
    {
        Task<List<ChangeHistory>> GetAll();
        Task<ChangeHistory> GetById(int id);
        Task Create(ChangeHistory model);

        Task Update(ChangeHistory model);
        Task Delete(int id);
    }
}
