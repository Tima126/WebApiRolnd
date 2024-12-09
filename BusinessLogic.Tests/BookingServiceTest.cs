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
    public class BookingServiceTest
    {
        private readonly BookingService service;
        private readonly Mock<IBookingRepository> bookingRepositoryMoq;

        public BookingServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            bookingRepositoryMoq = new Mock<IBookingRepository>();

            repositoryWrapperMoq.Setup(x => x.Booking)
               .Returns(bookingRepositoryMoq.Object);

            service = new BookingService(repositoryWrapperMoq.Object);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectBookings))]
        public async Task CreateAsync_NewBookingShouldNotCreateNewBooking(Booking booking)
        {
            // arrange
            var newBooking = booking;

            // act
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(newBooking));

            // assert
            Assert.IsType<ArgumentNullException>(ex);
            bookingRepositoryMoq.Verify(x => x.Create(It.IsAny<Booking>()), Times.Never);
        }

        public static IEnumerable<object[]> GetIncorrectBookings()
        {
            return new List<object[]>
            {
                new object[] { new Booking { UserId = null, FlightId = null, BookingDate = DateTime.MinValue, Status = "" } },
                new object[] { new Booking { UserId = 1, FlightId = null, BookingDate = DateTime.MinValue, Status = "" } },
                new object[] { new Booking { UserId = null, FlightId = 1, BookingDate = DateTime.MinValue, Status = "" } },
                new object[] { new Booking { UserId = null, FlightId = null, BookingDate = DateTime.Now, Status = "" } },
                new object[] { new Booking { UserId = null, FlightId = null, BookingDate = DateTime.MinValue, Status = "TestStatus" } },
            };
        }

        [Fact]
        public async Task CreateAsyncNewBookingShouldNotCreateNewBooking()
        {
            var newBooking = new Booking
            {
                UserId = 1,
                FlightId = 1,
                BookingDate = DateTime.Now,
                Status = "TestStatus"
            };

            // act
            await service.Create(newBooking);

            // assert
            bookingRepositoryMoq.Verify(x => x.Create(It.IsAny<Booking>()), Times.Once);
        }
    }
}