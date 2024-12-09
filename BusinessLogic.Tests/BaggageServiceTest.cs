using BusinessLogic.Sevices;
using Domain.Interface;
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
    public class BaggageServiceTest
    {
        private readonly BaggageService service;
        private readonly Mock<IBaggageRepository> baggageRepositoryMoq;

        public BaggageServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            baggageRepositoryMoq = new Mock<IBaggageRepository>();

            repositoryWrapperMoq.Setup(x => x.Baggage)
               .Returns(baggageRepositoryMoq.Object);

            service = new BaggageService(repositoryWrapperMoq.Object);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectBaggages))]
        public async Task CreateAsync_NewBaggageShouldNotCreateNewBaggage(Baggage baggage)
        {
            // arrange
            var newBaggage = baggage;

            // act
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(newBaggage));

            // assert
            Assert.IsType<ArgumentNullException>(ex);
            baggageRepositoryMoq.Verify(x => x.Create(It.IsAny<Baggage>()), Times.Never);
        }

        public static IEnumerable<object[]> GetIncorrectBaggages()
        {
            return new List<object[]>
            {
                new object[] { new Baggage { PassengerId = null, Weight = 0, Status = "" } },
                new object[] { new Baggage { PassengerId = 1, Weight = 0, Status = "" } },
                new object[] { new Baggage { PassengerId = null, Weight = 10, Status = "" } },
                new object[] { new Baggage { PassengerId = null, Weight = 0, Status = "TestStatus" } },
            };
        }

        [Fact]
        public async Task CreateAsyncNewBaggageShouldNotCreateNewBaggage()
        {
            var newBaggage = new Baggage
            {
                PassengerId = 1,
                Weight = 10,
                Status = "TestStatus"
            };

            // act
            await service.Create(newBaggage);

            // assert
            baggageRepositoryMoq.Verify(x => x.Create(It.IsAny<Baggage>()), Times.Once);
        }
    }
}