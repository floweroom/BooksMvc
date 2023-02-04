using Microsoft.AspNetCore.Identity;

namespace BooksMvc.Models
{
    public class User : IdentityUser
    {
        public const string Administrator = "Admin";
        public const string AdminPassword = "AdPAss_123";

        public override string ToString() => UserName;
    }
}
