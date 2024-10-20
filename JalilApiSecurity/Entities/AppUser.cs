using Microsoft.AspNetCore.Identity;

namespace JalilApiSecurity.Entities
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public DateTime Brithdate { get; set; }


    }
}
