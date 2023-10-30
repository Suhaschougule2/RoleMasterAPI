using Microsoft.EntityFrameworkCore;
using RoleMasterAPI.Data;
using RoleMasterAPI.Migrations;
using RoleMasterAPI.Model;


namespace RoleMasterAPI.Repositories
{
   

    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _dbContext;

        public RoleRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /* public RoleRepository (AppDbContext appdbContext)
         {
             _appdbContext = appdbContext;
         }
         public void AddRole(Role role)
         {
             _appdbContext.Roles.Add(role);
             _appdbContext.SaveChanges();
         }

         public void DeleteRole(int id)
         {
             var roleToDelete = _appdbContext.Roles.Find(id);

             if (roleToDelete != null)
             {
                 _appdbContext.Roles.Remove(roleToDelete);
                 _appdbContext.SaveChanges();
             }
         }

         public IEnumerable<Role> GetAllRoles()
         {
             return _appdbContext.Roles.ToList();
         }

         public Role GetRoleById(int id)
         {
             return _appdbContext.Roles.FirstOrDefault(r => r.Id == id);
         }

         public void UpdateRole(Role role)
         {
             var existingRole = _appdbContext.Roles.Find(role.Id);

             if (existingRole != null)
             {
                 existingRole.Name = role.Name;
                 existingRole.isActive = role.isActive;

                 _appdbContext.SaveChanges();
             }
         }
 */


        //public RoleRepository(AppDbContext appdbContext)
        //{
        //    _appdbContext = appdbContext;
        //}

        public async Task<IReadOnlyCollection<Role>> GetAllRolesAsync()
        {
            try
            {
                return await _dbContext.Roles.ToListAsync();
            }
            catch (Exception ex)
            {
               Console.Error.WriteLine($"An error occurred while retrieving roles: {ex.Message}");
               throw ;
            }
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            try
            {
                return await _dbContext.Roles.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred while retrieving role by ID {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<int> AddRoleAsync(Role role)
        {
            /*try
            {
                _appdbContext.Roles.Add(role);
                await _appdbContext.SaveChangesAsync();
                return role.Id;
            }*/

             try
             {
                /*if (await _dbContext.Roles.AnyAsync(r => r.Name == role.Name))
                {
                    Console.Error.WriteLine("A role with the same Name already exists.");
                    return -1;
                }*/

                _dbContext.Roles.Add(role);
                await _dbContext.SaveChangesAsync();
                return role.Id;
             }


            catch (Exception ex)
             {
                Console.Error.WriteLine($"An error occurred while adding a role: {ex.Message}");
                return -1; 
             }
        }



        public async Task<int> UpdateRoleAsync(int id, Role role)
        {
            try
            {
                var existingRole = await _dbContext.Roles.FindAsync(id);

              /*  if (existingRole == null)
                {
                    return -1; // Indicates that the role with the given ID was not found.
                }

                // Check for duplicate name
                if (await _dbContext.Roles.AnyAsync(r => r.Name == role.Name && r.Id != id))
                {
                    return -1; // Indicates a duplicate role name.
                }*/

                existingRole.Name = role.Name;
                existingRole.isActive = role.isActive; 

               
                await _dbContext.SaveChangesAsync();
                return existingRole.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<int> DeleteRoleAsync(int id)
        {
            try
            {
                var role = await _dbContext.Roles.FindAsync(id);
                if (role == null)
                {
                    return 0;
                }
                _dbContext.Roles.Remove(role);
                return await _dbContext.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred while retrieving roles: {ex.Message}");
                throw ex;
            }
        }

        
    }
}
