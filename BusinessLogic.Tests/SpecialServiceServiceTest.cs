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
    public class SpecialServiceServiceTest
    {
        private readonly SpecialService2 service;
        private readonly Mock<ISpecialRepository> serviceRepositoryMoq;

        public SpecialServiceServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            serviceRepositoryMoq = new Mock<ISpecialRepository>();

            repositoryWrapperMoq.Setup(x => x.Special)
               .Returns(serviceRepositoryMoq.Object);

            service = new SpecialService2(repositoryWrapperMoq.Object);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectServices))]
        public async Task CreateAsync_NewServiceShouldNotCreateNewService(SpecialService service)
        {
            // arrange
            var newService = service;

            // act
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => this.service.Create(newService));

            // assert
            Assert.IsType<ArgumentNullException>(ex);
            serviceRepositoryMoq.Verify(x => x.Create(It.IsAny<SpecialService>()), Times.Never);
        }

        public static IEnumerable<object[]> GetIncorrectServices()
        {
            return new List<object[]>
            {
                new object[] { new SpecialService { ServiceName = "", Description = null, Bookings = null } },
                new object[] { new SpecialService { ServiceName = "TestService", Description = null, Bookings = null } },
                new object[] { new SpecialService { ServiceName = "", Description = "TestDescription", Bookings = null } },
                new object[] { new SpecialService { ServiceName = "", Description = null, Bookings = new List<Booking>() } },
            };
        }

        [Fact]
        public async Task CreateAsyncNewServiceShouldNotCreateNewService()
        {
            var newService = new SpecialService
            {
                ServiceName = "TestService",
                Description = "TestDescription",
                Bookings = new List<Booking>()
            };

            // act
            await service.Create(newService);

            // assert
            serviceRepositoryMoq.Verify(x => x.Create(It.IsAny<SpecialService>()), Times.Once);
        }
    }
}