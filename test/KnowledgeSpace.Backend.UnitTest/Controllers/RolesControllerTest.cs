using KnowledgeSpace.Backend.Controllers;
using Microsoft.AspNetCore.Identity;
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
        [Fact]
        public void RolesController_ShouldCreateInstance_NotNull()
        {
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            var mockRoleManager = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);
            var rolesControler = new RolesController(mockRoleManager.Object);

            Assert.NotNull(rolesControler);
        }
    }
}
