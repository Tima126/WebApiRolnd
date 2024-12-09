using BusinessLogic.Sevices;

using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;

using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogic.Tests
{
    public class AirportServiceTest
    {
        private readonly AirportService service;
        private readonly Mock<IAirportRepository> airportRepositoryMoq;

        public AirportServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            airportRepositoryMoq = new Mock<IAirportRepository>();

            repositoryWrapperMoq.Setup(x => x.Airport)
               .Returns(airportRepositoryMoq.Object);

            service = new AirportService(repositoryWrapperMoq.Object);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectAirports))]
        public async Task CreateAsync_NewAirportShouldNotCreateNewAirport(Airport airport)
        {
            // arrange
            var newAirport = airport;

            // act
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(newAirport));

            // assert
            Assert.IsType<ArgumentNullException>(ex);
            airportRepositoryMoq.Verify(x => x.Create(It.IsAny<Airport>()), Times.Never);
        }

        public static IEnumerable<object[]> GetIncorrectAirports()
        {
            return new List<object[]>
            {
                new object[] { new Airport { AirportCode = "", AirportName = "", City = "", Country = "" } },
                new object[] { new Airport { AirportCode = "ABC", AirportName = "", City = "", Country = "" } },
                new object[] { new Airport { AirportCode = "", AirportName = "Test Airport", City = "", Country = "" } },
                new object[] { new Airport { AirportCode = "", AirportName = "", City = "Test City", Country = "" } },
                new object[] { new Airport { AirportCode = "", AirportName = "", City = "", Country = "Test Country" } },
            };
        }

        [Fact]
        public async Task CreateAsyncNewAirportShouldNotCreateNewAirport()
        {
            var newAirport = new Airport
            {
                AirportCode = "ABC",
                AirportName = "Test Airport",
                City = "Test City",
                Country = "Test Country"
            };

            // act
            await service.Create(newAirport);

            // assert
            airportRepositoryMoq.Verify(x => x.Create(It.IsAny<Airport>()), Times.Once);
        }
    }
}