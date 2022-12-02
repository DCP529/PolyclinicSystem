
namespace Models
{
    public class Specialization
    {
        public Guid SpecializationId { get; set; }
        public string Name { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
