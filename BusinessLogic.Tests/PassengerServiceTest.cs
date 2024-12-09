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
    public class PassengerServiceTest
    {
        private readonly PassengerService service;
        private readonly Mock<IPassengerRepository> passengerRepositoryMoq;

        public PassengerServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            passengerRepositoryMoq = new Mock<IPassengerRepository>();

            repositoryWrapperMoq.Setup(x => x.Passenger)
               .Returns(passengerRepositoryMoq.Object);

            service = new PassengerService(repositoryWrapperMoq.Object);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectPassengers))]
        public async Task CreateAsync_NewPassengerShouldNotCreateNewPassenger(Passenger passenger)
        {
            // arrange
            var newPassenger = passenger;

            // act
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(newPassenger));

            // assert
            Assert.IsType<ArgumentNullException>(ex);
            passengerRepositoryMoq.Verify(x => x.Create(It.IsAny<Passenger>()), Times.Never);
        }

        public static IEnumerable<object[]> GetIncorrectPassengers()
        {
            return new List<object[]>
            {
                new object[] { new Passenger { FirstName = "", LastName = "", PassportNumber = "", DateOfBirth = DateTime.MinValue } },
                new object[] { new Passenger { FirstName = "Test", LastName = "", PassportNumber = "", DateOfBirth = DateTime.MinValue } },
                new object[] { new Passenger { FirstName = "", LastName = "Test", PassportNumber = "", DateOfBirth = DateTime.MinValue } },
                new object[] { new Passenger { FirstName = "", LastName = "", PassportNumber = "123456789", DateOfBirth = DateTime.MinValue } },
                new object[] { new Passenger { FirstName = "", LastName = "", PassportNumber = "", DateOfBirth = DateTime.Now } },
            };
        }

        [Fact]
        public async Task CreateAsyncNewPassengerShouldNotCreateNewPassenger()
        {
            var newPassenger = new Passenger
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                PassportNumber = "123456789",
                DateOfBirth = new DateTime(1990, 1, 1)
            };

            // act
            await service.Create(newPassenger);

            // assert
            passengerRepositoryMoq.Verify(x => x.Create(It.IsAny<Passenger>()), Times.Once);
        }
    }
}