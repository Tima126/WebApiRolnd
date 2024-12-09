
using BusinessLogic.Sevices;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Wrapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogic.Tests
{
    public class ReviewServiceTest
    {
        private readonly ReviewService service;
        private readonly Mock<IReviewRepository> reviewRepositoryMoq;

        public ReviewServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            reviewRepositoryMoq = new Mock<IReviewRepository>();

            repositoryWrapperMoq.Setup(x => x.Review)
               .Returns(reviewRepositoryMoq.Object);

            service = new ReviewService(repositoryWrapperMoq.Object);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectReviews))]
        public async Task CreateAsync_NewReviewShouldNotCreateNewReview(Review review)
        {
            // arrange
            var newReview = review;

            // act
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(newReview));

            // assert
            Assert.IsType<ArgumentNullException>(ex);
            reviewRepositoryMoq.Verify(x => x.Create(It.IsAny<Review>()), Times.Never);
        }

        public static IEnumerable<object[]> GetIncorrectReviews()
        {
            return new List<object[]>
            {
                new object[] { new Review { UserId = null, FlightId = null, Rating = 0, Comment = null, ReviewDate = DateTime.MinValue } },
                new object[] { new Review { UserId = 1, FlightId = null, Rating = 0, Comment = null, ReviewDate = DateTime.MinValue } },
                new object[] { new Review { UserId = null, FlightId = 1, Rating = 0, Comment = null, ReviewDate = DateTime.MinValue } },
                new object[] { new Review { UserId = null, FlightId = null, Rating = 5, Comment = null, ReviewDate = DateTime.MinValue } },
                new object[] { new Review { UserId = null, FlightId = null, Rating = 0, Comment = "Test Comment", ReviewDate = DateTime.MinValue } },
                new object[] { new Review { UserId = null, FlightId = null, Rating = 0, Comment = null, ReviewDate = DateTime.Now } },
            };
        }

        [Fact]
        public async Task CreateAsyncNewReviewShouldNotCreateNewReview()
        {
            var newReview = new Review
            {
                UserId = 1,
                FlightId = 1,
                Rating = 5,
                Comment = "Test Comment",
                ReviewDate = DateTime.Now
            };

            // act
            await service.Create(newReview);

            // assert
            reviewRepositoryMoq.Verify(x => x.Create(It.IsAny<Review>()), Times.Once);
        }
    }
}