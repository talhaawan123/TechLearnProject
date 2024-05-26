using Microsoft.AspNetCore.Identity;

namespace Myshowroom.Models.NewFolder
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
