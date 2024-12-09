using Domain.Interfaces;
using Domain.Interfaces.Service;
using Domain.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Sevices
{
    public class TicketService : ITicketService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public TicketService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Ticket>> GetAll()
        {
            return await _repositoryWrapper.Ticket.FindAll();
        }
        public async Task<Ticket> GetById(int id)
        {
            var ticket = await _repositoryWrapper.Ticket
                .FindByCondition(x => x.TicketId == id);
            return ticket.First();
        }
        public async Task Create(Ticket model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.SeatNumber))
            {
                throw new ArgumentNullException(nameof(model.SeatNumber));
            }
            if (string.IsNullOrEmpty(model.Class))
            {
                throw new ArgumentNullException(nameof(model.Class));
            }
            await _repositoryWrapper.Ticket.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Ticket model)
        {
            await _repositoryWrapper.Ticket.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var ticket = await _repositoryWrapper.Ticket
                .FindByCondition(x => x.TicketId == id);

            await _repositoryWrapper.Ticket.Delete(ticket.First());
                await _repositoryWrapper.Save();
        }
    }
}
