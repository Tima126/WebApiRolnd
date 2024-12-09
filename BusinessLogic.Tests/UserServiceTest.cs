using BusinessLogic.Sevices;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Wrapper;
using Moq;

namespace BusinessLogic.Tests
{
    public class UserServiceTest
     {
        private readonly UserService service;
        private readonly Mock<IUserRepository> userRepositoryMoq;
        public UserServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IUserRepository>();

            repositoryWrapperMoq.Setup(x => x.User)
               .Returns(userRepositoryMoq.Object);

            service = new UserService(repositoryWrapperMoq.Object); 
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUsers))]
        public async Task CreateAsync_NewUserShouldNotCreateNewUser(User user)
        {
            // arrange
            var newUser = user;

            //act
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(newUser));

            // assert
            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Never);

        }
        public static IEnumerable<object[]> GetIncorrectUsers()
        {
            return new List<object[]>
    {
            new object[] { new User { FirstName = "", LastName = "", Email = "", PasswordHash = "", RoleId = null } },
            new object[] { new User { FirstName = "Test", LastName = "", Email = "", PasswordHash = "", RoleId = null } },
            new object[] { new User { FirstName = "", LastName = "Test", Email = "", PasswordHash = "", RoleId = null } },
            new object[] { new User { FirstName = "", LastName = "", Email = "test@example.com", PasswordHash = "", RoleId = null } },
            new object[] { new User { FirstName = "", LastName = "", Email = "", PasswordHash = "TestPasswordHash", RoleId = null } },
            new object[] { new User { FirstName = "", LastName = "", Email = "", PasswordHash = "", RoleId = 1 } },
    };
        }


        [Fact]
        public async Task CreateAsyncNewUserSholdNotCreateNewUser()
        {
            var newUser = new User
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Email = "test@example.com",
                PasswordHash = "TestPasswordHash",
                RoleId = 1
            };

            // act
            await service.Create(newUser);

            // assert
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Once);

        }
    }
}
