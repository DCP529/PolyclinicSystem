
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    public class Specialization
    {
        public Guid SpecializationId { get; set; }
        public string Name { get; set; }
        public Guid DoctorId { get; set; }
        public int ExperienceSpecialization { get; set; }

        [JsonIgnore]
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
