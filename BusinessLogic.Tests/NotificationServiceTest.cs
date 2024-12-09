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
    public class NotificationServiceTest
    {
        private readonly NotificationService service;
        private readonly Mock<INotificationRepository> notificationRepositoryMoq;

        public NotificationServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            notificationRepositoryMoq = new Mock<INotificationRepository>();

            repositoryWrapperMoq.Setup(x => x.Notification)
               .Returns(notificationRepositoryMoq.Object);

            service = new NotificationService(repositoryWrapperMoq.Object);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectNotifications))]
        public async Task CreateAsync_NewNotificationShouldNotCreateNewNotification(Notification notification)
        {
            // arrange
            var newNotification = notification;

            // act
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(newNotification));

            // assert
            Assert.IsType<ArgumentNullException>(ex);
            notificationRepositoryMoq.Verify(x => x.Create(It.IsAny<Notification>()), Times.Never);
        }

        public static IEnumerable<object[]> GetIncorrectNotifications()
        {
            return new List<object[]>
            {
                new object[] { new Notification { UserId = null, Message = "", NotificationDate = DateTime.MinValue, IsRead = null } },
                new object[] { new Notification { UserId = 1, Message = "", NotificationDate = DateTime.MinValue, IsRead = null } },
                new object[] { new Notification { UserId = null, Message = "Test Message", NotificationDate = DateTime.MinValue, IsRead = null } },
                new object[] { new Notification { UserId = null, Message = "", NotificationDate = DateTime.Now, IsRead = null } },
                new object[] { new Notification { UserId = null, Message = "", NotificationDate = DateTime.MinValue, IsRead = true } },
            };
        }

        [Fact]
        public async Task CreateAsyncNewNotificationShouldNotCreateNewNotification()
        {
            var newNotification = new Notification
            {
                UserId = 1,
                Message = "Test Message",
                NotificationDate = DateTime.Now,
                IsRead = false
            };

            // act
            await service.Create(newNotification);

            // assert
            notificationRepositoryMoq.Verify(x => x.Create(It.IsAny<Notification>()), Times.Once);
        }
    }
}