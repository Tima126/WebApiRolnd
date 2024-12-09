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
    public class CharterServiceTest
    {
        private readonly CharterService service;
        private readonly Mock<ICharterRepository> charterRepositoryMoq;

        public CharterServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            charterRepositoryMoq = new Mock<ICharterRepository>();

            repositoryWrapperMoq.Setup(x => x.Charter)
               .Returns(charterRepositoryMoq.Object);

            service = new CharterService(repositoryWrapperMoq.Object);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectCharters))]
        public async Task CreateAsync_NewCharterShouldNotCreateNewCharter(Charter charter)
        {
            // arrange
            var newCharter = charter;

            // act
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(newCharter));

            // assert
            Assert.IsType<ArgumentNullException>(ex);
            charterRepositoryMoq.Verify(x => x.Create(It.IsAny<Charter>()), Times.Never);
        }

        public static IEnumerable<object[]> GetIncorrectCharters()
        {
            return new List<object[]>
            {
                new object[] { new Charter { FlightId = null, CharterCompany = "" } },
                new object[] { new Charter { FlightId = 1, CharterCompany = "" } },
                new object[] { new Charter { FlightId = null, CharterCompany = "Test Company" } },
            };
        }

        [Fact]
        public async Task CreateAsyncNewCharterShouldNotCreateNewCharter()
        {
            var newCharter = new Charter
            {
                FlightId = 1,
                CharterCompany = "Test Company"
            };

            // act
            await service.Create(newCharter);

            // assert
            charterRepositoryMoq.Verify(x => x.Create(It.IsAny<Charter>()), Times.Once);
        }
    }
}