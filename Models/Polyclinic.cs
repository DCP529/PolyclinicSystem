using Microsoft.AspNetCore.Http;

namespace Models
{
    public class Polyclinic
    {
        public Guid PolyclinicId { get; set; }
        public string Name { get; set; }        
        public string Address { get; set; }
        public int ContactNumber { get; set; }
        public IFormFile Image { get; set; }
        public City City { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
