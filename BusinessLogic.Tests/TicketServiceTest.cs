
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
    public class TicketServiceTest
    {
        private readonly TicketService service;
        private readonly Mock<ITicketRepository> ticketRepositoryMoq;

        public TicketServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            ticketRepositoryMoq = new Mock<ITicketRepository>();

            repositoryWrapperMoq.Setup(x => x.Ticket)
               .Returns(ticketRepositoryMoq.Object);

            service = new TicketService(repositoryWrapperMoq.Object);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectTickets))]
        public async Task CreateAsync_NewTicketShouldNotCreateNewTicket(Ticket ticket)
        {
            // arrange
            var newTicket = ticket;

            // act
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(newTicket));

            // assert
            Assert.IsType<ArgumentNullException>(ex);
            ticketRepositoryMoq.Verify(x => x.Create(It.IsAny<Ticket>()), Times.Never);
        }

        public static IEnumerable<object[]> GetIncorrectTickets()
        {
            return new List<object[]>
            {
                new object[] { new Ticket { BookingId = null, SeatNumber = "", Class = "", Price = 0 } },
                new object[] { new Ticket { BookingId = 1, SeatNumber = "", Class = "", Price = 0 } },
                new object[] { new Ticket { BookingId = null, SeatNumber = "A1", Class = "", Price = 0 } },
                new object[] { new Ticket { BookingId = null, SeatNumber = "", Class = "Economy", Price = 0 } },
                new object[] { new Ticket { BookingId = null, SeatNumber = "", Class = "", Price = 150.00m } },
            };
        }

        [Fact]
        public async Task CreateAsyncNewTicketShouldNotCreateNewTicket()
        {
            var newTicket = new Ticket
            {
                BookingId = 1,
                SeatNumber = "A1",
                Class = "Economy",
                Price = 150.00m
            };

            // act
            await service.Create(newTicket);

            // assert
            ticketRepositoryMoq.Verify(x => x.Create(It.IsAny<Ticket>()), Times.Once);
        }
    }
}