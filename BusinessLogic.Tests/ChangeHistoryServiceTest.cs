
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
    public class ChangerServiceTest
    {
        private readonly ChangerService service;
        private readonly Mock<IChangerRepository> changerRepositoryMoq;

        public ChangerServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            changerRepositoryMoq = new Mock<IChangerRepository>();

            repositoryWrapperMoq.Setup(x => x.Changer)
               .Returns(changerRepositoryMoq.Object);

            service = new ChangerService(repositoryWrapperMoq.Object);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectChanges))]
        public async Task CreateAsync_NewChangeShouldNotCreateNewChange(ChangeHistory changeHistory)
        {
            // arrange
            var newChange = changeHistory;

            // act
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(newChange));

            // assert
            Assert.IsType<ArgumentNullException>(ex);
            changerRepositoryMoq.Verify(x => x.Create(It.IsAny<ChangeHistory>()), Times.Never);
        }

        public static IEnumerable<object[]> GetIncorrectChanges()
        {
            return new List<object[]>
            {
                new object[] { new ChangeHistory { UserId = null, TableName = "", RecordId = 0, ChangeType = "", ChangeDate = DateTime.MinValue } },
                new object[] { new ChangeHistory { UserId = 1, TableName = "", RecordId = 0, ChangeType = "", ChangeDate = DateTime.MinValue } },
                new object[] { new ChangeHistory { UserId = null, TableName = "TestTable", RecordId = 0, ChangeType = "", ChangeDate = DateTime.MinValue } },
                new object[] { new ChangeHistory { UserId = null, TableName = "", RecordId = 1, ChangeType = "", ChangeDate = DateTime.MinValue } },
                new object[] { new ChangeHistory { UserId = null, TableName = "", RecordId = 0, ChangeType = "TestChange", ChangeDate = DateTime.MinValue } },
                new object[] { new ChangeHistory { UserId = null, TableName = "", RecordId = 0, ChangeType = "", ChangeDate = DateTime.Now } },
            };
        }

        [Fact]
        public async Task CreateAsyncNewChangeShouldNotCreateNewChange()
        {
            var newChange = new ChangeHistory
            {
                UserId = 1,
                TableName = "TestTable",
                RecordId = 1,
                ChangeType = "TestChange",
                ChangeDate = DateTime.Now
            };

            // act
            await service.Create(newChange);

            // assert
            changerRepositoryMoq.Verify(x => x.Create(It.IsAny<ChangeHistory>()), Times.Once);
        }
    }
}