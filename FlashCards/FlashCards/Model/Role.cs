using Microsoft.AspNetCore.Identity;

namespace FlashCards.Model
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
