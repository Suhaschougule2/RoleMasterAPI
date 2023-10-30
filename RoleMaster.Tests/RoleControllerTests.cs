using Microsoft.AspNetCore.Mvc;
using Moq;
using RoleMasterAPI.Controllers;
using RoleMasterAPI.Model;
using RoleMasterAPI.Services;
using System.Xml.Xsl;

namespace RoleMaster.Tests
{
    public class RoleControllerTests
    {
        [Fact]
        public async Task GetAllRoles_Returns_SingleRole_WhenServiceReturnsOneRole()
        {
            // Arrange
            var roleServiceMock = new Mock<IRoleService>();
            roleServiceMock.Setup(repo => repo.GetAllRolesAsync())
                .ReturnsAsync(new List<Role> { new Role { Id = 1, Name = "Admin", isActive = true } });

            var controller = new RoleController(roleServiceMock.Object);

            // Act
            var result = await controller.GetAllRoles();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var roles = Assert.IsAssignableFrom<IEnumerable<Role>>(okResult.Value);

            // Ensure that there is exactly one role in the collection
            Assert.Single(roles);
        }




        [Fact]
        public async Task GetRoleById_Returns_ResultWithRole()
        {
            // Arrange
            var roleServiceMock = new Mock<IRoleService>();
            roleServiceMock.Setup(repo => repo.GetRoleByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Role { Id = 1, Name = "Admin", isActive = true });

            var controller = new RoleController(roleServiceMock.Object);

            // Act
            var result = await controller.GetRoleById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var role = Assert.IsType<Role>(okResult.Value);
            Assert.Equal(1, role.Id); 
        }



        [Fact]
        public async Task AddRole_Returns_For_DuplicateName()
        {
            // Arrange
            var roleServiceMock = new Mock<IRoleService>();
            roleServiceMock.Setup(repo => repo.AddRoleAsync(It.IsAny<Role>()))
                .ReturnsAsync(-1); // Simulating conflict for duplicate name

            var controller = new RoleController(roleServiceMock.Object);

            // Act
            var result = await controller.AddRole(new Role { Name = "Admin", isActive = true });

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal("A role with the same Name already exists.", conflictResult.Value);
        }



       /* [Fact]
        public async Task UpdateRole_Returns_UpdatedRole()
        {
            // Arrange
            var roleServiceMock = new Mock<IRoleService>();
            roleServiceMock.Setup(repo => repo.GetRoleByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Role { Id = 1, Name = "Admin", isActive = true });

            var controller = new RoleController(roleServiceMock.Object);

            // Act
            var result = await controller.UpdateRole(1, new Role { Id = 1, Name = "SuperAdmin", isActive = true });

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedRole = Assert.IsType<Role>(okResult.Value);
            Assert.Equal("SuperAdmin", updatedRole.Name);
        }
*/
        [Fact]
        public async Task UpdateRole_Returns_OkResult_WithUpdatedRole()
        {
            // Arrange
            var roleServiceMock = new Mock<IRoleService>();
            roleServiceMock.Setup(repo => repo.UpdateRoleAsync(It.IsAny<int>(), It.IsAny<Role>()))
                .ReturnsAsync((int id, Role updatedRole) => id); // Returns the updated role ID

            var controller = new RoleController(roleServiceMock.Object);

            // Act
            var result = await controller.UpdateRole(1, new Role { Id = 1, Name = "SuperAdmin", isActive = true });

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedRoleId = Assert.IsType<int>(okResult.Value);

            // Add more assertions if needed
            Assert.Equal(1, updatedRoleId); // Ensure that the correct role ID is returned
        }

        [Fact]
        public async Task DeleteRole_Returns_NoContentResult()
        {
            // Arrange
            var roleServiceMock = new Mock<IRoleService>();
            roleServiceMock.Setup(repo => repo.DeleteRoleAsync(It.IsAny<int>()))
                .ReturnsAsync(1); 

            var controller = new RoleController(roleServiceMock.Object);
            var roleIdToDelete = 1;

            // Act
            var result = await controller.DeleteRole(roleIdToDelete);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var deletedRoleId = Assert.IsType<int>(okResult.Value);
            Assert.Equal(roleIdToDelete, deletedRoleId);

            roleServiceMock.Verify(repo => repo.DeleteRoleAsync(roleIdToDelete), Times.Once);
        }


    }
}