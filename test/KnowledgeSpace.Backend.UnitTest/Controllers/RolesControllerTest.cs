using KnowledgeSpace.Backend.Controllers;
using KnowledgeSpace.Backend.UnitTest.Extensions;
using KnowledgeSpace.ViewModels.Systems;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KnowledgeSpace.Backend.UnitTest.Controllers
{
    public class RolesControllerTest
    {
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private readonly List<IdentityRole> _roles;
        public RolesControllerTest()
        {
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);

            _roles = new List<IdentityRole>()
            {
                 new IdentityRole("test1"),
                new IdentityRole("test2"),
                new IdentityRole("test3"),
                new IdentityRole("test4"),
            };
        }

        [Fact]
        public void ShouldCreateInstance_NotNull_Success()
        {
            var rolesControler = new RolesController(_mockRoleManager.Object);

            Assert.NotNull(rolesControler);
        }

        [Fact]
        public async Task PostRole_ValidInput_Success()
        {
            _mockRoleManager.Setup(x => x.CreateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Success);
            var rolesControler = new RolesController(_mockRoleManager.Object);

            var result = await rolesControler.PostRole(new RoleVm()
            {
                Id = "test",
                Name = "test",
            });

            Assert.NotNull(rolesControler);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task PostRole_ValidInput_Failed()
        {
            _mockRoleManager.Setup(x => x.CreateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Failed(new IdentityError[] { }));
            var rolesControler = new RolesController(_mockRoleManager.Object);

            var result = await rolesControler.PostRole(new RoleVm()
            {
                Id = "test",
                Name = "test",
            });

            Assert.NotNull(rolesControler);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetRole_HasData_ReturnSuccess()
        {
            _mockRoleManager.Setup(x => x.Roles).Returns(_roles.AsAsyncQueryable());
            var rolesControler = new RolesController(_mockRoleManager.Object);

            var result = await rolesControler.GetRoles();

            var okResult = result as OkObjectResult;
            var roleVms = okResult.Value as IEnumerable<RoleVm>;
            Assert.True(roleVms.Count() > 0);
        }

        [Fact]
        public async Task GetRole_ThrowException_Failed()
        {
            _mockRoleManager.Setup(x => x.Roles).Throws<ArgumentException>();
            var rolesControler = new RolesController(_mockRoleManager.Object);

            await Assert.ThrowsAnyAsync<Exception>(async () => await rolesControler.GetRoles());
        }

        [Fact]
        public async Task GetRolesPaging_NoFilter_ReturnSuccess()
        {
            _mockRoleManager.Setup(x => x.Roles).Returns(_roles.AsAsyncQueryable());
            var rolesControler = new RolesController(_mockRoleManager.Object);

            var result = await rolesControler.GetRolesPaging(null, 1, 2);

            var okResult = result as OkObjectResult;
            var roleVms = okResult.Value as Pagination<RoleVm>;
            Assert.Equal(4, roleVms.TotalRecords);
            Assert.Equal(2, roleVms.Items.Count);
        }

        [Fact]
        public async Task GetRolesPaging_HasFilter_ReturnSuccess()
        {
            _mockRoleManager.Setup(x => x.Roles).Returns(_roles.AsAsyncQueryable());
            var rolesControler = new RolesController(_mockRoleManager.Object);

            var result = await rolesControler.GetRolesPaging("test1", 1, 2);

            var okResult = result as OkObjectResult;
            var roleVms = okResult.Value as Pagination<RoleVm>;
            Assert.Equal(1, roleVms.TotalRecords);
            Assert.Single(roleVms.Items);
        }

        [Fact]
        public async Task GetRolesPaging_NoFilter_Failed()
        {
            _mockRoleManager.Setup(x => x.Roles).Throws<ArgumentException>();
            var rolesControler = new RolesController(_mockRoleManager.Object);

            await Assert.ThrowsAnyAsync<Exception>(async () => await rolesControler.GetRolesPaging(null, 1, 2));
        }

        [Fact]
        public async Task GetById_HasData_ReturnSuccess()
        {
            _mockRoleManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new IdentityRole()
            {
                Id = "test1",
                Name = "test1"
            });
            var rolesControler = new RolesController(_mockRoleManager.Object);
            var result = await rolesControler.GetById("test1");
            var okResult = result as OkObjectResult;
            var roleVm = okResult.Value as RoleVm;

            Assert.Equal("test1", roleVm.Name);
        }

        [Fact]
        public async Task GetById_ThrowException_Failed()
        {
            _mockRoleManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).Throws<Exception>();
            var rolesControler = new RolesController(_mockRoleManager.Object);

            await Assert.ThrowsAnyAsync<Exception>(async () => await rolesControler.GetById("test1"));
        }
    }
}
