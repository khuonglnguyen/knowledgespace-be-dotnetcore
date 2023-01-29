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
        public RolesControllerTest()
        {
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);
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
            var roles = new List<IdentityRole>()
            {
                 new IdentityRole("test1"),
                new IdentityRole("test2"),
            }.AsAsyncQueryable();
            _mockRoleManager.Setup(x => x.Roles).Returns(roles);
            var rolesControler = new RolesController(_mockRoleManager.Object);

            var result = await rolesControler.GetRoles();

            var okResult = result as OkObjectResult;
            var roleVms=okResult.Value as IEnumerable<RoleVm>;
            Assert.True(roleVms.Count() > 0);
        }

        [Fact]
        public async Task GetRole_ThrowException_Failed()
        {
            _mockRoleManager.Setup(x => x.Roles).Throws<ArgumentException>();
            var rolesControler = new RolesController(_mockRoleManager.Object);

            await Assert.ThrowsAnyAsync<Exception>(async () => await rolesControler.GetRoles());
        }
    }
}
