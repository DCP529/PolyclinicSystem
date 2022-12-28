using System.Text.Json.Serialization;

namespace Models
{
    public class City
    {
        public Guid CityId { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<Polyclinic> Polyclinics { get; set; } = new List<Polyclinic>(); 
    }
}