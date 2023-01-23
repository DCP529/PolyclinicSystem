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
        public bool Archived { get; set; }

        [JsonIgnore]
        public virtual List<Polyclinic> Polyclinics { get; set; }

        [JsonIgnore]
        public virtual List<Specialization> Specializations { get; set; }

        public Doctor()
        {
            Specializations = new List<Specialization>();
            Polyclinics = new List<Polyclinic>();
        }
    }
}
