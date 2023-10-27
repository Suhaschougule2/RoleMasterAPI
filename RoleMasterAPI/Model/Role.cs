
using System.ComponentModel.DataAnnotations.Schema;

namespace RoleMasterAPI.Model
{
    [Table("Roles")]
    public class Role
    {
      
        public int Id { get; set; }

        public string Name { get; set; }

        public bool isActive { get; set; }

        public string Description { get; set; }
    }
}
