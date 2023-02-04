using Microsoft.AspNetCore.Identity;

namespace BooksMvc.Models
{
    public class Role : IdentityRole
    {
        public const string Administrators = "Administrators";
        public const string Users = "Users";
    }
}
