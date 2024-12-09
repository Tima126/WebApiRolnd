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
    public class FlightServiceTest
    {
        private readonly FlightService service;
        private readonly Mock<IFlightRepository> flightRepositoryMoq;

        public FlightServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            flightRepositoryMoq = new Mock<IFlightRepository>();

            repositoryWrapperMoq.Setup(x => x.Flight)
               .Returns(flightRepositoryMoq.Object);

            service = new FlightService(repositoryWrapperMoq.Object);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectFlights))]
        public async Task CreateAsync_NewFlightShouldNotCreateNewFlight(Flight flight)
        {
            // arrange
            var newFlight = flight;

            // act
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(newFlight));

            // assert
            Assert.IsType<ArgumentNullException>(ex);
            flightRepositoryMoq.Verify(x => x.Create(It.IsAny<Flight>()), Times.Never);
        }

        public static IEnumerable<object[]> GetIncorrectFlights()
        {
            return new List<object[]>
            {
                new object[] { new Flight { FlightNumber = "", DepartureAirportId = null, ArrivalAirportId = null, DepartureTime = DateTime.MinValue, ArrivalTime = DateTime.MinValue } },
                new object[] { new Flight { FlightNumber = "FL123", DepartureAirportId = null, ArrivalAirportId = null, DepartureTime = DateTime.MinValue, ArrivalTime = DateTime.MinValue } },
                new object[] { new Flight { FlightNumber = "", DepartureAirportId = 1, ArrivalAirportId = null, DepartureTime = DateTime.MinValue, ArrivalTime = DateTime.MinValue } },
                new object[] { new Flight { FlightNumber = "", DepartureAirportId = null, ArrivalAirportId = 1, DepartureTime = DateTime.MinValue, ArrivalTime = DateTime.MinValue } },
                new object[] { new Flight { FlightNumber = "", DepartureAirportId = null, ArrivalAirportId = null, DepartureTime = DateTime.Now, ArrivalTime = DateTime.MinValue } },
                new object[] { new Flight { FlightNumber = "", DepartureAirportId = null, ArrivalAirportId = null, DepartureTime = DateTime.MinValue, ArrivalTime = DateTime.Now } },
            };
        }

        [Fact]
        public async Task CreateAsyncNewFlightShouldNotCreateNewFlight()
        {
            var newFlight = new Flight
            {
                FlightNumber = "FL123",
                DepartureAirportId = 1,
                ArrivalAirportId = 2,
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(2)
            };

            // act
            await service.Create(newFlight);

            // assert
            flightRepositoryMoq.Verify(x => x.Create(It.IsAny<Flight>()), Times.Once);
        }
    }
}