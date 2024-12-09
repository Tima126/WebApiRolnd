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
    public class ReviewService : IReviewService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ReviewService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Review>> GetAll()
        {
            return await _repositoryWrapper.Review.FindAll();
        }
        public async Task<Review> GetById(int id)
        {
            var review = await _repositoryWrapper.Review
                .FindByCondition(x => x.ReviewId == id);
            return review.First();
        }
        public async Task Create(Review model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.UserId == null)
            {
                throw new ArgumentException("UserId cannot be null.", nameof(model.UserId));
            }
            if (model.FlightId == null)
            {
                throw new ArgumentException("FlightId cannot be null.", nameof(model.FlightId));
            }
            if (model.Rating < 1 || model.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.", nameof(model.Rating));
            }
            if (model.ReviewDate == DateTime.MinValue)
            {
                throw new ArgumentException("ReviewDate cannot be the minimum value.", nameof(model.ReviewDate));
            }

            await _repositoryWrapper.Review.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Review model)
        {
            await _repositoryWrapper.Review.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var user = await _repositoryWrapper.User
                .FindByCondition(x => x.UserId == id);

            await _repositoryWrapper.User.Delete(user.First());
            await _repositoryWrapper.Save();
        }
    }
}
