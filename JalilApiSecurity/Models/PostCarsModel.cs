using JalilApiSecurity.Entities;
using System.ComponentModel.DataAnnotations;

namespace JalilApiSecurity.Models
{
    public class PostCarsModel
    {
        [Required]
        public string Model { get; set; }

        [Range(3000,double.MaxValue)]
       public double Price { get; set; }
        public CarColor Color { get; set; }
    }
}
