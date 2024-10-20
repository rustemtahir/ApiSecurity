using System.ComponentModel.DataAnnotations;

namespace JalilApiSecurity.Models
{
    public class RegisterModel
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public DateTime BrithDate { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }    
    }
}
