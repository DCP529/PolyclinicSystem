using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Models
{
    public class Doctor
    {
        public Guid DoctorId { get; set; }
        public string FIO { get; set; }       
        public decimal AdmissionCost { get; set; }
        public int ContactNumber { get; set; }
        [JsonIgnore]
        public IFormFile Image { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }

        [JsonIgnore]
        public List<Polyclinic> Polyclinics { get; set; } = new List<Polyclinic>();

        [JsonIgnore]
        public List<Specialization> Specializations { get; set; } = new List<Specialization>(); 
    }
}
