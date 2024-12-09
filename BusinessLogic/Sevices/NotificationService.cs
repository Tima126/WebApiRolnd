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
    public class NotificationService : INotificationService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public NotificationService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Notification>> GetAll()
        {
            return await _repositoryWrapper.Notification.FindAll();
        }

        public async Task<Notification> GetById(int id)
        {
            var notification = await _repositoryWrapper.Notification
                .FindByCondition(x => x.NotificationId == id);
            return notification.First();
        }

        public async Task Create(Notification model)
        {
            await _repositoryWrapper.Notification.Create(model);
            await  _repositoryWrapper.Save();
        }

        public async Task Update(Notification model)
        {
            await _repositoryWrapper.Notification.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var notification = await _repositoryWrapper.Notification
                .FindByCondition(x => x.NotificationId == id);

            await _repositoryWrapper.Notification.Delete(notification.First());
            await _repositoryWrapper.Save();
        }
    }
}
