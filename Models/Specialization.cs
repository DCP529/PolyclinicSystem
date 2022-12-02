
namespace Models
{
    public class Specialization
    {
        public Guid specializationId { get; set; }
        public string Name { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
