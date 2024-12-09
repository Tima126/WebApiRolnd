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
    public class RoleServiceTest
    {
        private readonly RoleService service;
        private readonly Mock<IRoleRepository> roleRepositoryMoq;

        public RoleServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            roleRepositoryMoq = new Mock<IRoleRepository>();

            repositoryWrapperMoq.Setup(x => x.Role)
               .Returns(roleRepositoryMoq.Object);

            service = new RoleService(repositoryWrapperMoq.Object);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectRoles))]
        public async Task CreateAsync_NewRoleShouldNotCreateNewRole(Role role)
        {
            // arrange
            var newRole = role;

            // act
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(newRole));

            // assert
            Assert.IsType<ArgumentNullException>(ex);
            roleRepositoryMoq.Verify(x => x.Create(It.IsAny<Role>()), Times.Never);
        }

        public static IEnumerable<object[]> GetIncorrectRoles()
        {
            return new List<object[]>
            {
                new object[] { new Role { RoleName = "" } },
                new object[] { new Role { RoleName = null } },
                new object[] { new Role { RoleName = " " } },
            };
        }

        [Fact]
        public async Task CreateAsyncNewRoleShouldNotCreateNewRole()
        {
            var newRole = new Role
            {
                RoleName = "TestRole"
            };

            // act
            await service.Create(newRole);

            // assert
            roleRepositoryMoq.Verify(x => x.Create(It.IsAny<Role>()), Times.Once);
        }
    }
}