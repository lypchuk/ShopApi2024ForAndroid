using Microsoft.AspNetCore.Identity;

namespace ShopApi2024.Entities.Identity
{
    //united role and user
    public class UserRoleEntity : IdentityUserRole<int>
    {
        public virtual UserEntity User { get; set; } = new();
        public virtual RoleEntity Role { get; set; } = new();
    }
}
