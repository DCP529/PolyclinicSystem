namespace Models
{
    public class City
    {
        public Guid CityId { get; set; }
        public string Name { get; set; }
        public List<Polyclinic> Polyclinics { get; set; }
    }
}