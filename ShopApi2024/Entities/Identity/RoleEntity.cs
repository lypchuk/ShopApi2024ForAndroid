using Microsoft.AspNetCore.Identity;

namespace ShopApi2024.Entities.Identity
{
    public class RoleEntity : IdentityRole<int>
    {
        public virtual ICollection<UserRoleEntity>? UserRoles { get; set; }
    }
}
