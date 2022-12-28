using Microsoft.AspNetCore.Http;
using Models.ModelsDb;
using System.Text.Json.Serialization;

namespace Models
{
    public class Polyclinic
    {
        public Guid PolyclinicId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int ContactNumber { get; set; }
        [JsonIgnore]
        public IFormFile Image { get; set; }
        public Guid CityId { get; set; }
        public Guid DoctorId { get; set; }

        [JsonIgnore]
        public City City { get; set; }

        [JsonIgnore]
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
