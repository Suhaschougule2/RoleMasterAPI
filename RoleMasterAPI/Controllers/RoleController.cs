using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleMasterAPI.Model;
using RoleMasterAPI.Services;

namespace RoleMasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;



        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [HttpGet("Test")]
        public IActionResult Test() {


            return Ok("Tets");
        }


     /*   [HttpGet("GetAllRoles")]
        public async Task<IEnumerable<Role>> GetAllRoles()
        {

            var roles = await _roleService.GetAllRolesAsync();
            return roles;
        }*/
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAllRolesAsync();
                return Ok(roles); // Ensure you return Ok() with the roles
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate result, e.g., InternalServerError
                Console.Error.WriteLine($"An error occurred while retrieving roles: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }



        [HttpGet("GetRoleById/{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }


        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] Role role)
        {
            var roleId = await _roleService.AddRoleAsync(role);

            if (roleId == -1)
            {
                return Conflict("A role with the same Name already exists.");
            }

            return CreatedAtAction(nameof(GetRoleById), new { id = roleId }, role);
        }


        [HttpPut("UpdateRole/{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] Role role)
        {
            try
            {
                var updatedRoleId = await _roleService.UpdateRoleAsync(id, role);

                if (updatedRoleId == -1)
                {
                    return NotFound();
                }

                return Ok(updatedRoleId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating a role: {ex.Message}");
            }
        }


        [HttpDelete("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            if (result == 0)
            {
                // Role with the given ID not found
                return NotFound();
            }

            return Ok(result);
        }

    }
}
