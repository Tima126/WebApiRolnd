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
    public class ChangerService : IChangerService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ChangerService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<ChangeHistory>> GetAll()
        {
            return await _repositoryWrapper.Changer.FindAll();
        }

        public async Task<ChangeHistory> GetById(int id)
        {
            var change = await _repositoryWrapper.Changer
                .FindByCondition(x => x.ChangeId == id);
            return change.First();
        }

        public async Task Create(ChangeHistory model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.UserId == null)
            {
                throw new ArgumentException("UserId cannot be null.", nameof(model.UserId));
            }
            if (string.IsNullOrEmpty(model.TableName))
            {
                throw new ArgumentException("TableName cannot be null or empty.", nameof(model.TableName));
            }
            if (model.RecordId <= 0)
            {
                throw new ArgumentException("RecordId must be greater than 0.", nameof(model.RecordId));
            }
            if (string.IsNullOrEmpty(model.ChangeType))
            {
                throw new ArgumentException("ChangeType cannot be null or empty.", nameof(model.ChangeType));
            }
            if (model.ChangeDate == DateTime.MinValue)
            {
                throw new ArgumentException("ChangeDate cannot be the minimum value.", nameof(model.ChangeDate));
            }

            await _repositoryWrapper.Changer.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(ChangeHistory model)
        {
            await _repositoryWrapper.Changer.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var cange = await _repositoryWrapper.Changer
                .FindByCondition(x => x.ChangeId == id);

            await _repositoryWrapper.Changer.Delete(cange.First());
            await _repositoryWrapper.Save();
        }
    }
}
