using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.ModelsDb
{
    [Table(name: "specialization")]
    public class SpecializationDb
    {
        [Key]
        [Column("specialization_id")]
        public Guid SpecializationId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("doctor_id")]
        public Guid DoctorId { get; set; }

        [Column("experience_specialization")]
        public int ExperienceSpecialization { get; set; }

        [Column("archived")]
        public bool Archived { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public ICollection<DoctorDb> Doctors { get; set; }

        public SpecializationDb()
        {
            Doctors = new List<DoctorDb>();
        }
    }
}
