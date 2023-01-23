using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.ModelsDb
{
    [Table(name: "doctror")]
    public class DoctorDb
    {
        [Key]
        [Column("doctor_id")]
        public Guid DoctorId { get; set; }

        [Column("fio")]
        public string FIO { get; set; }

        [Column("admission_cost")]
        public decimal AdmissionCost { get; set; }

        [Column("contact_number")]
        public int ContactNumber { get; set; }

        [Column("image_path")]
        public string ImagePath { get; set; }     

        [Column("short_description")]
        public string ShortDescription { get; set; }       
        
        [Column("full_description")]
        public string FullDescription { get; set; }

        [Column("archived")]
        public bool Archived { get; set; }

        public virtual ICollection<PolyclinicDb> Polyclinics { get; set; } 
        public virtual ICollection<SpecializationDb> Specializations { get; set; }

        public DoctorDb()
        {
            Polyclinics = new List<PolyclinicDb>();
            Specializations = new List<SpecializationDb>();
        }
    }
}
