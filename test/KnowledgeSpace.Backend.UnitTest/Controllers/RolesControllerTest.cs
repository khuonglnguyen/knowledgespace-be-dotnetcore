using KnowledgeSpace.Backend.Controllers;
using KnowledgeSpace.ViewModels.Systems;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
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
    }
}
