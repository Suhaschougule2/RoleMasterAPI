using RoleMasterAPI.Model;

namespace RoleMasterAPI.Repositories
{
    public interface IRoleRepository
    {
        /* IEnumerable<Role> GetAllRoles();
         Role GetRoleById(int id);
         void AddRole(Role role);
         void UpdateRole(Role role);
         void DeleteRole(int id);
 */


        Task<IReadOnlyCollection<Role>> GetAllRolesAsync();
        Task<Role> GetRoleByIdAsync(int id);
        Task<int> AddRoleAsync(Role role);
        Task<int> UpdateRoleAsync(int id,Role role);
        Task<int> DeleteRoleAsync(int id);
        
    }



}

