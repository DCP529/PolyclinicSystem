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
        public IFormFile Image { get; set; }
        public Guid CityId { get; set; }
        public bool Archived { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }

        public Polyclinic()
        {
            Doctors = new List<Doctor>();
        }
    }
}
