using Microsoft.AspNetCore.Identity;

namespace ShopApi2024.Entities.Identity
{
    //united roles and users????????????
    public class RoleEntity : IdentityRole<int>
    {
        public virtual ICollection<UserRoleEntity>? UserRoles { get; set; }
    }
}
