using Microsoft.AspNetCore.Http;

namespace Models
{
    public class Doctor
    {
        public Guid DoctorId { get; set; }
        public string FIO { get; set; }       
        public decimal AdmissionCost { get; set; }
        public int ContactNumber { get; set; }
        public IFormFile Image { get; set; }
        public Dictionary<Specialization, int> ExperienceSpecialization { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public List<Polyclinic> Polyclinics { get; set; }
        public List<Specialization> Specializations { get; set; }
    }
}
