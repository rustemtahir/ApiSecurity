namespace JalilApiSecurity.Entities
{
    public class Car
    {
        public Guid CarId {  get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public CarColor Color { get; set; }
        public DateTime? UpdatedDate {  get; set; }
        public DateTime CreatedDate { get; set; }



    }

    public enum CarColor
    {
        Black,
        Red,
        Green,
        Yellow
    }
}
