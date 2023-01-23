using System.Text.Json.Serialization;

namespace Models
{
    public class City
    {
        public Guid CityId { get; set; }
        public string Name { get; set; }
        public bool Archived { get; set; }

        [JsonIgnore]
        public virtual List<Polyclinic> Polyclinics { get; set; } 

        public City()
        {
            Polyclinics = new List<Polyclinic>();
        }
    }
}