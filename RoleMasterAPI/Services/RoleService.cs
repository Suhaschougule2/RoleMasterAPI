using RoleMasterAPI.Data;
using RoleMasterAPI.Model;
using RoleMasterAPI.Repositories;

namespace RoleMasterAPI.Services
{
public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

        /* public Role GetRoleById(int id)
         {
             return _roleRepository.GetRoleById(id);
         }

         public IEnumerable<Role> GetAllRoles()
         {
             return _roleRepository.GetAllRoles();
         }

         public void AddRole(Role role)
         {
            _roleRepository.AddRole(role);
         }

         public void UpdateRole(Role role)
         {
             _roleRepository.UpdateRole(role);
         }

         public void DeleteRole(int id)
         {
             _roleRepository.DeleteRole(id);
         }
     */

        public async Task<IReadOnlyCollection<Role>> GetAllRolesAsync()
        {
            return await _roleRepository.GetAllRolesAsync();
        }
        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _roleRepository.GetRoleByIdAsync(id);
        }

        public async Task<int> AddRoleAsync(Role role)
        {
            return await _roleRepository.AddRoleAsync(role);
        }

        public async Task<int> UpdateRoleAsync(int id, Role role)
        {
            return await _roleRepository.UpdateRoleAsync(id, role);
        }

        public async Task<int> DeleteRoleAsync(int id) 
        {
            return await _roleRepository.DeleteRoleAsync(id);
        }

  
    }

}
