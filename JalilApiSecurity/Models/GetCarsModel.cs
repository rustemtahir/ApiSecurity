using JalilApiSecurity.Entities;

namespace JalilApiSecurity.Models
{
    public class GetCarsModel
    {
        public Guid CarId { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
    
    }
}
