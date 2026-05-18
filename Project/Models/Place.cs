namespace Project.Models
{
    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PhotoUrl { get; set; }
        public string Category { get; set; }
    }
}